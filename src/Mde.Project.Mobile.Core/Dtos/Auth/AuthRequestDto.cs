using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Dtos.Auth
{
    public class AuthRequestDto
    {
        public string DeviceIdentifier { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
    }
}
