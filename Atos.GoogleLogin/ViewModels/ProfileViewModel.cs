using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atos.GoogleLogin.Models;
using Atos.GoogleLogin.Services;
using Prism.Navigation;

namespace Atos.GoogleLogin.ViewModels
{
	public class ProfileViewModel : BindableBase
	{

        private GoogleProfile _googleProfile;
        private readonly GoogleService _googleService;
        public GoogleProfile GoogleProfile
        {
            get { return _googleProfile; }
            set { SetProperty(ref _googleProfile, value); }
        }
        public ProfileViewModel()
        {
            _googleService = new GoogleService();

        }

        public async Task<string> GetAccessTokenAsync(string code)
        {
            var accessToken = await _googleService.GetAccessTokenAsync(code);
            return accessToken;
        }

        public async Task SetGoogleUserProfileAsync(string accessToken)
        {
            GoogleProfile = await _googleService.GetGoogleUserProfileAsync(accessToken);
        }

        private string ExtractCodeFromUrl(string url)
        {
            if (url.Contains("code="))
            {
                var attributes = url.Split('&');

                var code = attributes.FirstOrDefault(s => s.Contains("code=")).Split('=')[1];

                return code;
            }

            return string.Empty;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            /*
            //var code = ExtractCodeFromUrl(parameters.Url);
            if (code != "")
            {

                var accessToken = GetAccessTokenAsync(code);

                //await SetGoogleUserProfileAsync(accessToken);

               // SetPageContent(_googleViewModel.GoogleProfile);
            }
            */
        }
    }
}
