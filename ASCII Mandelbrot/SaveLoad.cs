using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using ASCII_Mandelbrot.ExtensionMethods;

namespace ASCII_Mandelbrot
{
    static class SaveLoad
    {
        private const string SavePath = "presets.json";
        public static List<Preset> PresetList { get; } = new List<Preset>();

        private static void SavePresetList()
        {
            FileStream stream = new FileStream(SavePath, FileMode.Create, FileAccess.ReadWrite);
            stream.Write(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(PresetList)));
            stream.Close();
        }

        public static bool SavePreset(Preset preset, bool overwrite = false)
        {
            RefreshPresetList();

            if (!PresetList.ContainsPreset(preset))
            {
                PresetList.Add(preset);
            }

            else if (overwrite)
            {
                PresetList[PresetList.IndexOfPreset(preset.Name)] = preset;
            }

            else
            {
                return false;
            }

            SavePresetList();
            return true;
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

        public static void RefreshPresetList()
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
