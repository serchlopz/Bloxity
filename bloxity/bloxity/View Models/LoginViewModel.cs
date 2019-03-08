

namespace bloxity.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Views;

    public class LoginViewModel : BaseViewModel
    {
        #region Atributos
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Propiedades
        public String Email
        {
            get
            {
                return this.email;
            }

            set
            {
                setValue(ref this.email, value);
            }
        }
        public String Password
        {
            get
            {
                return this.password;
            }

            set
            {
                setValue(ref this.password, value);
            }
        }
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }

            set
            {
                setValue(ref this.isRunning, value);
            }
        }
        public bool IsRemember
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }

            set
            {
                setValue(ref this.isEnabled, value);
            }
        }

        #endregion

        #region Constructores
        public LoginViewModel()
        {
            this.IsRemember = false;
            this.isEnabled = true;
            this.email = "slopez@rfacil.com";
            this.Password = "1234";
        }

        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }

        }
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                "Error",
                "Debes ingresar el usuario",
                "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                "Error",
                "Debes ingresar la contraseña",
                "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "slopez@rfacil.com" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
               "Error",
               "Usuario o contraseña incorrectos",
                    "Aceptar");
                this.Password = string.Empty;
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;

            this.email = string.Empty;
            this.password = string.Empty;

            MainViewModel.GetInstance().Bloxity = new BloxityViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new BloxityPage());
        }
        #endregion
    }

}
