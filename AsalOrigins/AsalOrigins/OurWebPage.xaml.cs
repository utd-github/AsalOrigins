using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsalOrigins
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OurWebPage : ContentPage
	{
		public OurWebPage ()
		{
			InitializeComponent ();
		}

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            
            loadingRing.IsRunning = false;
            Webview.IsVisible = true;
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            
            loadingRing.IsRunning = true;
        }
    }
}