using AppNotex.Classes;
using AppNotex.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AppNotex
{
    public class App : Application
    {
        public App()
        {
            using (var db = new DataAccess())
            {
                var user = db.First<User>();

                if (user == null)
                {
                    this.MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    this.MainPage = new NavigationPage(new MainPage(user));
                }
            }

            //MainPage = new NavigationPage(new RegisterPage());

            // The root page of your application
            //MainPage = new NavigationPage(new LoginPage());
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                HorizontalTextAlignment = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
