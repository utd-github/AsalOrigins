using AsalOrigins.Data;
using AsalOrigins.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsalOrigins
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticleView : ContentPage
    {

        Post article;

        public ArticleView(Object post)
        {
            InitializeComponent();
            ShowPost(post);
        }

        private void ShowPost(object opost)
        {


            var singlepost = opost as Post;

            article = singlepost;

            article = new Post()
            {
                ID = singlepost.ID,
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


        }

        private async void Deletebtn_Clicked(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PostsQLite.db3");
            var postdb = new PostDatabase(dbPath);
            await postdb.DeleteNoteAsync(article);
            await Navigation.PopAsync();
        }

    }
}