using System;
using System.Collections.Generic;

namespace ASCII_Mandelbrot
{
    static class MandelbrotGenerator
    {
        public static List<char> GenerateMandelbrot(MandelbrotSettings settings)
        {
            List<char> pixels = new List<char>();

            for (int yInt = (int)(settings.heightChars / 2M); yInt > (int)(-settings.heightChars / 2M); yInt--)
            {
                double y = settings.centrePosY + (yInt / (double)settings.heightChars * settings.yWidth);
                for (int xInt = (int)(-settings.widthChars / 2M); xInt < (int)(settings.widthChars / 2M); xInt++)
                {
                    double x = settings.centrePosX + (xInt / (double)settings.widthChars * settings.xWidth);

                    double Zx = 0;
                    double Zy = 0;
                    double Cx = x;
                    double Cy = y;

                    double Zx2 = 0;
                    double Zy2 = 0;

                    int finalIteration = 1;
                    for (int iteration = 1; iteration <= settings.maxIterations; iteration++)
                    {
                        Zx2 = Zx * Zx;
                        Zy2 = Zy * Zy;

                        double savedZx = Zx;

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
                        pixels.Add(settings.palette.InMandelbrotChar);
                    }

                    else
                    {
                        pixels.Add(settings.palette.GetShadeChar(finalIteration, settings.maxIterations));
                    }
                }

                pixels.Add('\n');
            }

            return pixels;
        }
    }
}
