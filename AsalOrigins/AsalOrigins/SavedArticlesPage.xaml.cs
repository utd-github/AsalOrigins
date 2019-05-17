using AsalOrigins.Data;
using AsalOrigins.Models;
using SQLite;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsalOrigins
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedArticlesPage : ContentPage
    {
        private SQLiteConnection db;
        public int Count = 0;
        public short Counter = 0;
        public int SlidePosition = 0;
        int heightRowsList = 90;

        public SavedArticlesPage()
        {
            InitializeComponent();
            GetSavedPostsAsync();

        }

        private async
        Task
GetSavedPostsAsync()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PostsQLite.db3");

            var db = new PostDatabase(dbPath);
            try
            {
                //Activity indicator visibility on
                activity_indicator.IsVisible = true;
                activity_indicator.IsRunning = true;
                refreshcon.IsVisible = false;
                savedPosts.IsVisible = false;


                var tr = await db.GetPostsAsync();
                //After deserializing , we store our data in the List called ObservableCollection


                //Then finally we attach the List to the ListView. Seems Simple :)
                savedPosts.ItemsSource = tr;


                //We check the number of PostModelItem in the Observable Collection
                int i = tr.Count();
                if (i > 0)
                {
                    //If they are greater than Zero we stop the activity indicator
                    activity_indicator.IsRunning = false;
                    savedPosts.IsVisible = true;
                }
                else
                {
                    savedPosts.IsVisible = false;
                    activity_indicator.IsVisible = false;
                    refreshcon.IsVisible = true;
                    infolabel.Text = "No saved posts found";

                }

                //Here we Wrap  the size of the ListView according to the number of PostModelItem which have been retrieved 
                i = (tr.Count() * heightRowsList);
                activity_indicator.HeightRequest = i;

            }
            catch (Exception ey)
            {
                Debug.WriteLine("" + ey);
            }
        }

        private async void SavedPosts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Post item)
            {
                await Navigation.PushAsync(new ArticleView(item));

                savedPosts.SelectedItem = null;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetSavedPostsAsync();
        }
    }


}