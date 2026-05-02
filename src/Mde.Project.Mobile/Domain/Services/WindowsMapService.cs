using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services
{
    public class WindowsMapService : IMapService
    {
        private WebView _webView;

        public event Action<Guid> OnPinClicked;

        // methoden
        public void ClearPins()
        {
            _webView.EvaluateJavaScriptAsync("clearMarkers()");
        }
        public void Initialize(object mapControl)
        {
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
        }
        public void MoveTo(Location location)
        {
            var js = $"setLocation({location.Latitude}, {location.Longitude})";
            _webView?.EvaluateJavaScriptAsync(js);
        }
        public void SetPins(IEnumerable<Memoria> items)
        {
            foreach(var pin in items)
            {
                var js = $"addMarker({pin.Latitude}, {pin.Longitude}, '{pin.Name}')";
                _webView.EvaluateJavaScriptAsync(js);
            }
        }
    }
}
