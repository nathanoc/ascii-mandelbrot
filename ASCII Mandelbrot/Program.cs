using System;
using System.Collections.Generic;
using System.Timers;

namespace ASCII_Mandelbrot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter initial frame width");
            decimal initialWidth = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter final frame width");
            decimal finalWidth = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter animation duration");
            float duration = float.Parse(Console.ReadLine());

            List<char> frame = MandelbrotGenerator.GenerateMandelbrot(
                width: 80,
                height: 80,
                xWidth: 4,
                yWidth: 4,
                centrePosX: 0,
                centrePosY: 0,
                iterations: 500
            );

            string frameStr = "";
            foreach (char c in frame)
            {
                if (c != '\n')
                {
                    frameStr += c;
                    frameStr += c;
                }

                else
                {
                    frameStr += c;
                }
            }
            Console.Write(frameStr);
            Console.ReadKey();
        }
    }

    static class MandelbrotGenerator
    {
        private static string Brightnesses = " .'`^,:;Il!i><~+_-?][}{1)(|/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";

        public static List<char> GenerateMandelbrot(int width, int height, decimal xWidth, decimal yWidth, decimal centrePosX, decimal centrePosY, int iterations)
        {
            List<char> pixels = new List<char>();

            for (int yInt = (int)(height / 2M); yInt > (int)(-height / 2M); yInt--)
            {
                decimal y = centrePosY + (yInt / (decimal)height * yWidth);
                for (int xInt = (int)(-width / 2M); xInt < (int)(width / 2M); xInt++)
                {
                    decimal x = centrePosX + (xInt / (decimal)width * xWidth);

                    decimal Zx = 0;
                    decimal Zy = 0;
                    decimal Cx = x;
                    decimal Cy = y;

                    decimal Zx2 = 0;
                    decimal Zy2 = 0;

                    int finalIteration = 1;
                    for (int iteration = 1; iteration <= iterations; iteration++)
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
                        pixels.Add('@');
                    }

                    else
                    {
                        pixels.Add(Brightnesses[(int)Math.Floor(Math.Pow(finalIteration / ((float)iterations + 1), 0.25) * Brightnesses.Length)]);
                    }
                }

                pixels.Add('\n');
            }

            return pixels;
        }
    }
}
