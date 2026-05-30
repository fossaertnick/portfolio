using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services;

namespace Mde.Project.Mobile.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        // fields
        private bool isBusy;
        private string errorMessage;
        private bool hasInternet;

        // properties
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if(SetProperty(ref isBusy, value))
                {
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                SetProperty(ref errorMessage, value);
            }
        }
        public bool HasInternet
        {
            get { return  hasInternet; }
            set
            {
                if(SetProperty(ref hasInternet, value))
                {
                    OnPropertyChanged(nameof(IsOffline));
                }
            }
        }
        public bool IsLoading => !IsBusy;
        public bool IsOffline => !HasInternet;

        // constructor
        public BaseViewModel()
        {
            UpdateConnectivity();
            Connectivity.Current.ConnectivityChanged += async (_, e) =>
            {
                var previous = HasInternet;
                HasInternet = e.NetworkAccess == NetworkAccess.Internet;
                if (!previous && HasInternet) await OnInternetRestored();
            };
        }
        // methoden
        protected async Task<T?> HandleResult<T>(ResultModel<T> result)
        {
            if (result.IsSucces) { ErrorMessage = string.Empty; return result.Data; }

            ErrorMessage = result.UserMessage ?? "Unknown error.";
            foreach (var error in result.Errors) System.Diagnostics.Debug.WriteLine(error);
            if (!string.IsNullOrWhiteSpace(result.UserMessage))
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    result.UserMessage,
                    "OK");
            }

            return default;
        }
        private void UpdateConnectivity()
        {
            HasInternet = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }
        protected virtual Task OnInternetRestored()
        {
            return Task.CompletedTask;
        }
    }
}
