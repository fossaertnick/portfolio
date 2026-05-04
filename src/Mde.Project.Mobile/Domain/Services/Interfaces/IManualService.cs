using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IManualService
    {
        // methoden
        Task<string> HelpTheUserAsync();
    }
}
