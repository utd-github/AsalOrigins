using AsalOrigins.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsalOrigins
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public int Count = 0;
        public short Counter = 0;
        public int SlidePosition = 0;
        int heightRowsList = 90;
        private const string Url = "https://asalorigin.herokuapp.com/posts/api";
        // This handles the Web data request
        private HttpClient _client = new HttpClient();

        public HomePage()
        {
            InitializeComponent();
            GetPosts();
            PostsList.RefreshCommand = new Command(() =>
            {
                GetPosts();
                PostsList.IsRefreshing = false;
            });
        }

        private async void GetPosts()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    //Activity indicator visibility on
                    activity_indicator.IsVisible = true;
                    activity_indicator.IsRunning = true;
                    refreshcon.IsVisible = false;
                    PostsList.IsVisible = false;
                    //Getting JSON data from the Web
                    var content = await _client.GetStringAsync(Url);
                    //We deserialize the JSON data from this line
                    var tr = JsonConvert.DeserializeObject<List<PostModelItem>>(content);
                    //After deserializing , we store our data in the List called ObservableCollection
                    ObservableCollection<PostModelItem> trends = new ObservableCollection<PostModelItem>(tr);

                    //Then finally we attach the List to the ListView. Seems Simple :)
                    PostsList.ItemsSource = trends;


                    //We check the number of PostModelItem in the Observable Collection
                    int i = trends.Count;
                    if (i > 0)
                    {
                        //If they are greater than Zero we stop the activity indicator
                        activity_indicator.IsRunning = false;
                        PostsList.IsVisible = true;
                    }
                    else
                    {
                        PostsList.IsVisible = false;
                        activity_indicator.IsVisible = false;
                        refreshcon.IsVisible = true;
                        infolabel.Text = "No posts were found please come back again";

                    }

                    //Here we Wrap  the size of the ListView according to the number of PostModelItem which have been retrieved 
                    i = (trends.Count * heightRowsList);
                    activity_indicator.HeightRequest = i;

                }
                catch (Exception ey)
                {
                    Debug.WriteLine("" + ey);
                }
            }
            else
            {
                PostsList.IsVisible = false;
                activity_indicator.IsVisible = false;
                refreshcon.IsVisible = true;
                infolabel.Text = "Error No connectivity";

            }
        }

        private async void PostsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as PostModelItem;
            if (item != null)
            {
                await Navigation.PushAsync(new PostView(item));

                PostsList.SelectedItem = null;
            }
        }

        private void Refreshbtn_Clicked(object sender, EventArgs e)
        {
            GetPosts();
        }


    }
}