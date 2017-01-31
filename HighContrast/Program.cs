using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HighContrast
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                throw new Exception("exception source and target");
            var input = args[0];
            var output = args[1];

            if (!System.IO.File.Exists(input))
                throw new Exception("File doesn't exist " + input);

            var source = new Bitmap(Image.FromFile(input));
            var contrast = 0.2f;
            for(var x = 0; x < source.Width; x++)
                for(var y = 0; y < source.Height; y++)
                {
                    var color = source.GetPixel(x, y);
                    float currentContrast = (color.R + color.G + color.B) / 256.0f / 3.0f;
                    float cubic = 256.0f * currentContrast * currentContrast * currentContrast;
                    float square = 256.0f * currentContrast * currentContrast;

                    float v = (cubic * contrast) + (square * (1.0f - contrast));
                    int luminance = Convert.ToInt32(v);

                    source.SetPixel(x, y, Color.FromArgb(luminance, luminance, luminance));
                }
            source.Save(output);

        }
    }
}
