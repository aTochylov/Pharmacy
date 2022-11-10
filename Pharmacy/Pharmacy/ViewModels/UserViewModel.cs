using Pharmacy.Data;
using Pharmacy.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pharmacy.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private const string _deleteUser = "Delete user";
        private const string _addUser = "Register user";

        private UserRepository repository { get; }
        private string username;
        private string password;
        private string commandText = string.Empty;
        private bool isFrameVisible = false;
        private bool isAdmin = false;
        private bool isAdminVisible = true;

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public bool IsFrameVisible
        {
            get => isFrameVisible;
            set
            {
                if (value == isFrameVisible) return;
                isFrameVisible = value;
                OnPropertyChanged();
            }
        }
        public string CommandText
        {
            get => commandText;
            set => SetProperty(ref commandText, value);
        }
        public bool IsAdmin
        {
            get => isAdmin;
            set => SetProperty(ref isAdmin, value);
        }
        public bool IsAdminVisible
        {
            get => isAdminVisible;
            set
            {
                if (value == isAdminVisible) return;
                isAdminVisible = value;
                OnPropertyChanged();
            }
        }

        public Command DeleteCommand { get; }
        public Command AddCommand { get; }
        public Command SubmitCommand { get; set; }

        public UserViewModel()
        {
            repository = new UserRepository();
            DeleteCommand = new Command(TappedDelete);
            AddCommand = new Command(TappedAdd);
            SubmitCommand = new Command(Submit);
        }

        private void TappedAdd()
        {
            Username = Password = string.Empty;
            if (CommandText.Equals(_addUser) && IsFrameVisible)
                IsFrameVisible = false;
            else
            {
                CommandText = _addUser;
                IsFrameVisible = true;
                IsAdminVisible = true;
            }
        }

        private void TappedDelete()
        {
            IsAdminVisible = false;
            Username = Password = string.Empty;
            if (CommandText.Equals(_deleteUser) && IsFrameVisible)
                IsFrameVisible = false;
            else
            {
                CommandText = _deleteUser;
                IsFrameVisible = true;
            }
        }

        private void Submit()
        {
            if (!String.IsNullOrEmpty(Password) && Password.Length > 0 &&
            !String.IsNullOrEmpty(Username) && Username.Length > 0)
            {
                if (CommandText.Equals(_addUser))
                    Device.BeginInvokeOnMainThread(() => DependencyService.Get<IMessage>().ShortAlert(
                        Task.Run(async () => await repository.Register(new UserDto { Username = Username, Password = Password, IsAdmin = IsAdmin })).Result));
                if (CommandText.Equals(_deleteUser))
                    Device.BeginInvokeOnMainThread(() => DependencyService.Get<IMessage>().ShortAlert(
                        Task.Run(async () => await repository.Delete(new UserDto { Username = Username, Password = Password })).Result));
            }
        }
    }
}
