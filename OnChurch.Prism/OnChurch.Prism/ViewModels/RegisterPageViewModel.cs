using OnChurch.Common.Models;
using OnChurch.Common.Requests;
using OnChurch.Common.Responses;
using OnChurch.Common.Services;
using OnChurch.Prism.Helpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OnChurch.Prism.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IRegexHelper _regexHelper;
        private readonly IApiService _apiService;
        private ImageSource _image;
        private UserRequest _user;
        private Church _church;
        private Profession _profession;
        private ObservableCollection<Church> _churches;
        private ObservableCollection<Profession> _professions;
        private Section _section;
        private ObservableCollection<Section> _sections;
        private Campus _campus;
        private ObservableCollection<Campus> _campuses;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _registerCommand;
        private readonly IFilesHelper _filesHelper;
        private MediaFile _file;
        private DelegateCommand _changeImageCommand;

        public RegisterPageViewModel(
            INavigationService navigationService,
            IRegexHelper regexHelper,
            IApiService apiService,
            IFilesHelper filesHelper)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _regexHelper = regexHelper;
            _apiService = apiService;
            Title = Languages.Register;
            _filesHelper = filesHelper;
            Image = App.Current.Resources["UrlNoImage"].ToString();
            IsEnabled = true;
            User = new UserRequest();
            LoadCampusesAsync();

        }

        public DelegateCommand ChangeImageCommand => _changeImageCommand ??
    (_changeImageCommand = new DelegateCommand(ChangeImageAsync));


        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public UserRequest User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public Campus Campus
        {
            get => _campus;
            set
            {
                Sections = value != null ? new ObservableCollection<Section>(value.Sections) : null;
                Churches = new ObservableCollection<Church>();
                Section = null;
                Church = null;
                SetProperty(ref _campus, value);
            }
        }

        public ObservableCollection<Campus> Campuses
        {
            get => _campuses;
            set => SetProperty(ref _campuses, value);
        }

        public Section Section
        {
            get => _section;
            set
            {
                Churches = value != null ? new ObservableCollection<Church>(value.Churches) : null;
                Church = null;
                SetProperty(ref _section, value);
            }
        }

        public ObservableCollection<Section> Sections
        {
            get => _sections;
            set => SetProperty(ref _sections, value);
        }

        public Church Church
        {
            get => _church;
            set => SetProperty(ref _church, value);
        }

        public Profession Profession
        {
            get => _profession;
            set => SetProperty(ref _profession, value);
        }

        public ObservableCollection<Church> Churches
        {
            get => _churches;
            set => SetProperty(ref _churches, value);
        }
        public ObservableCollection<Profession> Professions
        {
            get => _professions;
            set => SetProperty(ref _professions, value);
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void LoadCampusesAsync()
        {
            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<Campus>(url, "/api", "/Campuses");
            Response responseProfessions = await _apiService.GetListAsync<Profession>(url, "/api", "/Professions");
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess || !responseProfessions.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<Campus> list = (List<Campus>)response.Result;
            List<Profession> listProfession = (List<Profession>)responseProfessions.Result;
            Campuses = new ObservableCollection<Campus>(list.OrderBy(c => c.Name));
            Professions = new ObservableCollection<Profession>(listProfession.OrderBy(p => p.Name));
        }

        private async void RegisterAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            User.ImageArray = imageArray;


            string url = App.Current.Resources["UrlAPI"].ToString();

            User.ChurchId = Church.Id;
            User.ProfessionId = Profession.Id;

            Response response = await _apiService.RegisterUserAsync(url, "/api", "/Account/Register", User);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                if (response.Message == "Error003")
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error003, Languages.Accept);
                }
                else if (response.Message == "Error004")
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error004, Languages.Accept);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                }

                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.RegisterMessage, Languages.Accept);
            await _navigationService.GoBackAsync();

        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(User.Document))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.DocumentError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.FirstName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.LastName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Address))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.AddressError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Email) || !_regexHelper.IsValidEmail(User.Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Phone))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PhoneError, Languages.Accept);
                return false;
            }

            if (Campus == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CampusError, Languages.Accept);
                return false;
            }

            if (Section == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.SectionError, Languages.Accept);
                return false;
            }

            if (Church == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ChurchError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Password) || User.Password?.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordConfirmError1, Languages.Accept);
                return false;
            }

            if (User.Password != User.PasswordConfirm)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordConfirmError2, Languages.Accept);
                return false;
            }

            return true;
        }

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.PictureSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.FromCamera);

            if (source == Languages.Cancel)
            {
                _file = null;
                return;
            }

            if (source == Languages.FromCamera)
            {
                if (!CrossMedia.Current.IsCameraAvailable)
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoCameraSupported, Languages.Accept);
                    return;
                }

                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoGallerySupported, Languages.Accept);
                    return;
                }

                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file.GetStream();
                    return stream;
                });
            }
        }

    }


}
