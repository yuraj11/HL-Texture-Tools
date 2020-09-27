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
using System.Linq;
using FreeImageAPI;

namespace HLTools
{
    /// <summary>
    /// Type of sprite.
    /// </summary>
    public enum SprType
    {
        VP_PARALLEL_UPRIGHT,
        FACING_UPRIGHT,
        VP_PARALLEL,
        ORIENTED,
        VP_PARALLEL_ORIENTED
    }

    /// <summary>
    /// Texture format of sprite.
    /// </summary>
    public enum SprTextFormat
    {
        SPR_NORMAL,
        SPR_ADDITIVE,
        SPR_INDEXALPHA,
        SPR_ALPHTEST
    }

    /// <summary>
    /// Synch. type of sprite.
    /// </summary>
    public enum SprSynchType
    {
        Synchronized,
        Random
    }

    /// <summary>
    /// GoldSrc Sprites Parser 0.8
    /// Written by Yuraj.
    /// </summary>
    public class SpriteLoader
    {
        /// <summary>
        /// Sprite header.
        /// </summary>
        public struct SprHeader
        {
            public char[] Id; //Must be 4 chars = IDSP
            public int Version;
            public SprType Type;
            public SprTextFormat TextFormat;
            public float BoundingRadius;
            public int MaxWidth;
            public int MaxHeight;
            public int NumFrames;
            public float BeamLen;
            public SprSynchType SynchType;
        }

        /// <summary>
        /// Sprite frame.
        /// </summary>
        public struct Frame
        {
            public int OriginX;
            public int OriginY;
            public Bitmap Image;
        }

        public SprHeader SpriteHeader { get; private set; }
        public string Filename { get; private set; }

        private const string SpriteHeaderId = "IDSP";
        private const int MaxPaletteColors = 256;

        private BinaryReader binReader;
        private FileStream fs;
        private long[] indexesOfPixelPositions;
        private uint[] pixelsLengths;

        /// <summary>
        /// Load and read Sprite file.
        /// </summary>
        /// <param name="inputFile">Input file.</param>
        /// <param name="transparent">Replace blue color with transparent.</param>
        /// <exception cref="HLToolsUnsupportedFile"></exception>
        public Frame[] LoadFile(string inputFile, bool transparent = false)
        {
            Filename = inputFile;

            //Reset previous loaded data
            List<Frame> frames = new List<Frame>();
            Close();

            fs = new FileStream(inputFile, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            binReader = new BinaryReader(fs);

            //First try get header ID
            SprHeader spriteHeader = new SprHeader();
            spriteHeader.Id = binReader.ReadChars(4);

            string magic = new string(spriteHeader.Id);
            if (magic != SpriteHeaderId) //if invalid SPR file
            {
                throw new HLToolsUnsupportedFile("Invalid or unsupported Sprite File!");
            }

            spriteHeader.Version = binReader.ReadInt32();
            spriteHeader.Type = (SprType)binReader.ReadInt32();
            spriteHeader.TextFormat = (SprTextFormat)binReader.ReadInt32();
            spriteHeader.BoundingRadius = binReader.ReadSingle();
            spriteHeader.MaxWidth = binReader.ReadInt32();
            spriteHeader.MaxHeight = binReader.ReadInt32();
            spriteHeader.NumFrames = binReader.ReadInt32();
            spriteHeader.BeamLen = binReader.ReadSingle();
            spriteHeader.SynchType = (SprSynchType)binReader.ReadInt32();

            SpriteHeader = spriteHeader;

            //Palette length
            ushort u = binReader.ReadUInt16();

            //Prepare new palette for bitmap
            ColorPalette pal = null;
            using (Bitmap tmpBitmap = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                pal = tmpBitmap.Palette;
                byte[] palBytes = binReader.ReadBytes(u * 3);
                for (int i = 0, j = 0; i < u; i++)
                {
                    //Load (R,G,B) from file
                    /*if (transparent && SpriteHeader.TextFormat == SprTextFormat.SPR_ADDITIVE)
                    {
                        //pal.Entries[i] = Color.FromArgb((u - 1) - i, palBytes[j], palBytes[j + 1], palBytes[j + 2]);
                    }
                    else
                    {
                        //pal.Entries[i] = Color.FromArgb(palBytes[j], palBytes[j + 1], palBytes[j + 2]);
                    }*/
                    pal.Entries[i] = Color.FromArgb(palBytes[j], palBytes[j + 1], palBytes[j + 2]);

                    //Check for transparent color
                    if (i == (u - 1)) //256th color is alpha
                    {
                        if (transparent && spriteHeader.TextFormat == SprTextFormat.SPR_ALPHTEST)
                        {
                            pal.Entries[i] = Color.FromArgb(0, pal.Entries[i]);
                        }

                    }

                    j += 3;
                }
            }

            indexesOfPixelPositions = new long[spriteHeader.NumFrames];
            pixelsLengths = new uint[spriteHeader.NumFrames];


            //Load frames
            for (int i = 0; i < spriteHeader.NumFrames; i++)
            {
                int frameGroup = binReader.ReadInt32();
                int frameOriginX = binReader.ReadInt32();
                int frameOriginY = binReader.ReadInt32();
                int frameWidth = binReader.ReadInt32();
                int frameHeight = binReader.ReadInt32();

                Bitmap bmp = new Bitmap(frameWidth, frameHeight, PixelFormat.Format8bppIndexed)
                {
                    Palette = pal
                };

                //Get pixelsize
                uint pixelSize = (uint)(frameWidth * frameHeight);

                indexesOfPixelPositions[i] = binReader.BaseStream.Position;
                pixelsLengths[i] = pixelSize;

                //Load all pixels from file to array
                byte[] pixels = binReader.ReadBytes((int)pixelSize);

                //Lock bitmap for pixel manipulation
                BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmd.Scan0, pixels.Length);
                bmp.UnlockBits(bmd);

                //Insert new frame to frames list
                Frame frame = new Frame
                {
                    OriginX = frameOriginX,
                    OriginY = frameOriginY,
                    Image = bmp
                };
                frames.Add(frame);
            }

            return frames.ToArray();
        }

