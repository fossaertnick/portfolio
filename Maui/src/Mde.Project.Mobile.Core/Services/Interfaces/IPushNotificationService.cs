using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Services.Interfaces
{
    public interface IPushNotificationService
    {
        // methoden (chatterbox to user)
        Task<bool> AreNotficationsEnabled();
        Task<bool> RequestPermission();
        Task InitializeNotifications();
        Task StopNotifications();
    }
}
