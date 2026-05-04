using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Models.enums;
using Mde.Project.Mobile.Domain.Services;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class StatisticsViewModel : ObservableObject
    {
        private readonly IStatisticService _statisticService; 

        // fields
        private int totalMemorias;
        private int werkMemoria;
        private int reisMemoria;
        private int uitgaanMemoria;
        private int andereMemoria;
        private int totalMemoriaFotos;
        private int totalMemoriaVideos;
        private string favoriteMemoriaCity;

        // properties
        public int TotalMemorias
        {
            get { return totalMemorias; }
            set
            {
                SetProperty(ref totalMemorias, value);
            }
        }
        public int WerkMemoria
        {
            get { return werkMemoria; }
            set
            {
                SetProperty(ref werkMemoria, value);
            }
        }
        public int ReisMemoria
        {
            get { return reisMemoria; }
            set
            {
                SetProperty(ref reisMemoria, value);
            }
        }
        public int UitgaanMemoria
        {
            get { return uitgaanMemoria; }
            set
            {
                SetProperty(ref uitgaanMemoria, value);
            }
        }
        public int AndereMemoria
        {
            get { return andereMemoria; }
            set
            {
                SetProperty(ref andereMemoria, value);
            }
        }
        public int TotalMemoriaFotos
        {
            get { return totalMemoriaFotos; }
            set
            {
                SetProperty(ref totalMemoriaFotos, value);
            }
        }
        public int TotalMemoriaVideos
        {
            get { return totalMemoriaVideos; }
            set
            {
                SetProperty(ref totalMemoriaVideos, value);
            }
        }
        public string FavoriteMemoriaCity
        {
            get { return favoriteMemoriaCity; }
            set
            {
                SetProperty(ref favoriteMemoriaCity, value);
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
            TotalMemorias =  _statisticService.GetTotalMemorias();
            WerkMemoria =  _statisticService.GetMemoriasByOccasionAsync(OccationType.Werk);
            ReisMemoria =  _statisticService.GetMemoriasByOccasionAsync(OccationType.Reis);
            UitgaanMemoria =  _statisticService.GetMemoriasByOccasionAsync(OccationType.Uitgaan);
            AndereMemoria =  _statisticService.GetMemoriasByOccasionAsync(OccationType.Ander);
            TotalMemoriaFotos =  _statisticService.GetPhotoCountAsync();
            TotalMemoriaVideos =  _statisticService.GetVideoCountAsync();
            FavoriteMemoriaCity =  _statisticService.GetFavoriteCountryAsync();
        }
    }
}
