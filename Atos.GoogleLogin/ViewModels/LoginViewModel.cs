using Atos.GoogleLogin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Atos.GoogleLogin.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        INavigationService _navigationService;
        
        private GoogleProfile _googleProfile;
        public GoogleProfile GoogleProfile
        {
            get { return _googleProfile; }
            set { SetProperty(ref _googleProfile, value); }
        }
        
        
        private string _link = "https://accounts.google.com/o/oauth2/v2/auth?scope=openid&redirect_uri=https://www.youtube.com/&response_type=code&client_id=533856360051-s6q57dc1oo1bn19u3kofcq30lv86cik2.apps.googleusercontent.com";
        public string Link
        {
            get { return _link; }
            set { SetProperty(ref _link, value); }
        }
        
        

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //Browser.Source = "https://accounts.google.com/o/oauth2/v2/auth?scope=openid&redirect_uri=https://www.youtube.com/&response_type=code&client_id=533856360051-s6q57dc1oo1bn19u3kofcq30lv86cik2.apps.googleusercontent.com";
      
        }

        




        public ICommand LoginViewNavigatedCommand => new Command(async () =>
        {
            //await _navigationService.NavigateAsync("MainPage");
           /* var code = await ExtractCodeFromUrl(e.Url);

            if (code != "")
            {

                var accessToken = await GetAccessTokenAsync(code);

                _googleProfile = SetGoogleUserProfileAsync(accessToken);

                //SetPageContent(_googleViewModel.GoogleProfile);
            }
            */
         

        });

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

        public async Task<string> GetAccessTokenAsync(string code)
        {
            var requestUrl =
                "https://www.googleapis.com/oauth2/v4/token"
                + "?code=" + code
                + "&client_id=533856360051-s6q57dc1oo1bn19u3kofcq30lv86cik2.apps.googleusercontent.com"
                + "&client_secret=Xc3yKo1Q05U5oMwvB_P9mX3y"
                + "&redirect_uri=https://www.youtube.com/"
                + "&grant_type=authorization_code";

            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(requestUrl, null);

            var json = await response.Content.ReadAsStringAsync();

            var accessToken = JsonConvert.DeserializeObject<JObject>(json).Value<string>("access_token");

            return accessToken;
        }

        public async Task<GoogleProfile> SetGoogleUserProfileAsync(string accessToken)
        {

            var requestUrl = "https://www.googleapis.com/plus/v1/people/me"
                             + "?access_token=" + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var googleProfile = JsonConvert.DeserializeObject<GoogleProfile>(userJson);

            return googleProfile;
        }

    }
}
