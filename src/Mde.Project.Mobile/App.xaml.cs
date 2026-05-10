using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Data.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Mde.Project.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}