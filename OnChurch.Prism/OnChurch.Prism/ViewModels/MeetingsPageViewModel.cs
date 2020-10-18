using OnChurch.Common.Models;
using OnChurch.Common.Responses;
using OnChurch.Common.Services;
using OnChurch.Prism.Helpers;
using OnChurch.Prism.ItemViewModels;
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
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ObservableCollection<AssistanceItemViewModel> _meetings;
        private List<Meeting> _myMeetings;
        private bool _isRunning;

        public MeetingsPageViewModel(INavigationService navigationService, IApiService apiService)
            :base(navigationService)
        {
            Title = Languages.Meetings;
            _navigationService = navigationService;
            _apiService = apiService;
            LoadMeetingsAsync();
        }

        public bool IsRunning
        { 
            get => _isRunning;
            set => SetProperty(ref _isRunning, value); 
        }

        public ObservableCollection<AssistanceItemViewModel> Meetings
        {
            get => _meetings;
            set => SetProperty(ref _meetings, value);
        }
        private async void LoadMeetingsAsync()
        {
            if(Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<Meeting>(url, "/api", "/Meeting");
            IsRunning = false;
            if(!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            _myMeetings = (List<Meeting>)response.Result;
            ShowAssistances();
        }

        private void ShowAssistances()
        {
            Meetings = new ObservableCollection<AssistanceItemViewModel>(_myMeetings.Select(m => new AssistanceItemViewModel(_navigationService)
            {
                Id = m.Id,
                Date = m.Date,
                Church = m.Church,
                Assistances = m.Assistances,
            })
                .ToList());
        }
    }
}
