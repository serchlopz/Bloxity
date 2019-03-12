namespace bloxity.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class BloxityViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiServices;
        #endregion

        #region Atributos
        private ObservableCollection<Bloxity> bloxity;
        private bool isRefreshing;
        #endregion


        #region Propiedades
        public ObservableCollection<Bloxity> Bloxity
        {
            get { return this.bloxity; }
            set { setValue(ref this.bloxity, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { setValue(ref this.isRefreshing, value); }
        }
        #endregion


        #region Constructores
        public BloxityViewModel()
        {
            this.apiServices = new ApiService();
            this.LoadLands();
        }
        #endregion


        #region Metodos
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this.apiServices.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
               "Error",
               connection.Message,
               "Aceptar");

                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiServices.GetList<Bloxity>("http://restcountries.eu", "/rest","/v2/all"); //http://restcountries.eu/rest/v2/all

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                "Error", 
                response.Message,
                "Aceptar");
                return;
            }
            var list = (List<Bloxity>)response.Result;
            this.Bloxity = new ObservableCollection<Bloxity>(list);
            this.IsRefreshing = false;

        }
        #endregion
        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }
        #endregion
    }
}
