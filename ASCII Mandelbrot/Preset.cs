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

        public ZoomSettings ToZoomSettings()
        {
            IPalette palette = PalettePropertiesToPalette();

            return new ZoomSettings(InitialWidth, FinalWidth, CentrePosX, CentrePosY, MaxIterations, Duration, palette);
        }

        public IPalette PalettePropertiesToPalette()
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

            return palette;
        }
    }
}
