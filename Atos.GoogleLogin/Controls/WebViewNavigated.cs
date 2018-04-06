using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Atos.GoogleLogin.Controls
{
    class WebViewNavigated : WebView
    {
        public static readonly BindableProperty NavigatedCommandProperty = 
            BindableProperty.Create(nameof(NavigatedCommand), typeof(ICommand), typeof(WebViewNavigated), null);

        public WebViewNavigated()
        {
            Navigated += (s, e) =>
            {
                if (NavigatedCommand?.CanExecute(e) ?? false)
                    NavigatedCommand.Execute(e);
            };
        }

        public ICommand NavigatedCommand
        {
            get { return (ICommand)GetValue(NavigatedCommandProperty); }
            set { SetValue(NavigatedCommandProperty, value); }
        }
    }
}
