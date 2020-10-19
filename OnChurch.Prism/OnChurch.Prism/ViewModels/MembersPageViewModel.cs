using Newtonsoft.Json;
using OnChurch.Common.Helpers;
using OnChurch.Common.Models;
using OnChurch.Common.Responses;
using OnChurch.Common.Services;
using OnChurch.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace OnChurch.Prism.ViewModels
{
    public class MembersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private List<User> _users;
        private bool _isRunning;
        private readonly IApiService _apiService;

        public MembersPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            Title = "Users";
            _navigationService = navigationService;
            _apiService = apiService;
            LoadUsers();
        }

        public List<User> User
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public async void LoadUsers()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
            IsRunning = true;
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetMembersAsync(url, "/api", "/Members", token.Token);
            IsRunning = false;
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            _users = (List<User>)response.Result;
        }
    }
}
