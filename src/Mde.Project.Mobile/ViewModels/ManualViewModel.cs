using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class ManualViewModel : BaseViewModel
    {
        private readonly IManualService _manualService;

        // fields
        private string text;

        // properties
        public string Text
        {
            get { return text; }
            set
            {
                SetProperty(ref text, value);
            }
        }

        // commands
        public ICommand InitializeCommand { get; }

        // constructor
        public ManualViewModel(IManualService manualService)
        {
            _manualService = manualService;
            InitializeCommand = new Command(async () => await LoadManual());
        }

        // methoden
        private async Task LoadManual()
        {
            try
            {
                IsBusy = true;
                Text = await HandleResult(await _manualService.HelpTheUserAsync()) ?? string.Empty;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
