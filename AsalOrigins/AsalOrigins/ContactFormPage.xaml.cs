using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsalOrigins
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactFormPage : ContentPage
	{
		public ContactFormPage ()
		{
			InitializeComponent ();
		}

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(emailtxt.Text);
                mail.To.Add("mohaaosman13@gmail.com");
                mail.Subject = subjecttxt.Text;
                mail.Body = bodytxt.Text;

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("mohaaosman13@gmail.com", "hafsa4ever");

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
            finally
            {
                await DisplayAlert("Email sent", "Thank you for contacting us, we'll reply soon", "OK");
                await Navigation.PopAsync();
            }
        }

    }
}