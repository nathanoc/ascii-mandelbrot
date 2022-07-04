using System;

namespace ASCII_Mandelbrot
{
    static class PresetMenu
    {
        public static Preset InputPreset()
        {
            Console.WriteLine("Enter preset name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter preset description:");
            string desc = Console.ReadLine();

            ZoomSettings settings = ZoomSettings.InputZoomSettings();

            return new Preset(name, desc, settings);
        }

        [Flags]
        public enum RevealedProperties
        {
            None = 0,
            Name = 1,
            Description = 2,
            InitialWidth = 4,
            FinalWidth = 8,
            CentrePosX = 16,
            CentrePosY = 32,
            MaxIterations = 64,
            Duration = 128,
            PaletteType = 256,
            PaletteProperties = 512
        }
        public static Preset SelectPreset(RevealedProperties revealedProperties = RevealedProperties.Name | RevealedProperties.Description | RevealedProperties.Duration)
        {
            SaveLoad.RefreshPresetList();

            Console.WriteLine("Preset list:\n");
            for (int i = 0; i < SaveLoad.PresetList.Count; i++)
            {
                Console.WriteLine($"{i + 1}.");
                OutputPresetDetails(SaveLoad.PresetList[i], revealedProperties);
                Console.WriteLine();
            }

            Console.WriteLine("Enter the number corresponding to your chosen preset. Enter 0 to make your own.");

            int selection = int.Parse(Console.ReadLine());

            if (selection == 0)
            {
                return InputPreset();
            }

            else
            {
                return SaveLoad.PresetList[selection - 1];
            }
        }

        private static void OutputPresetDetails(Preset preset,
            RevealedProperties revealedProperties = RevealedProperties.Name | RevealedProperties.Description | RevealedProperties.Duration,
            ConsoleColor nameHighlightColor = ConsoleColor.Gray)
        {
            if (revealedProperties.HasFlag(RevealedProperties.Name))
            {
                Console.Write("Name: ");
                ConsoleColor originalConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = nameHighlightColor;
                Console.WriteLine(preset.Name);
                Console.ForegroundColor = originalConsoleColor;
            }

            if (revealedProperties.HasFlag(RevealedProperties.Description))
            {
                Console.WriteLine($"Description: {preset.Description}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.InitialWidth))
            {
                Console.WriteLine($"Initial width: {preset.InitialWidth}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.FinalWidth))
            {
                Console.WriteLine($"Final width: {preset.FinalWidth}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.CentrePosX))
            {
                Console.WriteLine($"Centre X position: {preset.CentrePosX}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.CentrePosY))
            {
                Console.WriteLine($"Centre Y position: {preset.CentrePosY}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.MaxIterations))
            {
                Console.WriteLine($"Maximum iterations: {preset.MaxIterations}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.Duration))
            {
                Console.WriteLine($"Duration: {preset.Duration}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.PaletteType))
            {
                string paletteType = "";
                switch (preset.PaletteType)
                {
                    case PaletteType.NonLoopingPalette:
                        paletteType = "Non-Looping Palette";
                        break;
                    case PaletteType.FractionalLoopingPalette:
                        paletteType = "Fraction-based Looping Palette";
                        break;
                    case PaletteType.NumericLoopingPalette:
                        paletteType = "Number-based Looping Palette";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                Console.WriteLine($"Palette type: {paletteType}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.PaletteProperties))
            {
                throw new NotImplementedException();    // TODO: Implement this
            }
        }
    }
}
