using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Security.AccessControl;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public partial class ManualViewModel : BaseViewModel
    {
        private readonly IDeviceSystemService _manualService;

        // fields
        private ObservableCollection<ManualSection> sections = new ObservableCollection<ManualSection>();

        // properties
        public ObservableCollection<ManualSection> Sections
        {
            get { return sections; }
            set
            {
                SetProperty(ref sections, value);
            }
        }

        // commands
        public ICommand InitializeCommand { get; }

        // constructor
        public ManualViewModel(IDeviceSystemService manualService)
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
                string markDown = await HandleResult(await _manualService.HelpTheUserAsync()) ?? string.Empty;

                ParseMarkdown(markDown);
            }
            finally
            {
                IsBusy = false;
            }
        }

        // ondersteunende methoden
        private void ParseMarkdown(string markdown)
        {
            Sections.Clear();
            string[] blocks = markdown.Split("## ", StringSplitOptions.RemoveEmptyEntries);
            foreach(string block in blocks)
            {
                string[] lines = block.Split('\n', 2);
                Sections.Add(new ManualSection
                {
                    Title = lines[0].Trim(),
                    Content = lines.Length > 1 ? lines[1].Trim() : string.Empty,
                });
            }
        }
    }
}
