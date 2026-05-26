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
