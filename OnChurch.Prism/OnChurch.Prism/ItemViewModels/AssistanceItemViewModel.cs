using OnChurch.Common.Models;
using OnChurch.Prism.ViewModels;
using OnChurch.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnChurch.Prism.ItemViewModels
{
    public class AssistanceItemViewModel : Meeting
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectAssistanceCommand;

        public AssistanceItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectAssistanceCommand => _selectAssistanceCommand ?? 
            (_selectAssistanceCommand = new DelegateCommand(SelectAssistanceAsync));

        private async void SelectAssistanceAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "meeting", this }
            };
            await _navigationService.NavigateAsync(nameof(AssistancesPage), parameters);
        }
    }
}
