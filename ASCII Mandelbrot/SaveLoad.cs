using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

namespace ASCII_Mandelbrot
{
    static class SaveLoad
    {
        public const string SavePath = "presets.json";
        public static List<Preset> PresetList { get; set; } = new List<Preset>();

        public static void SavePreset(Preset preset)
        {
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
            PresetList = (List<Preset>)JsonSerializer.Deserialize(Encoding.ASCII.GetBytes(File.ReadAllText(SavePath)), typeof(List<Preset>));

            foreach (Preset preset in PresetList)
            {
                if (preset.Name == name)
                {
                    return preset;
                }
            }

            return null;
        }
    }
}
