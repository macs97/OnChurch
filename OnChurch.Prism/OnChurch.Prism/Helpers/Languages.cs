﻿using OnChurch.Common.Interfaces;
using OnChurch.Prism.Resources;
using System.Globalization;
using Xamarin.Forms;

namespace OnChurch.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string Loading => Resource.Loading;

        public static string Date => Resource.Date;
        
        public static string Meetings => Resource.Meetings;
    }

}
