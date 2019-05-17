using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsalOrigins
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonatePage : ContentPage
    {
        public DonatePage()
        {
            InitializeComponent();
        }

        private void Sahal_Clicked(object sender, EventArgs e)
        {
            var number = "*884*490214*1#";
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
                Console.WriteLine("Error Argument Null Exception: ", anEx);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device..
                Console.WriteLine("Error Feature Not Supported Exception: ", ex);
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine("Error Exception: ", ex);
            }
        }

        private void Zaad_Clicked(object sender, EventArgs e)
        {
            var number = "*880*0906214499*1#";
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
                Console.WriteLine("Error Argument Null Exception: ", anEx);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device..
                Console.WriteLine("Error Feature Not Supported Exception: ", ex);
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                Console.WriteLine("Error Exception: ", ex);
            }
        }
    }
}