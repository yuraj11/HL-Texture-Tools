/*
  HLTools by Yuraj
  Copyright © 2006-2020 Juraj Novák (Yuraj)
  
  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.  
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FreeImageAPI;

namespace HLTools
{
    /// <summary>
    /// GoldSrc WAD Parser 0.8.1
    /// Written by Yuraj.
    /// </summary>
    public class WAD3Loader
    {
        public List<WADLump> LumpsInfo { get; private set; } = new List<WADLump>();
        public string Filename { get; private set; }

        //Private members
        private const int MaxPaletteColors = 256;
        private const int MaxNameLength = 16;
        private const int LumpSize = 32;
        private const int MaxTextureWidth = 4096;
        private const int MaxTextureHeight = 4096;
        private const int QCharWidth = 16;
        private const int QNumbOfGlyphs = 256;
        private readonly static byte[] WadHeaderId = { 0x57, 0x41, 0x44, 0x33 }; //WAD3

        private WADHeader header;
        private BinaryReader binReader;
        private FileStream fs;

        private long palleteBlockPos = 0;
        private long pixelsBlockPos = 0;
        private long lastImageSize = 0;
        private uint lastImageWidth = 0;

        public enum TextureEffects
        {
            None, Grayscale, Invert
        }

        /// <summary>
        /// WAD header.
        /// </summary>
        public struct WADHeader
        {
            public char[] Id; //Must be 4 chars = WAD3
            public uint LumpCount;
            public uint LumpOffset;
        }

        /// <summary>
        /// WAD lump (texture) info.
        /// </summary>
        public struct WADLump
        {
            public uint Offset;
            public uint CompressedLength;
            public uint FullLength;
            public byte Type;
            public byte Compression;
            public string Name;

            public override string ToString()
            {
                return Name;
            }
        }

        public class Texture
        {
            public Bitmap Image;
            public TextureMipmaps Mipmaps;

            public class TextureMipmaps
            {
                public byte[] Mipmap1;
                public byte[] Mipmap2;
                public byte[] Mipmap3;
            }
        }


        public struct CharInfo
        {
            public ushort StartOffset;
            public ushort CharWidth;

            public override string ToString()
            {
                return string.Format("Offset: {0:X} , Width: {1:X}", StartOffset, CharWidth);
            }
        }

        public struct IncludedBSPTexture
        {
            public uint Offset;
            public uint Size;
            public byte[] Name;

            public IncludedBSPTexture(uint offset, uint size, byte[] name)
            {
                Offset = offset;
                Size = size;
                Name = name;
            }

            public override string ToString()
            {
                return System.Text.Encoding.ASCII.GetString(Name);
            }
        }
        /*
       public class QFont
       {
           public uint Width;
           public uint Height;
           public uint RowCount;
           public uint RowHeight;
           public CharInfo[] FontInfo; //Size = 256
           public byte[] data; //Size = 4
       }*/


        /// <summary>
        /// Load and read WAD3 file.
        /// </summary>
        /// <param name="inputFile">Input file.</param>
        /// <exception cref="HLToolsUnsupportedFile"></exception>
        public void LoadFile(string inputFile)
        {
            Filename = inputFile;

            //Reset previous loaded data
            LumpsInfo.Clear();
            Close();

            fs = new FileStream(inputFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            binReader = new BinaryReader(fs);

            //First try get header ID
            header = new WADHeader
            {
                Id = binReader.ReadChars(4)
            };

            string magic = new string(header.Id);
            if (magic != System.Text.Encoding.ASCII.GetString(WadHeaderId)) //if invalid WAD file
            {
                Close();
                throw new HLToolsUnsupportedFile("Invalid or unsupported WAD File!");
            }

            header.LumpCount = binReader.ReadUInt32();
            header.LumpOffset = binReader.ReadUInt32();

            //Load all lumps info
            LoadLumpsInfo();
        }

        /// <summary>
        /// Close binary reader.
        /// </summary>
        public void Close()
        {
            if (binReader != null)
            {
                binReader.Close();
            }

            if (fs != null)
            {
                fs.Close();
            }
        }

        /// <summary>
        /// Extract Bitmap from WAD.
        /// </summary>
        /// <param name="index">Lump index.</param>
        /// <param name="transparent">Replace blue color with alpha 0.</param>
        /// <returns>Returns Texture. Contains image and Mipmaps.</returns>
        /// <exception cref="TextureDimensionException"></exception>
        public Texture GetLumpImage(int index, bool transparent = false)
        {
            Texture retVal;

            if (index > -1 && index < LumpsInfo.Count)
            {
                byte type = LumpsInfo[index].Type;

                //0x40 - tempdecal.wad
                //0x42 - cached.wad
                //0x43 - normald wads
                //0x46 - fonts 
                if (type == 0x40 || type == 0x42 || type == 0x43 || type == 0x46) //Supported types
                {
                    //Go to lump
                    binReader.BaseStream.Seek(LumpsInfo[index].Offset, SeekOrigin.Begin);

                    if (type == 0x40 || type == 0x43)
                    {
                        //Skip lump name
                        binReader.BaseStream.Seek(MaxNameLength, SeekOrigin.Current);
                    }

                    //Read texture size
                    uint width = binReader.ReadUInt32();
                    uint height = binReader.ReadUInt32();

                    if (width > MaxTextureWidth || height > MaxTextureHeight)
                        throw new TextureDimensionException("Texture width or height exceeds maximum size!");

                    if (width == 0 || height == 0)
                        throw new TextureDimensionException("Texture width and height must be larger than 0!");

                    //If QFont
                    if (type == 0x46)
                    {
                        //width = width * QCharWidth;
                        width = 256;
                        uint RowCount = binReader.ReadUInt32();
                        uint RowHeight = binReader.ReadUInt32();
                        CharInfo[] FontInfo = new CharInfo[QNumbOfGlyphs];
                        for (int i = 0; i < QNumbOfGlyphs; i++)
                        {
                            FontInfo[i].StartOffset = binReader.ReadUInt16();
                            FontInfo[i].CharWidth = binReader.ReadUInt16();
                        }

                    }

                    //Initialize bitmap
                    Bitmap bmp = new Bitmap((int)width, (int)height, PixelFormat.Format8bppIndexed);

                    //Read pixel offset, skip MIPMAPS offsets
                    if (type == 0x40 || type == 0x43)
                    {
                        //Not used, but needed
                        uint pixelOffset = binReader.ReadUInt32();

                        //Skip MIPMAPS offsets, not needed
                        binReader.BaseStream.Seek(12, SeekOrigin.Current);
                    }

                    //Read RAW pixels
                    uint pixelSize = width * height;
                    pixelsBlockPos = binReader.BaseStream.Position;
                    lastImageSize = pixelSize;
                    lastImageWidth = width;
                    byte[] pixels = pixels = binReader.ReadBytes((int)pixelSize);

                    //Read MIPMAPS
                    Texture.TextureMipmaps mipmaps = null;
                    if (type == 0x40 || type == 0x43)
                    {
                        mipmaps = new Texture.TextureMipmaps
                        {
                            Mipmap1 = binReader.ReadBytes((int)((width / 2) * (height / 2))),
                            Mipmap2 = binReader.ReadBytes((int)((width / 4) * (height / 4))),
                            Mipmap3 = binReader.ReadBytes((int)((width / 8) * (height / 8)))
                        };
                    }

                    //Padding 2-bytes
                    binReader.BaseStream.Seek(2, SeekOrigin.Current);

                    //Prepare new palette for bitmap
                    ColorPalette pal = bmp.Palette;
                    palleteBlockPos = binReader.BaseStream.Position;

                    //Read palette bytes from file into array
                    byte[] palBytes = binReader.ReadBytes(MaxPaletteColors * 3);
                    for (int i = 0, j = 0; i < MaxPaletteColors; i++)
                    {
                        if (type == 0x40) //e.g.: tempdecal.wad
                        {
                            pal.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        else
                        {
                            //Read palette entry RGB
                            pal.Entries[i] = Color.FromArgb(palBytes[j], palBytes[j + 1], palBytes[j + 2]);
                        }

                        //Check for transparent (blue) color
                        if (transparent && i == (MaxPaletteColors - 1) && LumpsInfo[index].Name.StartsWith("{"))
                        {
                            pal.Entries[i] = Color.FromArgb(0, pal.Entries[i]);
                        }
                        j += 3;
                    }
                    bmp.Palette = pal;

                    //Lock bitmap for pixel manipulation
                    BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, (int)width, (int)height),ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                    System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmd.Scan0, pixels.Length);
                    bmp.UnlockBits(bmd);

                    retVal = new Texture
                    {
                        Image = bmp,
                        Mipmaps = mipmaps
                    };
                }
                else
                {
                    throw new HLToolsUnsupportedFile("Unsupported type (0x" + LumpsInfo[index].Type.ToString("X") + ") of lump (texture) in WAD!");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

            return retVal;
        }

        /// <summary>
        /// Change color palette of last loaded texture using GetLumpImage.
        /// </summary>
        /// <param name="newPalette"></param>
        public void ChangeColorPalette(ColorPalette newPalette, string outputFileName = null)
        {
            //Check if valid palette
            if (newPalette == null) return;

            //Save to original file
            if (outputFileName == null)
            {
                fs.Seek(palleteBlockPos, SeekOrigin.Begin); //Skip first

                for (int i = 0; i < newPalette.Entries.Length; i++)
                {
                    fs.WriteByte(newPalette.Entries[i].R);
                    fs.WriteByte(newPalette.Entries[i].G);
                    fs.WriteByte(newPalette.Entries[i].B);
                }
                fs.Flush();
                //binReader.BaseStream.Flush();
            }
            else
            {
                //Save to other file than original
                try
                {
                    File.Copy(Filename, outputFileName);
                    using (FileStream sw = new FileStream(outputFileName, FileMode.Open, FileAccess.ReadWrite))
                    {
                        sw.Seek(palleteBlockPos, SeekOrigin.Begin); //Skip first

                        for (int i = 0; i < newPalette.Entries.Length; i++)
                        {
                            sw.WriteByte(newPalette.Entries[i].R);
                            sw.WriteByte(newPalette.Entries[i].G);
                            sw.WriteByte(newPalette.Entries[i].B);
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Load basic lumps data.
        /// </summary>
        private void LoadLumpsInfo()
        {
            //Seek to first lump
            binReader.BaseStream.Seek(header.LumpOffset, SeekOrigin.Begin);

            //Iterate all lumps, insert every lump to array
            for (int i = 0; i < header.LumpCount; i++)
            {
                WADLump lump = new WADLump
                {
                    Offset = binReader.ReadUInt32(),
                    CompressedLength = binReader.ReadUInt32(),
                    FullLength = binReader.ReadUInt32(),
                    Type = binReader.ReadByte(),
                    Compression = binReader.ReadByte()
                };
                //Padding, 2-bytes
                binReader.BaseStream.Seek(2, SeekOrigin.Current);
                lump.Name = GetNullTerminatedString(binReader.ReadChars(MaxNameLength));

                LumpsInfo.Add(lump);
            }
        }

        private string GetNullTerminatedString(char[] chars)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == 0x0) break;

                sb.Append(chars[i]);
            }
            return sb.ToString();
        }

        private static byte[] CreateTextureName(string text)
        {
            byte[] newName = new byte[MaxNameLength];
            byte[] b = System.Text.Encoding.ASCII.GetBytes(text);
            b.CopyTo(newName, 0);

            newName[MaxNameLength - 1] = 0;

            return newName;
        }

        public void ChangeTextureName(int lumpIndex, string val)
        {
            byte[] newName = CreateTextureName(val);

            //Edit in header
            //Seek to first lump
            fs.Seek(header.LumpOffset + ((lumpIndex + 1) * LumpSize) - 16, SeekOrigin.Begin);
            fs.Write(newName, 0, newName.Length);

            //Edit in texture if type
            if (LumpsInfo[lumpIndex].Type == 0x40 || LumpsInfo[lumpIndex].Type == 0x43)
            {
                fs.Seek(LumpsInfo[lumpIndex].Offset, SeekOrigin.Begin);
                fs.Write(newName, 0, newName.Length);
            }
            fs.Flush();

            WADLump lumpRenamed = new WADLump
            {
                CompressedLength = LumpsInfo[lumpIndex].CompressedLength,
                Compression = LumpsInfo[lumpIndex].Compression,
                FullLength = LumpsInfo[lumpIndex].FullLength,
                Name = val,
                Offset = LumpsInfo[lumpIndex].Offset,
                Type = LumpsInfo[lumpIndex].Type
            };

            LumpsInfo[lumpIndex] = lumpRenamed;
        }

        /// <summary>
        /// Switch color index in texture of last loaded using GetLumpImage.
        /// </summary>
        /// <param name="lumpIndex">Index of texture.</param>
        /// <param name="source">Source palette index.</param>
        /// <param name="destination">Destination palette index.</param>
        public void SwitchColorIndex(byte source, byte destination)
        {
            binReader.BaseStream.Seek(pixelsBlockPos, SeekOrigin.Begin);
            for (int p = 0; p < lastImageSize; p++)
            {
                if (binReader.ReadByte() == source)
                {
                    fs.Seek(binReader.BaseStream.Position - 1, SeekOrigin.Begin);
                    fs.WriteByte(destination);
                }
            }

            fs.Flush();
        }

        public byte GetPixelIndexAtPos(int x, int y)
        {
            long relativePos = (y * lastImageWidth) + x;
            long pos = pixelsBlockPos + relativePos;
            if (pos > binReader.BaseStream.Length || pos < 0) throw new ArgumentOutOfRangeException();
            binReader.BaseStream.Seek(pos, SeekOrigin.Begin);
            return binReader.ReadByte();
        }

        /// <summary>
        /// Extract WADs from BSP file.
        /// </summary>
        /// <param name="inputBspFilename">Input compiled map file BSP.</param>
        /// <param name="outputWadFilename">Output file to save textures.</param>
        public static int ExtractWadFromBsp(string inputBspFilename, string outputWadFilename)
        {
            //TODO: Remove unused name fragments from exported wad
            const int sizeInt = sizeof(int);
            bool foundEOF = false;
            List<IncludedBSPTexture> includedTextures = new List<IncludedBSPTexture>();

            //Ugly slow algorithm but it works! :D
            if (Path.GetExtension(inputBspFilename) != ".bsp")
                throw new ArgumentException("Input file must be BSP!");

            long bspLength = 0;
            using (FileStream fs = new FileStream(inputBspFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                bspLength = fs.Length;
                byte[] buffer = new byte[bspLength]; //TODO: not good solution
                byte[] num1 = new byte[sizeInt];
                byte[] num2 = new byte[sizeInt];
                byte[] num3 = new byte[sizeInt];
                byte[] num4 = new byte[sizeInt];

                while (fs.Read(buffer, 0, buffer.Length) != 0)
                {
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        if ((i + 19 + 16) > buffer.Length) break;
                        //Search pattern
                        byte nullByte1 = buffer[i];
                        byte nullByte2 = buffer[i + 1];
                        byte firstNameByte = buffer[i + 2];
                        byte secondNameByte = buffer[i + 3];
                        //+15 bytes of name
                        byte lastNameByte = buffer[i + 17];
                        Buffer.BlockCopy(buffer, i + 18, num1, 0, num1.Length);
                        Buffer.BlockCopy(buffer, i + 18 + sizeInt, num2, 0, num2.Length);
                        Buffer.BlockCopy(buffer, i + 18 + sizeInt * 2, num3, 0, num3.Length);
                        Buffer.BlockCopy(buffer, i + 18 + sizeInt * 3, num4, 0, num4.Length);
                        int num1int = BitConverter.ToInt32(num1, 0); //width
                        int num2int = BitConverter.ToInt32(num2, 0); //height
                        int num3int = BitConverter.ToInt32(num3, 0); // > 0 for custom wad
                        int num4int = BitConverter.ToInt32(num4, 0); // > 0 for custom wad


                        if (((nullByte2) < 2) && firstNameByte > 32 && firstNameByte < 127 &&
                            secondNameByte > 32 && secondNameByte < 127 &&
                            lastNameByte == 0 && num1int > 0 && num1int < 1024 && num2int > 0 && num2int < 1024)
                        {
                            uint textureOffset = (uint)i + 2;
                            long textureOffsetAbsolute = textureOffset + fs.Position - buffer.Length;

                            if (num3int > 0 && num3int < bspLength && num4int > 0 && num4int < bspLength) //custom included wad
                            {
                                //Set previous texture size
                                if (includedTextures.Count > 0)
                                {
                                    uint offset = includedTextures[includedTextures.Count - 1].Offset;
                                    byte[] name = includedTextures[includedTextures.Count - 1].Name;
                                    includedTextures[includedTextures.Count - 1] = new IncludedBSPTexture(offset, (uint)(textureOffsetAbsolute - offset), name);
                                }

                                string nameNulled = System.Text.Encoding.ASCII.GetString(buffer, (int)textureOffset, 16);
                                string normal = nameNulled.Substring(0, nameNulled.IndexOf('\0'));

                                includedTextures.Add(new IncludedBSPTexture((uint)textureOffsetAbsolute, 0, CreateTextureName(normal)));
                            }
                            else
                            {
                                if (includedTextures.Count > 0 && textureOffsetAbsolute > includedTextures[includedTextures.Count - 1].Offset)
                                {
                                    uint offsetOrig = includedTextures[includedTextures.Count - 1].Offset;
                                    byte[] nameOrig = includedTextures[includedTextures.Count - 1].Name;
                                    includedTextures[includedTextures.Count - 1] =
                                        new IncludedBSPTexture(offsetOrig,
                                            (uint)textureOffsetAbsolute - offsetOrig,
                                            nameOrig);
                                    foundEOF = true;
                                }
                            }
                        }
                    }
                }
            }

            //Fix last texture size
            if (includedTextures.Count < 1) return 0;

            if (includedTextures[includedTextures.Count - 1].Size == 0 && !foundEOF)
            {
                uint offsetOrig = includedTextures[includedTextures.Count - 1].Offset;
                byte[] nameOrig = includedTextures[includedTextures.Count - 1].Name;
                includedTextures[includedTextures.Count - 1] = new IncludedBSPTexture(offsetOrig, (uint)(bspLength - offsetOrig), nameOrig);
            }

            //Lets extract textures to single wad
            using (FileStream fs2 = new FileStream(inputBspFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (FileStream fsOut = new FileStream(outputWadFilename, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            using (BinaryWriter fsWad = new BinaryWriter(fsOut))
            {
                fsWad.Write(WadHeaderId, 0, WadHeaderId.Length);
                fsWad.Write(includedTextures.Count);
                fsWad.Write(0); //offset lumps, later changed

                int[] outputWadOffsets = new int[includedTextures.Count];

                for (int i = 0; i < includedTextures.Count; i++)
                {
                    fs2.Seek((long)includedTextures[i].Offset, SeekOrigin.Begin);
                    byte[] texture = new byte[includedTextures[i].Size];
                    fs2.Read(texture, 0, texture.Length);
                    outputWadOffsets[i] = (int)fsWad.BaseStream.Position;
                    fsWad.Write(texture, 0, texture.Length);
                }

                //Write lump infos
                long posLumps = fsWad.BaseStream.Position;
                fsWad.Seek(8, SeekOrigin.Begin);
                fsWad.Write((uint)posLumps);
                fsWad.Seek((int)posLumps, SeekOrigin.Begin);
                //Write Lumps infos
                for (int i = 0; i < includedTextures.Count; i++)
                {
                    fsWad.Write(outputWadOffsets[i]);
                    fsWad.Write(includedTextures[i].Size);
                    fsWad.Write(includedTextures[i].Size);
                    fsWad.Write((byte)0x43);
                    fsWad.Write((byte)0);
                    fsWad.Write(new byte[] { 0x00, 0x00 });
                    fsWad.Write(includedTextures[i].Name);
                }
            }

            return includedTextures.Count;
        }

        public static void CreateWad(string outputFilename, string[] images, string[] names, bool reserverLastPalColor = false)
        {
            CreateWad(outputFilename, images, names, Color.Blue, reserverLastPalColor);
        }

        /// <summary>
        /// Compile input images into WAD texture file.
        /// </summary>
        /// <param name="outputFilename">Output wad file path.</param>
        /// <param name="images">Input image files.</param>
        /// <param name="names">Names of textures.</param>
        /// <param name="reserverLastPalColor">Reserve last color in palette if name starts with {.</param>
        public static void CreateWad(string outputFilename, string[] images, string[] names, Color alphaReplacementColor, bool reserverLastPalColor = false)
        {
            using (FileStream fs = new FileStream(outputFilename, FileMode.Create))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                //Convert bitmaps to 8bpp format
                List<FreeImageBitmap> imgs = new List<FreeImageBitmap>();
                for (int i = 0; i < images.Length; i++)
                {
                    //Quantize images
                    FreeImageBitmap originalImage = new FreeImageBitmap(images[i]);

                    //If texture will be transparent, reserve last color if enabled
                    bool reserveLastClr = (names[i].StartsWith("{") && reserverLastPalColor);
                    bool isTransparentImage = originalImage.IsTransparent;
                    bool is8Bpp = originalImage.BitsPerPixel == 8;
                    int r = reserveLastClr ? 1 : 0;

                    if (isTransparentImage)
                    {
                        if (originalImage.TransparentIndex != -1)
                        {
                            originalImage.Palette[originalImage.TransparentIndex] = alphaReplacementColor;
                        }
                        else
                        {
                            originalImage.SwapColors(new RGBQUAD(Color.FromArgb(0, 0, 0, 0)), new RGBQUAD(alphaReplacementColor), false);
                        }
                    }

                    originalImage.Quantize(FREE_IMAGE_QUANTIZE.FIQ_NNQUANT, MaxPaletteColors - r);
                    if (!is8Bpp)
                    {
                        originalImage.ConvertColorDepth(FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
                    }

                    if (reserveLastClr) {
                        if (isTransparentImage)
                        {
                            bool foundReplacementColor = false;
                            for (int pindex = 0; pindex < originalImage.Palette.Length; pindex++)
                            {
                                RGBQUAD rgb = originalImage.Palette.GetValue(pindex);
                                if (rgb.rgbRed == alphaReplacementColor.R && rgb.rgbGreen == alphaReplacementColor.G && rgb.rgbBlue == alphaReplacementColor.B)
                                {
                                    var lastColor = originalImage.Palette.GetValue(MaxPaletteColors - 1);
                                    originalImage.Palette[pindex] = lastColor;
                                    originalImage.Palette[MaxPaletteColors - 1] = new RGBQUAD(alphaReplacementColor);
                                    originalImage.SwapPaletteIndices((byte)pindex, MaxPaletteColors - 1);
                                    if (originalImage.TransparentIndex != -1) {
                                        originalImage.TransparentIndex = MaxPaletteColors - 1;
                                    }
                                    foundReplacementColor = true;
                                    break;
                                }
                            }

                            // If didn't found replacement, set directly last alpha color
                            if (!foundReplacementColor)
                            {
                                originalImage.Palette[MaxPaletteColors - 1] = new RGBQUAD(alphaReplacementColor);
                            }
                        }
                        else
                        {
                            originalImage.Palette[MaxPaletteColors - 1] = new RGBQUAD(alphaReplacementColor);
                        }
                    }

                    imgs.Add(originalImage);
                }
                uint[] offsets = new uint[images.Length];
                uint[] sizes = new uint[images.Length];

                //WAD header
                bw.Write(WadHeaderId);
                bw.Write(images.Length);
                bw.Write(0); //This will be changed later

                //Write textures
                for (int i = 0; i < images.Length; i++)
                {
                    uint posTextureStart = (uint)bw.BaseStream.Position;
                    offsets[i] = posTextureStart;
                    //Texture name
                    byte[] name = CreateTextureName(names[i]);
                    bw.Write(name, 0, name.Length);

                    //Texture dimensions
                    bw.Write(imgs[i].Width);
                    bw.Write(imgs[i].Height);

                    //Offsets
                    uint posImage = (uint)(bw.BaseStream.Position - posTextureStart);
                    bw.Write(posImage + 16); //image
                    int pixelSize = ((imgs[i].Width) * (imgs[i].Height));
                    int m1 = ((imgs[i].Width / 2) * (imgs[i].Height / 2));
                    int m2 = ((imgs[i].Width / 4) * (imgs[i].Height / 4));
                    int m3 = ((imgs[i].Width / 8) * (imgs[i].Height / 8));
                    bw.Write((uint)(posImage + pixelSize + 16)); //mipmap1
                    bw.Write((uint)(posImage + pixelSize + m1 + 16)); //mipmap2
                    bw.Write((uint)(posImage + pixelSize + m1 + m2 + 16)); //mipmap3

                    //Write pixel data
                    imgs[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
                    byte[] arr = new byte[imgs[i].Width * imgs[i].Height];
                    System.Runtime.InteropServices.Marshal.Copy(imgs[i].GetScanlinePointer(0), arr, 0, arr.Length);
                    Array.Reverse(arr);
                    bw.Write(arr);
                    //

                    //Mip map data
                    int factor = 2;
                    for (int a = 0; a < 3; a++)
                    {
                        int widthMM = (imgs[i].Width / factor);
                        int heightMM = (imgs[i].Height / factor);

                        using (FreeImageBitmap clBmp = new FreeImageBitmap(imgs[i]))
                        {
                            //TODO: Transparent png
                            clBmp.Rescale(widthMM, heightMM, FREE_IMAGE_FILTER.FILTER_LANCZOS3);
                            clBmp.Quantize(FREE_IMAGE_QUANTIZE.FIQ_NNQUANT, MaxPaletteColors, imgs[i].Palette);

                            byte[] arrMM = new byte[widthMM * heightMM];
                            System.Runtime.InteropServices.Marshal.Copy(clBmp.GetScanlinePointer(0), arrMM, 0, arrMM.Length);
                            Array.Reverse(arrMM);
                            bw.Write(arrMM);
                        }
                        factor *= 2;
                    }

                    //Unknown 2 bytes
                    bw.Write(new byte[] { 0x00, 0x01 });

                    //Write color palette
                    for (int p = 0; p < imgs[i].Palette.Length; p++)
                    {
                        bw.Write(imgs[i].Palette[p].rgbRed);
                        bw.Write(imgs[i].Palette[p].rgbGreen);
                        bw.Write(imgs[i].Palette[p].rgbBlue);
                    }

                    //Padding
                    bw.Write(new byte[] { 0x00, 0x00 });
                    sizes[i] = (uint)bw.BaseStream.Position - posTextureStart;
                }

                long posLumps = bw.BaseStream.Position;
                bw.Seek(8, SeekOrigin.Begin);
                bw.Write((uint)posLumps);
                bw.Seek((int)posLumps, SeekOrigin.Begin);
                //Write Lumps infos
                for (int i = 0; i < images.Length; i++)
                {
                    bw.Write(offsets[i]);
                    bw.Write(sizes[i]);
                    bw.Write(sizes[i]);
                    bw.Write((byte)0x43);
                    bw.Write((byte)0);
                    bw.Write(new byte[] { 0x00, 0x00 });
                    byte[] name = CreateTextureName(names[i]);
                    bw.Write(name, 0, name.Length);
                }

                //Free resources
                for (int i = 0; i < imgs.Count; i++)
                {
                    imgs[i].Dispose();
                }
            }
        }
    }
}
