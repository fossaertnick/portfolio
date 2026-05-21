using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Core.Entities.Models;

namespace Mde.Project.Mobile.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        // fields
        private bool isBusy;
        private string errorMessage;
        private bool isLoading = true;

        // properties
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
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
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                SetProperty(ref isLoading, value);
            }
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
    }
}
