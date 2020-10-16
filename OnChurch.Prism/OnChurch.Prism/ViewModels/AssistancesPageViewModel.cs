using OnChurch.Common.Models;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace OnChurch.Prism.ViewModels
{
    public class AssistancesPageViewModel : ViewModelBase
    {
        private Meeting _meeting;
        //private User _user;
        private List<Assistance> _assistances;

        public AssistancesPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Assistances";
        }

        public Meeting Meeting 
        {
            get => _meeting;
            set => SetProperty(ref _meeting, value);
        }

        /*public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }*/

        public List<Assistance> Assistances
        {
            get => _assistances;
            set => SetProperty(ref _assistances, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("meeting"))
            {
                Meeting = parameters.GetValue<Meeting>("meeting");
                Title = Meeting.Date.ToString();
                Assistances = Meeting.Assistances.ToList();
            }
        }
    }
}
