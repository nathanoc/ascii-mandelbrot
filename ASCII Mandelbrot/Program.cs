using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ASCII_Mandelbrot
{
    class Program
    {
        static void Main(string[] args) // TODO: Abstract all of this out of main with separate functions
        {
            Console.WriteLine("(Fullscreen recommended)");
            Console.WriteLine("Enter initial frame width");
            decimal initialWidth = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter final frame width");
            decimal finalWidth = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter X coordinate to zoom to");
            decimal finalCentreX = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter Y coordinate to zoom to");
            decimal finalCentreY = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter maximum iteration count");
            int maxIterations = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter animation duration");
            float duration = float.Parse(Console.ReadLine());

            Stopwatch totalElapsedSW = new Stopwatch();
            Stopwatch deltaTimeSW = new Stopwatch();

            totalElapsedSW.Start();
            deltaTimeSW.Start();

            int framesPrinted = 0;
            bool finalFrameDrawn = false;
            while (totalElapsedSW.ElapsedMilliseconds < duration * 1000 || finalFrameDrawn == false)
            {
                deltaTimeSW.Restart();

                long functionalElapsedMilliseconds = totalElapsedSW.ElapsedMilliseconds;   // So that the final frame drawn is precisely as wanted
                if (totalElapsedSW.ElapsedMilliseconds > duration * 1000)
                {
                    functionalElapsedMilliseconds = (long)duration * 1000;
                    finalFrameDrawn = true;
                }

                List<char> frame = MandelbrotGenerator.GenerateMandelbrot(
                    width: 80,
                    height: 80,
                    xWidth: (decimal)((double)initialWidth * Math.Pow((double)(finalWidth / initialWidth), functionalElapsedMilliseconds / (float)1000 / duration)),
                    yWidth: (decimal)((double)initialWidth * Math.Pow((double)(finalWidth / initialWidth), functionalElapsedMilliseconds / (float)1000 / duration)),
                    centrePosX: finalCentreX,
                    centrePosY: finalCentreY,
                    iterations: maxIterations
                );

                string frameStr = "";
                foreach (char c in frame)
                {
                    if (c != '\n')
                    {
                        frameStr += c;
                        frameStr += c;  // Add twice to make up for character rectangularity
                    }

                    else
                    {
                        frameStr += c;
                    }
                }
                Console.SetCursorPosition(0, 0);
                Console.Write(frameStr);
                
                Console.WriteLine($"\n{Math.Round(1000f / deltaTimeSW.ElapsedMilliseconds)} FPS   "); // Maybe move this into a separate function i.e. DisplayFPS()
                
                framesPrinted++;
            }
            Console.WriteLine("Finished");
            Console.WriteLine("Press any key to exit");
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
                        pixels.Add('.');
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
