using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class StatisticsViewModel : BaseViewModel
    {
        private readonly IStatisticService _statisticService;

        // fields
        private string totalMemorias;
        private string werkMemoria;
        private string reisMemoria;
        private string uitgaanMemoria;
        private string andereMemoria;
        private string totalMemoriaFotos;
        private string totalMemoriaVideos;
        private string favoriteMemoriaCountry;

        // properties
        public string TotalMemorias
        {
            get { return totalMemorias; }
            set
            {
                SetProperty(ref totalMemorias, value);
            }
        }
        public string WerkMemoria
        {
            get { return werkMemoria; }
            set
            {
                SetProperty(ref werkMemoria, value);
            }
        }
        public string ReisMemoria
        {
            get { return reisMemoria; }
            set
            {
                SetProperty(ref reisMemoria, value);
            }
        }
        public string UitgaanMemoria
        {
            get { return uitgaanMemoria; }
            set
            {
                SetProperty(ref uitgaanMemoria, value);
            }
        }
        public string AndereMemoria
        {
            get { return andereMemoria; }
            set
            {
                SetProperty(ref andereMemoria, value);
            }
        }
        public string TotalMemoriaFotos
        {
            get { return totalMemoriaFotos; }
            set
            {
                SetProperty(ref totalMemoriaFotos, value);
            }
        }
        public string TotalMemoriaVideos
        {
            get { return totalMemoriaVideos; }
            set
            {
                SetProperty(ref totalMemoriaVideos, value);
            }
        }
        public string FavoriteMemoriaCountry
        {
            get { return favoriteMemoriaCountry; }
            set
            {
                SetProperty(ref favoriteMemoriaCountry, value);
            }
        }

        // commands
        public ICommand InitializeCommand => new Command (async () =>
        {
            await ExecuteInitializeCommand();
        });

        // constructor
        public StatisticsViewModel(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        // methoden
        private async Task ExecuteInitializeCommand()
        {
            try
            {
                IsBusy = true;

                var result = await HandleResult(await _statisticService.GetStatisticsAsync());
                if (result == null) return;

                TotalMemorias = result.TotalMemorias;
                WerkMemoria = result.WerkMemoria;
                ReisMemoria = result.ReisMemoria;
                UitgaanMemoria = result.UitgaanMemoria;
                AndereMemoria = result.AndereMemoria;
                TotalMemoriaFotos = result.TotalMemoriaFotos;
                TotalMemoriaVideos = result.TotalMemoriaVideo;
                FavoriteMemoriaCountry = result.FavoriteMemoriaCountry;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
