using System;

namespace ASCII_Mandelbrot
{
    class Preset
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double InitialWidth { get; set; }
        public double FinalWidth { get; set; }
        public double CentrePosX { get; set; }
        public double CentrePosY { get; set; }
        public int MaxIterations { get; set; }
        public float Duration { get; set; }
        public PaletteType PaletteType { get; set; }
        public object[] PaletteProperties { get; set; }

        public Preset(string name, string description, double initialWidth, double finalWidth, double centrePosX, double centrePosY, int maxIterations, float duration, object[] paletteParams)
        {
            Name = name;
            Description = description;
            InitialWidth = initialWidth;
            FinalWidth = finalWidth;
            CentrePosX = centrePosX;
            CentrePosY = centrePosY;
            MaxIterations = maxIterations;
            Duration = duration;
            PaletteProperties = paletteParams;
        }

        public Preset(string name, string description, ZoomSettings zoomSettings)
        {
            Name = name;
            Description = description;
            InitialWidth = zoomSettings.InitialWidth;
            FinalWidth = zoomSettings.FinalWidth;
            CentrePosX = zoomSettings.CentrePosX;
            CentrePosY = zoomSettings.CentrePosY;
            MaxIterations = zoomSettings.MaxIterations;
            Duration = zoomSettings.Duration;
            PaletteType = zoomSettings.ZoomPalette.GetPaletteType();
            PaletteProperties = zoomSettings.ZoomPalette.PropertiesArray();
        }

        public Preset() { }

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

        public ZoomSettings ToZoomSettings()
        {
            IPalette palette;

            switch (PaletteType)
            {
                case PaletteType.NonLoopingPalette:
                    palette = new NonLoopingPalette(PaletteProperties);
                    break;
                case PaletteType.FractionalLoopingPalette:
                    palette = new FractionalLoopingPalette(PaletteProperties);
                    break;
                case PaletteType.NumericLoopingPalette:
                    palette = new NumericLoopingPalette(PaletteProperties);
                    break;
                default:
                    throw new Exception($"PaletteType not implemented in switch statement in Preset.ToZoomSettings() method.");
            }

            return new ZoomSettings(InitialWidth, FinalWidth, CentrePosX, CentrePosY, MaxIterations, Duration, palette);
        }
    }
}
