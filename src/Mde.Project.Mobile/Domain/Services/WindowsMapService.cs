using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;

namespace Mde.Project.Mobile.Domain.Services
{
    public class WindowsMapService : IMapService
    {
        private WebView _webView;

        public event Action<Guid> OnPinClicked;

        // methoden
        public async Task<ResultModel<bool>> ClearPins()
        {
            try
            {
                if (_webView == null) return ResultModel<bool>.Failure("WebView was null", "Map was not initialized.");
                await _webView.EvaluateJavaScriptAsync("clearMarkers()");

                return ResultModel<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong with the deleting of the markers.");
            }
        }
        public async Task<ResultModel<bool>> Initialize(object mapControl)
        {
            try
            {
                if (mapControl is not WebView webview) return ResultModel<bool>.Failure("map control was not a webView", "Map could not be initialized.");
                _webView = (WebView)mapControl;

                var html = @"
                <html>
                <head>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <link rel='stylesheet' href='https://unpkg.com/leaflet/dist/leaflet.css' />
                    <script src='https://unpkg.com/leaflet/dist/leaflet.js'></script>
                    <style>
                        #map {
                            height: 100%;
                            width: 100%;
                        }
                    </style>
                </head>
                <body>
                    <div id='map'></div>
                    <script>
                        var map = L.map('map').setView([0, 0], 2);
                        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                            attribution: '© OpenStreetMap'
                        }).addTo(map);
                        
                        window.setCenter = function(lat, lng) {
                            map.setView([lat, lng], 13);
                        };

                        window.AddMarker = function(lat, lng, title) {
                            L.marker([lat, lng]).addTo(map).bindPopup(title);
                        };
                    </script>
                </body>
                </html>";
                _webView.Source = new HtmlWebViewSource { Html = html };

                return ResultModel<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong with the initializing of the map.");
            }


        }
        public async Task<ResultModel<bool>> MoveTo(Location location)
        {
            try
            {
                if (_webView == null) return ResultModel<bool>.Failure("WebView was null", "Map was not initialized.");
                var js = $"setLocation({location.Latitude}, {location.Longitude})";
                await _webView?.EvaluateJavaScriptAsync(js);
                
                return ResultModel<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong with the moving of the map.");
            }
        }
        public async Task<ResultModel<bool>> SetPins(IEnumerable<MemoriaList> items)
        {
            try
            {
                if (_webView == null) return ResultModel<bool>.Failure("WebView was null", "Map was not initialized.");
                if (items == null) return ResultModel<bool>.Failure("Items collection was null", "No locations received.");

                await _webView.EvaluateJavaScriptAsync("clearMarkers()");
                foreach (var pin in items)
                {
                    var safeTitle = pin.Name.Replace("'", "\\");
                    var js = $"addMarker({pin.Latitude}, {pin.Longitude}, '{pin.Name}')";
                    await _webView.EvaluateJavaScriptAsync(js);
                }

                return ResultModel<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong while putting the markers.");
            }
        }
    }
}
