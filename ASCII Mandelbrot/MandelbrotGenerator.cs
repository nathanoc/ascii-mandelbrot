using System;
using System.Collections.Generic;

namespace ASCII_Mandelbrot
{
    static class MandelbrotGenerator
    {
        private static string Brightnesses = " .'`^,:;Il!i><~+_-?][}{1)(|/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";

        public static List<char> GenerateMandelbrot(MandelbrotSettings settings)
        {
            List<char> pixels = new List<char>();

            for (int yInt = (int)(settings.heightChars / 2M); yInt > (int)(-settings.heightChars / 2M); yInt--)
            {
                decimal y = settings.centrePosY + (yInt / (decimal)settings.heightChars * settings.yWidth);
                for (int xInt = (int)(-settings.widthChars / 2M); xInt < (int)(settings.widthChars / 2M); xInt++)
                {
                    decimal x = settings.centrePosX + (xInt / (decimal)settings.widthChars * settings.xWidth);

                    decimal Zx = 0;
                    decimal Zy = 0;
                    decimal Cx = x;
                    decimal Cy = y;

                    decimal Zx2 = 0;
                    decimal Zy2 = 0;

                    int finalIteration = 1;
                    for (int iteration = 1; iteration <= settings.maxIterations; iteration++)
                    {
                        Zx2 = Zx * Zx;
                        Zy2 = Zy * Zy;

                        decimal savedZx = Zx;

                        Zx = Zx2 - Zy2 + Cx;
                        Zy = 2 * savedZx * Zy + Cy;

                        Zx2 = Zx * Zx;
                        Zy2 = Zy * Zy;

                        finalIteration = iteration;
                        if (Zx2 + Zy2 > 4)
                        {
                            break;
                        }
                    }

                    if (Zx2 + Zy2 < 4)
                    {
                        pixels.Add('.');
                    }

                    else
                    {
                        pixels.Add(Brightnesses[(int)Math.Floor(Math.Pow(finalIteration / ((float)settings.maxIterations + 1), 0.25) * Brightnesses.Length)]);
                    }
                }

                pixels.Add('\n');
            }

            return pixels;
        }
    }
}
