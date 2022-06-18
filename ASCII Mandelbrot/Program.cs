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
            ZoomSettings settings = ZoomSettings.inputZoomSettings();

            Console.Clear();

            Stopwatch totalElapsedSW = new Stopwatch();
            Stopwatch deltaTimeSW = new Stopwatch();

            totalElapsedSW.Start();
            deltaTimeSW.Start();

            int framesPrinted = 0;
            bool finalFrameDrawn = false;
            while (totalElapsedSW.ElapsedMilliseconds < settings.duration * 1000 || finalFrameDrawn == false)
            {
                deltaTimeSW.Restart();

                long functionalElapsedMilliseconds = totalElapsedSW.ElapsedMilliseconds;
                if (totalElapsedSW.ElapsedMilliseconds > settings.duration * 1000)
                {
                    functionalElapsedMilliseconds = (long)settings.duration * 1000; // So that the final frame drawn is precisely as wanted
                    finalFrameDrawn = true;
                }

                List<char> frame = MandelbrotGenerator.GenerateMandelbrot(
                    new MandelbrotSettings(
                        widthChars: 80,
                        heightChars: 80,
                        xWidth: settings.initialWidth * Math.Pow(settings.finalWidth / settings.initialWidth, functionalElapsedMilliseconds / (float)1000 / settings.duration),
                        yWidth: settings.initialWidth * Math.Pow(settings.finalWidth / settings.initialWidth, functionalElapsedMilliseconds / (float)1000 / settings.duration),
                        centrePosX: settings.centrePosX,
                        centrePosY: settings.centrePosY,
                        maxIterations: settings.maxIterations,
                        palette: new NumericLoopingPalette(
                                brightnesses: "'`^,:;Il!i><~+_-?][}{1)(|/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$",
                                loopLength: 125,
                                inMandelbrotChar: '.'
                            )
                    )
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
}
