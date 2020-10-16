using OnChurch.Common.Services;
using OnChurch.Prism.ViewModels;
using OnChurch.Prism.Views;
using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace OnChurch.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MzM2MjIyQDMxMzgyZTMzMmUzMG1aSkc4dEdSNFRPWmcxRnh2d21GWUFVNHV4ZEJUWThZcVFIM3hCMTd6eUU9");

            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MeetingsPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MeetingsPage, MeetingsPageViewModel>();
            containerRegistry.RegisterForNavigation<AssistancesPage, AssistancesPageViewModel>();
        }
    }
}
