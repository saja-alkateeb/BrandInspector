using System.Collections.Generic;

namespace BrandInspector.Api.Models
{
    public class BrandSettings
    {
        public List<string> Fonts { get; set; }
        public List<string> Colors { get; set; }
        public List<double> Sizes { get; set; }
    }
}
