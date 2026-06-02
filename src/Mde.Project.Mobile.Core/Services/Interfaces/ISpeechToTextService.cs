using Mde.Project.Mobile.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Services.Interfaces
{
    public interface ISpeechToTextService
    {
        Task StartListening(Action<string> onResult);
        void StopListening();
    }
}
