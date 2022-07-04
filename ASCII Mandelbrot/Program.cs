using System;
using System.Collections.Generic;
using System.Diagnostics;
using ASCII_Mandelbrot.Tests;

namespace ASCII_Mandelbrot
{
    class Program
    {
        static void Main(string[] args) // TODO: Abstract all of this out of main with separate functions
        {
            TestMethods.RunTestsOnStart();

            Preset preset = PresetMenu.SelectPreset();  // TODO: Make this more user-friendly and add more options e.g. deleting presets
            ZoomSettings settings = preset.ToZoomSettings();

            Console.Clear();

            Stopwatch totalElapsedSW = new Stopwatch();
            Stopwatch deltaTimeSW = new Stopwatch();

            totalElapsedSW.Start();
            deltaTimeSW.Start();

            int framesPrinted = 0;
            bool finalFrameDrawn = false;
            while (totalElapsedSW.ElapsedMilliseconds < settings.Duration * 1000 || finalFrameDrawn == false)
            {
                deltaTimeSW.Restart();

                long functionalElapsedMilliseconds = totalElapsedSW.ElapsedMilliseconds;
                if (totalElapsedSW.ElapsedMilliseconds > settings.Duration * 1000)
                {
                    functionalElapsedMilliseconds = (long)settings.Duration * 1000; // So that the final frame drawn is precisely as wanted
                    finalFrameDrawn = true;
                }

                List<char> frame = MandelbrotGenerator.GenerateMandelbrot(
                    new MandelbrotSettings(
                        widthChars: 160,
                        heightChars: 80,
                        xWidth: settings.InitialWidth * Math.Pow(settings.FinalWidth / settings.InitialWidth, functionalElapsedMilliseconds / (float)1000 / settings.Duration),
                        yWidth: settings.InitialWidth * Math.Pow(settings.FinalWidth / settings.InitialWidth, functionalElapsedMilliseconds / (float)1000 / settings.Duration),
                        centrePosX: settings.CentrePosX,
                        centrePosY: settings.CentrePosY,
                        maxIterations: settings.MaxIterations,
                        palette: settings.ZoomPalette
                    )
                );

                string frameStr = "";
                foreach (char c in frame)
                {
                    frameStr += c;
                }
                Console.SetCursorPosition(0, 0);
                Console.Write(frameStr);
                
                Console.WriteLine($"\n{Math.Round(1000f / deltaTimeSW.ElapsedMilliseconds)} FPS   "); // Maybe move this into a separate function i.e. DisplayFPS()
                
                framesPrinted++;
            }

            Console.WriteLine("Finished");

            if (!SaveLoad.PresetList.Contains(preset))
            {
                Console.WriteLine("Would you like to save this configuration as a preset? Y/N");
                if (Console.ReadLine() == "Y")
                {
                    bool noOverwrite = SaveLoad.SavePreset(preset, overwrite: false);
                    if (!noOverwrite)
                    {
                        Console.WriteLine("A preset with this name already exists. Overwrite it? Y/N");
                        if (Console.ReadLine() == "Y")
                        {
                            SaveLoad.SavePreset(preset, overwrite: true);
                            Console.WriteLine("Saved.");
                        }
                    }

                    else
                    {
                        Console.WriteLine("Saved.");
                    }
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
