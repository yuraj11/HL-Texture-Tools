using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HLTextureTools
{
    public struct DetailTexture
    {
        public string OriginalTextureName;
        public string DetailTexturePath;
        public float ScaleX;
        public float ScaleY;
    }

    class DetailTextureLoader
    {
        public DetailTextureLoader()
        {

        }

        public List<DetailTexture> Load(string inputFile)
        {
            string parentDir = Path.Combine(Directory.GetParent(inputFile).Parent.FullName, "gfx");
            List<DetailTexture> listOfDetails = new List<DetailTexture>();
            using (StreamReader sr = new StreamReader(inputFile, Encoding.ASCII))
            {
                string ln;

                int tokPos = 0;
                bool prevWS = false;
                while ((ln = sr.ReadLine()) != null)
                {
                    string trimmed = ln.Trim();

                    if (trimmed.Length <= 0 || trimmed.StartsWith("//"))
                        continue;

                    DetailTexture curText = new DetailTexture();
                    StringBuilder sb = new StringBuilder();
                    ln += " ";
                    for (int i = 0; i < ln.Length; i++)
                    {
                        if (!char.IsWhiteSpace(ln[i]))
                        {
                            sb.Append(ln[i]);
                            prevWS = false;
                        }
                        else
                        {
                            if (!prevWS)
                            {
                                switch (tokPos)
                                {
                                    case 0:
                                        curText.OriginalTextureName = sb.ToString();
                                        sb.Length = 0;
                                        tokPos++;
                                        break;
                                    case 1:
                                        sb.Replace('/', '\\');
                                        curText.DetailTexturePath = Path.Combine(parentDir, sb.ToString());

                                        if (!curText.DetailTexturePath.EndsWith(".tga"))
                                            curText.DetailTexturePath += ".tga";
                                        //if (!curText.DetailTexturePath.StartsWith("\\"))
                                        //    curText.DetailTexturePath = curText.DetailTexturePath.Insert(0, "\\");
                                        
                                        sb.Length = 0;
                                        tokPos++;
                                        break;
                                    case 2:
                                        curText.ScaleX = float.Parse(sb.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                                        sb.Length = 0;
                                        tokPos++;
                                        break;
                                    default:
                                        curText.ScaleY = float.Parse(sb.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                                        tokPos = 0;
                                        break;
                                }
                                
                            }

                            prevWS = true;
                        }
                    }
                    listOfDetails.Add(curText);
                }
            }

            return listOfDetails;
        }
    }
}
