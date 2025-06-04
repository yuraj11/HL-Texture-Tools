/*
  HLTools by Yuraj
  Copyright © 2006-2024 Juraj Novák (Yuraj)

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
using System.Runtime.InteropServices;
using System.Text;
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
    /// GoldSrc Sprites Parser 0.8.5
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
        private static readonly Encoding DefaultEncoding = Encoding.ASCII;

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
            binReader = new BinaryReader(fs, DefaultEncoding);

            //First try to get header ID
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
            ColorPalette pal;
            using (var tmpBitmap = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                pal = tmpBitmap.Palette;
                byte[] palBytes = binReader.ReadBytes(u * 3);
                for (int i = 0, j = 0; i < u; i++)
                {
                    //Load (R,G,B) from file
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
                BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, 
                    PixelFormat.Format8bppIndexed);
                Marshal.Copy(pixels, 0, bmd.Scan0, pixels.Length);
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

        /// <summary>
        /// Create new sprite file from image files.
        /// </summary>
        /// <param name="outputPath">Output filename *.SPR</param>
        /// <param name="files">Input image paths.</param>
        /// <param name="spriteType">SprType</param>
        /// <param name="textFormat">SprTextFormat</param>
        /// <param name="palIndex">Which palette use from files</param>
        /// <param name="alphaReplacementColor">Which color to use as replacement for alpha</param>
        /// <param name="quantizePalette">Use quantized palette of all frames.</param>
        public static void CreateSpriteFile(string outputPath, string[] files, SprType spriteType,
            SprTextFormat textFormat, int palIndex, Color alphaReplacementColor, bool quantizePalette)
        {
            var images = files.Select(file => new FreeImageBitmap(file)).ToList();

            //Retrieve maximum width, height
            int prevSize = 0;
            int maxW = 0, maxH = 0;

            foreach (var image in images)
            {
                if ((image.Height + image.Width) > prevSize)
                {
                    prevSize = image.Height + image.Width;
                    maxW = image.Width;
                    maxH = image.Height;
                }

                if (image.IsTransparent)
                {
                    if (image.TransparentIndex != -1)
                    {
                        image.Palette[image.TransparentIndex] = alphaReplacementColor;
                    }
                    else
                    {
                        image.SwapColors(new RGBQUAD(Color.FromArgb(0, 0, 0, 0)), new RGBQUAD(alphaReplacementColor),
                            false);
                    }
                }

                // Convert to 8BPP image if needed
                if (!image.HasPalette || image.Palette.Length != MaxPaletteColors)
                {
                    image.ConvertColorDepth(FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
                }
            }

            //Calc. bounding box
            float f = (float)Math.Sqrt((maxW >> 1) * (maxW >> 1) + (maxH >> 1) * (maxH >> 1));

            using (var bw = new BinaryWriter(new FileStream(outputPath, FileMode.Create), DefaultEncoding))
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
                bw.Write(1); //Synchronization type
                bw.Write((ushort)MaxPaletteColors); // Color palette (always 256)

                Palette palette;

                if (quantizePalette)
                {
                    // Create big image with all frames
                    // (could be optimized without creating a separate image)

                    int width = images.Sum(img => img.Width);
                    int height = images.Max(img => img.Height);

                    using (var canvas = new Bitmap(width, height))
                    using (var graphics = Graphics.FromImage(canvas))
                    {
                        int totalImageWidth = 0;

                        foreach (var image in images)
                        {
                            graphics.DrawImage(image.ToBitmap(), totalImageWidth, 0);
                            totalImageWidth += image.Width;
                        }

                        using (var allImages = new FreeImageBitmap(canvas))
                        {
                            allImages.ConvertColorDepth(FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);
                            palette = new Palette(allImages.Palette.AsArray);
                        }
                    }
                }
                else
                {
                    palIndex = Math.Max(0, Math.Min(palIndex, images.Count - 1));
                    palette = images[palIndex].Palette;
                }

                // Swap last color for a transparent one
                if (quantizePalette && textFormat == SprTextFormat.SPR_ALPHTEST)
                {
                    var transparentIndex = palette.AsArray.ToList().FindIndex(rgb =>
                        rgb.rgbRed == alphaReplacementColor.R && rgb.rgbGreen == alphaReplacementColor.G &&
                        rgb.rgbBlue == alphaReplacementColor.B);

                    if (transparentIndex != -1)
                    {
                        var tempColor = palette[MaxPaletteColors - 1];
                        palette[MaxPaletteColors - 1] = new RGBQUAD(alphaReplacementColor);
                        palette[transparentIndex] = tempColor;                        
                    }
                }

                for (int i = 0; i < MaxPaletteColors; i++)
                {
                    bw.Write(palette[i].rgbRed);
                    bw.Write(palette[i].rgbGreen);
                    bw.Write(palette[i].rgbBlue);
                }

                //Write images
                foreach (var image in images)
                {
                    bw.Write(0); //group
                    bw.Write(-(image.Width / 2)); //origin x
                    bw.Write(image.Height / 2); //origin y
                    bw.Write(image.Width); //w
                    bw.Write(image.Height); //h

                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);

                    byte[] imagePixels;
                    if (quantizePalette)
                    {
                        imagePixels = RemapImageToPalette(image, palette);
                    }
                    else
                    {
                        imagePixels = new byte[image.Width * image.Height];
                        Marshal.Copy(image.GetScanlinePointer(0), imagePixels, 0, imagePixels.Length);
                    }

                    Array.Reverse(imagePixels);
                    bw.Write(imagePixels);
                }
            }

            //Free resources
            images.ForEach(image => image.Dispose());
        }

        private static int FindClosestPaletteIndex(Color color, Palette palette, Dictionary<int, int> colorCache)
        {
            int colorKey = (color.R << 16) | (color.G << 8) | color.B;

            if (colorCache.TryGetValue(colorKey, out var cached))
            {
                return cached;
            }

            int bestIndex = 0;
            int bestDistance = int.MaxValue;

            for (int i = 0; i < palette.Length; i++)
            {
                int dr = palette[i].rgbRed - color.R;
                int dg = palette[i].rgbGreen - color.G;
                int db = palette[i].rgbBlue - color.B;

                int dist = dr * dr + dg * dg + db * db;
                if (dist < bestDistance)
                {
                    bestDistance = dist;
                    bestIndex = i;
                }
            }

            colorCache[colorKey] = bestIndex;
            return bestIndex;
        }

        private static byte[] RemapImageToPalette(FreeImageBitmap source, Palette sharedPalette)
        {
            int width = source.Width;
            int height = source.Height;
            byte[] remapped = new byte[width * height];

            // Allocate buffer to hold raw scanline
            byte[] scanlineBuffer = new byte[width];
            Palette palette = source.Palette;
            Dictionary<int, int> colorCache = new  Dictionary<int, int>();

            for (int y = 0; y < height; y++)
            {
                // Copy scanline into buffer
                IntPtr scanlinePtr = source.GetScanlinePointer(y);
                Marshal.Copy(scanlinePtr, scanlineBuffer, 0, scanlineBuffer.Length);

                for (int x = 0; x < width; x++)
                {
                    int pixelOffset = x;

                    int index = FindClosestPaletteIndex(palette[scanlineBuffer[pixelOffset]], sharedPalette, colorCache);
                    remapped[y * width + x] = (byte)index;
                }
            }

            return remapped;
        }

        /// <summary>
        /// Close file.
        /// </summary>
        public void Close()
        {
            binReader?.Close();
            fs?.Close();
        }


        /// <summary>
        /// Only change sprite type. This method doesn't check header.
        /// </summary>
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
        /// <param name="newPalette">Replacement palette.</param>
        /// <param name="outputFileName">Output file name.</param>
        public void ChangeColorPalette(ColorPalette newPalette, string outputFileName = null)
        {
            //Check if valid palette
            if (newPalette == null) return;

            //Save to original file
            if (outputFileName == null)
            {
                fs.Seek(0x2A, SeekOrigin.Begin); //Skip first

                foreach (var entry in newPalette.Entries)
                {
                    fs.WriteByte(entry.R);
                    fs.WriteByte(entry.G);
                    fs.WriteByte(entry.B);
                }

                fs.Flush();
            }
            else
            {
                //Save to other file than original
                File.Copy(Filename, outputFileName);
                using (var sw = new FileStream(outputFileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    sw.Seek(0x2A, SeekOrigin.Begin); //Skip first

                    foreach (var entry in newPalette.Entries)
                    {
                        sw.WriteByte(entry.R);
                        sw.WriteByte(entry.G);
                        sw.WriteByte(entry.B);
                    }
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