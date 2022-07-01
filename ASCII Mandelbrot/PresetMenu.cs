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

        public static Preset SelectPreset()
        {
            SaveLoad.RefreshPresetList();

            Console.WriteLine("Preset list:\n");
            for (int i = 0; i < SaveLoad.PresetList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {SaveLoad.PresetList[i].Name}");
                Console.WriteLine($"\tDescription: {SaveLoad.PresetList[i].Description}");
                Console.WriteLine($"\tDuration: {SaveLoad.PresetList[i].Duration}");
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
    }
}
