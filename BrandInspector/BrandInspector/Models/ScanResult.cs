namespace BrandInspector.Models
{
    public class ScanResult
    {
        public int SlideNumber { get; set; }
        public string ShapeType { get; set; }
        public string SampleText { get; set; }
        public string FontFamily { get; set; }
        public double FontSize { get; set; }
        public string Color { get; set; }
        public string Compliance { get; set; }
    }
}