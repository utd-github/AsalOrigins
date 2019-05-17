using AsalOrigins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using AsalOrigins.Data;
using System.Diagnostics;
using Xamarin.Forms.Internals;

namespace AsalOrigins
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostView : ContentPage
    {
        SQLiteConnection db;

        Post article;

        public PostView(object ppost)
        {

            InitializeComponent();

            ShowPost(opost: ppost);

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PostsQLite.db3");

            db = new SQLiteConnection(dbPath);

            db.CreateTable<Post>();

        }

        private async void ShowPost(object opost)
        {

            savebtn.IsEnabled = false;


            var singlepost = opost as PostModelItem;
            article = new Post()
            {
                ID = singlepost._id,
                Author = singlepost.Author,
                Body = singlepost.Body,
                Date = singlepost.Date,
                Intro = singlepost.Intro,
                Title = singlepost.Title
            };


            MainPageContent.Title = singlepost.Title;

            var htmlSource = new HtmlWebViewSource
            {
                Html = @singlepost.Body
            };
            browser.Source = htmlSource;

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PostsQLite.db3");

            var postdb = new PostDatabase(dbPath);

            var gpost = await postdb.GetPostAsync(article.ID);

            if (gpost == null)
            {
                savebtn.IsEnabled = true;
            }
            else
            {
                await DisplayAlert("Warning", "Post Already Saved", "OK");
                savebtn.IsEnabled = false;
            }

        }


        private async void Savebtn_Clicked(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PostsQLite.db3");

            var postdb = new PostDatabase(dbPath);


            if (await postdb.SavePostAsync(article) != 0)
            {
                savebtn.IsEnabled = false;
            }
            else
            {
                await DisplayAlert("Error", "Error saving", "OK");
                savebtn.IsEnabled = true;
            }

        }
    }
}