        public static void CreateSpriteFile(string outputPath, string[] files, SprType spriteType, SprTextFormat textFormat, int palIndex)
        {
            CreateSpriteFile(outputPath, files, spriteType, textFormat, palIndex, Color.Blue);
        }

        /// <summary>
        /// Create new sprite file from image files.
        /// </summary>
        /// <param name="outputPath">Output filename *.SPR</param>
        /// <param name="files">Input image paths.</param>
        /// <param name="spriteType">SprType</param>
        /// <param name="textFormat">SprTextFormat</param>
        /// <param name="palIndex">Which palette use from files</param>
        public static void CreateSpriteFile(string outputPath, string[] files, SprType spriteType, SprTextFormat textFormat, int palIndex, Color alphaReplacementColor)
        {
            List<FreeImageBitmap> images = files.Select(file => new FreeImageBitmap(file)).ToList();

            //Retrieve maximum width, height
            int prevSize = 0;
            int maxW = 0, maxH = 0;

            for (int i = 0; i < images.Count; i++)
            {
                FreeImageBitmap image = images[i];
                if ((image.Height + image.Width) > prevSize)
                {
                    prevSize = image.Height + image.Width;
                    maxW = image.Width;
                    maxH = image.Height;
                }

                if (image.IsTransparent)
                {
                    image.SwapColors(new RGBQUAD(Color.Transparent), new RGBQUAD(alphaReplacementColor), false);
                }
            }

            //Calc. bounding box
            float f = (float)Math.Sqrt((maxW >> 1) * (maxW >> 1) + (maxH >> 1) * (maxH >> 1));

            using (BinaryWriter bw = new BinaryWriter(new FileStream(outputPath, FileMode.Create)))
            {
                //Write header first
                bw.Write(SpriteHeaderId.ToCharArray());
                bw.Write(2);
                bw.Write((uint)spriteType);
                bw.Write((uint)textFormat);
                bw.Write(f);
                bw.Write(maxW);
                bw.Write(maxH);
                bw.Write(images.Count);
                bw.Write(0.0f); //Always 0 ?
                bw.Write(1); //Synch. type
                //Color palette
                bw.Write((ushort)MaxPaletteColors); //Always 256 ?

                if ((palIndex > (images.Count - 1)) || palIndex < images.Count)
                {
                    palIndex = 0;
                }

                if (!images[palIndex].HasPalette || images[palIndex].Palette.Length != 256)
                {
                    images[palIndex].ConvertColorDepth(FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
                }

                Palette pal = images[palIndex].Palette;

                for (int i = 0; i < 256; i++)
                {
                    bw.Write(pal[i].rgbRed);
                    bw.Write(pal[i].rgbGreen);
                    bw.Write(pal[i].rgbBlue);
                }

                //Write images
                for (int i = 0; i < images.Count; i++)
                {
                    bw.Write(0); //group
                    bw.Write(-(images[i].Width / 2)); //origin x
                    bw.Write(images[i].Height / 2); //origin y
                    bw.Write(images[i].Width); //w
                    bw.Write(images[i].Height); //h

                    byte[] arr = new byte[images[i].Width * images[i].Height];
                    images[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
                    System.Runtime.InteropServices.Marshal.Copy(images[i].GetScanlinePointer(0), arr, 0, arr.Length);
                    Array.Reverse(arr);
                    bw.Write(arr);
                }
            }

            //Free resources
            images.ForEach(image => image.Dispose());
        }


        /// <summary>
        /// Close file.
        /// </summary>
        public void Close()
        {
            //Close binaryreader
            if (binReader != null)
            {
                binReader.Close();
            }

            //Close filestream
            if (fs != null)
            {
                fs.Close();
            }
        }


        /// <summary>
        /// Only change sprite type. This method doesn't check header.
        /// </summary>
        /// <param name="inputFile">Sprite file.</param>
        /// <param name="newType">New sprite type.</param>
        public void FixSpriteType(SprType newType)
        {
            if (fs.Length >= 12)
            {
                fs.Seek(8, SeekOrigin.Begin); //Skip first
                byte[] type = BitConverter.GetBytes((int)newType);
                fs.Write(type, 0, type.Length);
                fs.Flush();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Change color palette of sprite.
        /// </summary>
        /// <param name="newPalette"></param>
        public void ChangeColorPalette(ColorPalette newPalette, string outputFileName = null)
        {
            //Check if valid palette
            if (newPalette == null) return;

            //Save to original file
            if (outputFileName == null)
            {
                fs.Seek(0x2A, SeekOrigin.Begin); //Skip first

                for (int i = 0; i < newPalette.Entries.Length; i++)
                {
                    fs.WriteByte(newPalette.Entries[i].R);
                    fs.WriteByte(newPalette.Entries[i].G);
                    fs.WriteByte(newPalette.Entries[i].B);
                }
                fs.Flush();
            }
            else
            {
                //Save to other file than original
                try
                {
                    File.Copy(Filename, outputFileName);
                    using (FileStream sw = new FileStream(outputFileName, FileMode.Open, FileAccess.ReadWrite))
                    {
                        sw.Seek(0x2A, SeekOrigin.Begin); //Skip first

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

        public byte GetPixelIndexAtPos(int frameIndex, int x, int y)
        {
            long relativePos = (y * SpriteHeader.MaxWidth) + x;
            long pos = indexesOfPixelPositions[frameIndex] + relativePos;
            if (pos > binReader.BaseStream.Length) throw new ArgumentOutOfRangeException();
            binReader.BaseStream.Seek(pos, SeekOrigin.Begin);
            return binReader.ReadByte();
        }

        /// <summary>
        /// Switch color index in every frame.
        /// </summary>
        /// <param name="source">Source palette index.</param>
        /// <param name="destination">Destination palette index.</param>
        public void SwitchColorIndex(byte source, byte destination)
        {
            binReader.BaseStream.Seek(0x14, SeekOrigin.Begin);

            for (int i = 0; i < indexesOfPixelPositions.Length; i++)
            {
                binReader.BaseStream.Seek(indexesOfPixelPositions[i], SeekOrigin.Begin);
                for (int p = 0; p < pixelsLengths[i]; p++)
                {
                    if (binReader.ReadByte() == source)
                    {

                        fs.Seek(binReader.BaseStream.Position - 1, SeekOrigin.Begin);
                        fs.WriteByte(destination);
                    }
                }
            }

            fs.Flush();
        }
    }
}
