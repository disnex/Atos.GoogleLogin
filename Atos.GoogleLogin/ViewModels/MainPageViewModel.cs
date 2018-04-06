using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atos.GoogleLogin.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        private DelegateCommand _loginViewNavigation;
        public DelegateCommand LoginViewNavigation
        {
            get { return _loginViewNavigation; }
            set { SetProperty(ref _loginViewNavigation, value); }
        }
        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Login with Google";
            _navigationService = navigationService;
            LoginViewNavigation = new DelegateCommand(Navigate);
            

            
            
    }

        private void Navigate()
        {
            _navigationService.NavigateAsync("Login");    
        }
    }
}
