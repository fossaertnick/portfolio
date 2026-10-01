using Mde.Project.Mobile.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Services.Interfaces
{
    public interface IAuthService
    {
        // methoden
        Task<ResultModel<string>> AuthenticateDeviceAsync();
        Task<string?> GetTokenAsync();
    }
}
