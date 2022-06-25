using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

namespace ASCII_Mandelbrot
{
    static class SaveLoad
    {
        public const string SavePath = "presets.json";
        public static List<Preset> PresetList { get; } = new List<Preset>();

        public static Preset SelectPreset()
        {
            RefreshPresetList();

            Console.WriteLine("Preset list:\n");
            for (int i = 0; i < PresetList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {PresetList[i].Name}");
                Console.WriteLine($"\tDescription: {PresetList[i].Description}");
                Console.WriteLine($"\tDuration: {PresetList[i].Duration}");
                Console.WriteLine();
            }

            Console.WriteLine("Enter the number corresponding to your chosen preset. Enter 0 to make your own.");

            int selection = int.Parse(Console.ReadLine());

            if (selection == 0)
            {
                return Preset.InputPreset();
            }

            else
            {
                return PresetList[selection - 1];
            }
        }

        public static void SavePreset(Preset preset)
        {
            RefreshPresetList();

            if (!PresetList.Contains(preset))
            {
                PresetList.Add(preset);
            }

            FileStream stream = new FileStream(SavePath, FileMode.Create, FileAccess.ReadWrite);
            stream.Write(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(PresetList)));
            stream.Close();
        }

        public static Preset LoadPreset(string name)
        {
            RefreshPresetList();

            foreach (Preset preset in PresetList)
            {
                if (preset.Name == name)
                {
                    return preset;
                }
            }

            return null;
        }

        private static void RefreshPresetList()
        {
            PresetList.Clear();
            List<Preset> deserialized = (List<Preset>)JsonSerializer.Deserialize(Encoding.ASCII.GetBytes(File.ReadAllText(SavePath)), typeof(List<Preset>));

            for (int i = 0; i < deserialized.Count; i++)
            {
                JsonElement element;
                switch (deserialized[i].PaletteType)
                {
                    case PaletteType.NonLoopingPalette:
                        element = (JsonElement)deserialized[i].PaletteProperties[0];
                        deserialized[i].PaletteProperties[0] = JsonSerializer.Deserialize<string>(element.GetRawText());
                        element = (JsonElement)deserialized[i].PaletteProperties[1];
                        deserialized[i].PaletteProperties[1] = JsonSerializer.Deserialize<float>(element.GetRawText());
                        element = (JsonElement)deserialized[i].PaletteProperties[2];
                        deserialized[i].PaletteProperties[2] = JsonSerializer.Deserialize<char>(element.GetRawText());
                        break;
                    case PaletteType.FractionalLoopingPalette:
                        element = (JsonElement)deserialized[i].PaletteProperties[0];
                        deserialized[i].PaletteProperties[0] = JsonSerializer.Deserialize<string>(element.GetRawText());
                        element = (JsonElement)deserialized[i].PaletteProperties[1];
                        deserialized[i].PaletteProperties[1] = JsonSerializer.Deserialize<float>(element.GetRawText());
                        element = (JsonElement)deserialized[i].PaletteProperties[2];
                        deserialized[i].PaletteProperties[2] = JsonSerializer.Deserialize<char>(element.GetRawText());
                        break;
                    case PaletteType.NumericLoopingPalette:
                        element = (JsonElement)deserialized[i].PaletteProperties[0];
                        deserialized[i].PaletteProperties[0] = JsonSerializer.Deserialize<string>(element.GetRawText());
                        element = (JsonElement)deserialized[i].PaletteProperties[1];
                        deserialized[i].PaletteProperties[1] = JsonSerializer.Deserialize<int>(element.GetRawText());
                        element = (JsonElement)deserialized[i].PaletteProperties[2];
                        deserialized[i].PaletteProperties[2] = JsonSerializer.Deserialize<char>(element.GetRawText());
                        break;
                    default:
                        throw new ArgumentException("PaletteType either non-existent or not yet implemented into switch statement.");
                }
            }

            PresetList.AddRange(deserialized);
        }
    }
}
