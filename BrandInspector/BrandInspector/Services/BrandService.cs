using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrandInspector.Models;

namespace BrandInspector.Services
{
    public class BrandService
    {
        private readonly ApiService _apiService;
        private BrandSettings _cachedSettings;
        private string _token;

        public BrandService(ApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        }

        public void SetToken(string token)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public async Task<BrandSettings> GetBrandSettingsAsync(bool forceRefresh = false)
        {
            if (_cachedSettings != null && !forceRefresh)
                return _cachedSettings;

            if (string.IsNullOrEmpty(_token))
                throw new InvalidOperationException("JWT token not set. Please authenticate first.");

            var fonts = await _apiService.GetAuthorizedAsync<List<string>>("/brand/fonts", _token);
            var colors = await _apiService.GetAuthorizedAsync<List<string>>("/brand/colors", _token);
            var sizes = await _apiService.GetAuthorizedAsync<List<double>>("/brand/sizes", _token);

            _cachedSettings = new BrandSettings
            {
                Fonts = fonts,
                Colors = NormalizeColors(colors),
                Sizes = sizes
            };

            return _cachedSettings;
        }

        private List<string> NormalizeColors(List<string> colors)
        {
            var normalized = new List<string>();
            foreach (var color in colors)
            {
                if (string.IsNullOrWhiteSpace(color)) continue;

                var val = color.Trim().ToUpper();
                if (!val.StartsWith("#"))
                    val = "#" + val;
                normalized.Add(val);
            }
            return normalized;
        }

        public void ClearCache()
        {
            _cachedSettings = null;
        }
    }
}
