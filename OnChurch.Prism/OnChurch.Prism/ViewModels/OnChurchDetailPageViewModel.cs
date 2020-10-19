using Newtonsoft.Json;
using OnChurch.Common.Helpers;
using OnChurch.Common.Models;
using OnChurch.Common.Responses;
using OnChurch.Prism.Helpers;
using OnChurch.Prism.ItemViewModels;
using OnChurch.Prism.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OnChurch.Prism.ViewModels
{
    public class OnChurchDetailPageViewModel : ViewModelBase    
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;
        private static OnChurchDetailPageViewModel _instance;

        public OnChurchDetailPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            LoadMenus();
            LoadUser();
        }

        public static OnChurchDetailPageViewModel GetInstance()
        {
            return _instance;
        }


        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        public void LoadUser()
        {
            if (Settings.IsLogin)
            {
                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                User = token.User;
            }
        }


        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
        {
            new Menu
            {
                Icon = "ic_members",
                PageName = $"{nameof(MembersPage)}",
                Title = Languages.Members
            },
            new Menu
            {
                Icon = "ic_meet",
                PageName = $"{nameof(MeetingsPage)}",
                Title = Languages.Meetings
            },
            new Menu
            {
                Icon = "ic_person",
                PageName = $"{nameof(ModifyUserPage)}",
                Title = Languages.ModifyUser,
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_exit",
                PageName = $"{nameof(LoginPage)}",
                Title = Title = Settings.IsLogin ? Languages.Logout : Languages.Login
            }
        };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title,
                    IsLoginRequired = m.IsLoginRequired
                }).ToList());
        }
    }
}
