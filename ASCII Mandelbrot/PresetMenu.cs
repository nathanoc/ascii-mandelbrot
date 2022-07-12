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
            PaletteProperties = 512,

            All = 1023
        }
        public static Preset SelectPreset(RevealedProperties revealedProperties = RevealedProperties.Name | RevealedProperties.Description | RevealedProperties.Duration, bool showMoreOptions = true)
        {
            SaveLoad.RefreshPresetList();

            Console.WriteLine("Preset list:\n");
            for (int i = 0; i < SaveLoad.PresetList.Count; i++)
            {
                Console.WriteLine($"{i + 1}."); // TODO: Make this prettier
                OutputPresetDetails(SaveLoad.PresetList[i], revealedProperties);
                Console.WriteLine();
            }

            Console.WriteLine("Enter the number corresponding to your chosen preset.");

            if (showMoreOptions)
            {
                Console.WriteLine("Enter 0 for more options (editing / deleting presets)");
            }

            int selection = int.Parse(Console.ReadLine());

            if (selection == 0 && showMoreOptions)
            {
                Console.WriteLine("1. Edit a preset");
                Console.WriteLine("2. Create a preset");

                int actionSelection = int.Parse(Console.ReadLine());
                switch (actionSelection)
                {
                    case 1:
                        Console.WriteLine("Select a preset to edit.");
                        EditPreset(SelectPreset(showMoreOptions: false));
                        return SelectPreset(revealedProperties);
                    case 2:
                        return InputPreset();
                    default:
                        throw new NotImplementedException();
                }
            }

            else
            {
                return SaveLoad.PresetList[selection - 1];
            }
        }

        public static Preset EditPreset(Preset preset, RevealedProperties revealedProperties = RevealedProperties.All)
        {
            bool exit = false;
            while (!exit)
            {
                OutputPresetDetails(preset, revealedProperties, numbered: true);
                Console.WriteLine("Enter the number corresponding to the property you would like to edit, or 0 to exit.");
                int selectedProperty = int.Parse(Console.ReadLine());   // TODO: Validate

                switch (selectedProperty)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        Console.WriteLine("Enter new preset name.");
                        preset.Name = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter new preset description.");
                        preset.Description = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Enter new initial width.");
                        preset.InitialWidth = double.Parse(Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("Enter new final width.");
                        preset.FinalWidth = double.Parse(Console.ReadLine());
                        break;
                    case 5:
                        Console.WriteLine("Enter new centre X position.");
                        preset.CentrePosX = double.Parse(Console.ReadLine());
                        break;
                    case 6:
                        Console.WriteLine("Enter new centre Y position.");
                        preset.CentrePosY = double.Parse(Console.ReadLine());
                        break;
                    case 7:
                        Console.WriteLine("Enter new maximum iterations.");
                        preset.MaxIterations = int.Parse(Console.ReadLine());
                        break;
                    case 8:
                        Console.WriteLine("Enter new zoom duration (seconds).");
                        preset.Duration = float.Parse(Console.ReadLine());
                        break;
                    case 9: // PaletteType
                        throw new NotImplementedException("Editing palette type is not yet implemented.");
                    case 10: // PaletteProperties[0]
                        throw new NotImplementedException("Editing palette properties is not yet implemented.");
                    case 11: // PaletteProperties[1]
                        throw new NotImplementedException("Editing palette properties is not yet implemented.");
                    case 12: // PaletteProperties[2]
                        throw new NotImplementedException("Editing palette properties is not yet implemented.");
                    default:
                        throw new NotImplementedException();
                }
            }

            return preset;
        }

        private static void OutputPresetDetails(
            Preset preset,
            RevealedProperties revealedProperties = RevealedProperties.Name | RevealedProperties.Description | RevealedProperties.Duration,
            ConsoleColor nameHighlightColor = ConsoleColor.Gray,
            bool numbered = false)
        {
            int number = 0;
            string numString;

            void SetNumString()
            {
                number++;

                if (numbered)
                {
                    numString = number.ToString();
                }

                else
                {
                    numString = "";
                }
            }

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
                SetNumString();
                Console.Write(numString);
                Console.WriteLine($"Description: {preset.Description}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.InitialWidth))
            {
                SetNumString();
                Console.Write(numString);
                Console.WriteLine($"Initial width: {preset.InitialWidth}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.FinalWidth))
            {
                SetNumString();
                Console.Write(numString);
                Console.WriteLine($"Final width: {preset.FinalWidth}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.CentrePosX))
            {
                SetNumString();
                Console.Write(numString);
                Console.WriteLine($"Centre X position: {preset.CentrePosX}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.CentrePosY))
            {
                SetNumString();
                Console.Write(numString);
                Console.WriteLine($"Centre Y position: {preset.CentrePosY}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.MaxIterations))
            {
                SetNumString();
                Console.Write(numString);
                Console.WriteLine($"Maximum iterations: {preset.MaxIterations}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.Duration))
            {
                SetNumString();
                Console.Write(numString);
                Console.WriteLine($"Duration: {preset.Duration}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.PaletteType))
            {
                string paletteType;
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

                SetNumString();
                Console.Write(numString);
                Console.WriteLine($"Palette type: {paletteType}");
            }

            if (revealedProperties.HasFlag(RevealedProperties.PaletteProperties))
            {
                throw new NotImplementedException();    // TODO: Implement this
            }
        }
    }
}
