using OnChurch.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnChurch.Prism.ViewModels
{
    public class ShowMembersChurchViewModel : ViewModelBase
    {
        public ShowMembersChurchViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            Title = Languages.Members;
        }
    }
}
