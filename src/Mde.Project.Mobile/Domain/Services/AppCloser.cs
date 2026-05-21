#if ANDROID
using Android.OS;
#endif

namespace Mde.Project.Mobile.Domain.Services
{
    public class AppCloser 
    {
        public void Close()
        {

#if ANDROID

            Process.KillProcess(Process.MyPid());

#elif WINDOWS

            Application.Current?.Quit();

#endif
        }
    }
}
