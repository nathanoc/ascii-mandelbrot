using System;
using System.Collections.Generic;

namespace ASCII_Mandelbrot.ExtensionMethods
{
    static class PresetListExtensions
    {
        public static bool ContainsPreset(this List<Preset> presetList, string name)
        {
            foreach (Preset preset in presetList)
            {
                if (preset.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsPreset(this List<Preset> presetList, Preset preset) => ContainsPreset(presetList, preset.Name);

        public static int IndexOfPreset(this List<Preset> presetList, string name)
        {
            for (int i = 0; i < presetList.Count; i++)
            {
                if (presetList[i].Name == name)
                {
                    return i;
                }
            }

            throw new ArgumentOutOfRangeException("name");
        }

        public static int IndexOfPreset(this List<Preset> presetList, Preset preset) => IndexOfPreset(presetList, preset.Name);
    }
}
