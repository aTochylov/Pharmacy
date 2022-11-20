using Pharmacy.Data;
using Pharmacy.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pharmacy.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private UserRepository repository { get; }
        private string username;
        private string password;
        private bool isAdmin = false;
        private string newPassword;

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
        public string NewPassword
        {
            get => newPassword;
            set => SetProperty(ref newPassword, value);
        }
        public bool IsAdmin
        {
            get => isAdmin;
            set => SetProperty(ref isAdmin, value);
        }

        public Command DeleteCommand { get; }
        public Command AddCommand { get; }
        public Command SubmitCommand { get; }
        public Command ChangeCommand { get; }

        public UserViewModel()
        {
            repository = new UserRepository();
            DeleteCommand = new Command(()=>
            {
                if (!String.IsNullOrEmpty(Password) && Password.Length > 0 &&
                !String.IsNullOrEmpty(Username) && Username.Length > 0)
                    Device.BeginInvokeOnMainThread(() => DependencyService.Get<IMessage>().ShortAlert(
                    Task.Run(async () => await repository.Delete(new UserDto { Username = Username, Password = Password })).Result));
            });
            AddCommand = new Command(()=> {
                if (!String.IsNullOrEmpty(Password) && Password.Length > 0 &&
                !String.IsNullOrEmpty(Username) && Username.Length > 0)
                    Device.BeginInvokeOnMainThread(() => DependencyService.Get<IMessage>().ShortAlert(
                    Task.Run(async () => await repository.Register(new UserDto { Username = Username, Password = Password, IsAdmin = IsAdmin })).Result));
            });
            ChangeCommand = new Command(() =>
            {
                if (!String.IsNullOrEmpty(Password) && Password.Length > 0 &&
                !String.IsNullOrEmpty(Username) && Username.Length > 0 &&
                !String.IsNullOrEmpty(NewPassword) && NewPassword.Length > 0)
                    Device.BeginInvokeOnMainThread(() => DependencyService.Get<IMessage>().ShortAlert(
                    Task.Run(async () => await repository.ChangePassword(new UserDto { Username = Username, Password = Password, IsAdmin = IsAdmin }, NewPassword)).Result));
            });
        }
    }
}
