using OnChurch.Common.Models;
using OnChurch.Common.Responses;
using OnChurch.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace OnChurch.Prism.ViewModels
{
    public class MeetingsPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private ObservableCollection<Meeting> _meetings;
        private bool _isRunning;

        public MeetingsPageViewModel(INavigationService navigationService, IApiService apiService)
            :base(navigationService)
        {
            Title = "Meetings";
            _apiService = apiService;
            LoadMeetingsAsync();
        }

        public bool IsRunning
        { 
            get => _isRunning;
            set => SetProperty(ref _isRunning, value); 
        }

        public ObservableCollection<Meeting> Meetings
        {
            get => _meetings;
            set => SetProperty(ref _meetings, value);
        }
        private async void LoadMeetingsAsync()
        {
            if(Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<Meeting>(url, "/api", "/Meeting");
            IsRunning = false;
            if(!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var myMeetings = (List<Meeting>)response.Result;
            Meetings = new ObservableCollection<Meeting>(myMeetings);
        }
    }
}
