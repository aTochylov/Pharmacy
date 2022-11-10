using Pharmacy.Data;
using Pharmacy.Models;
using Pharmacy.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pharmacy.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private UserRepository repository { get; set; }
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            repository = new UserRepository();
        }
        public void OnSubmit()
        {
            var jwt = Task.Run(async() => await repository.Login(new UserDto { Username = username, Password = password })).Result;
            if (jwt is null)
            {
                DisplayInvalidLoginPrompt();
            }
            else
            {
                App.Current.Properties.Remove("token");
                App.Current.Properties.Add("token", jwt);
                App.Current.MainPage = new AppShell();
            }
        }
    }
}
