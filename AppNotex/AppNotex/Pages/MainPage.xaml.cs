using AppNotex.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNotex.Pages
{
    public partial class MainPage : ContentPage
    {
        private User user;
        public MainPage(User user)
        {
            InitializeComponent();

            this.user = user;

            this.Padding = Device.OnPlatform
                (
                    new Thickness(10,20,10,10),
                    new Thickness(10),
                    new Thickness(10)
                );

            logOutButton.Clicked += LogOutButton_Clicked;
        }

        private async void LogOutButton_Clicked(object sender, EventArgs e)
        {
            using (var db = new DataAccess())
            {
                var user = db.First<User>();

                if (user != null)
                {
                    db.Delete(user);
                }

                await Navigation.PushAsync(new LoginPage());
            }
        }

        //esto significa que cad que pase poraqui me carga el objeto user actualizado:
        protected override void OnAppearing()
        {
            base.OnAppearing();

           

            userNameLabel.Text = this.user.FullName;
            photImage.Source = this.user.PhotoFullPath;
            //tamañño en que la quiero que aparezca:
            photImage.HeightRequest = 280;
            photImage.WidthRequest = 280;

            //Pregsunto si profesor o  estudiante:
            if (this.user.IsStudent)
            {
                myNotesButton.IsVisible = true;
            }
            else
            {
                myNotesButton.IsVisible = false;
            }

            if (this.user.IsTeacher)
            {
                enterNotesButton.IsVisible = true;
            }
            else
            {
                enterNotesButton.IsVisible = false;
            }

        }
    }
}
