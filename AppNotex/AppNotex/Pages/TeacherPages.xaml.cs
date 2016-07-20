using AppNotex.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNotex.Pages
{
    public partial class TeacherPages : ContentPage
    {
        private User user;
        private Group group;
        public TeacherPages(User user, Group group)
        {
            InitializeComponent();

            this.user = user;
            this.group = group;

            this.Padding = Device.OnPlatform
                (
                 new Thickness(10,20,10,10),
                 new Thickness(10),
                 new Thickness(10)
                );

            newNotesButton.Clicked += NewNotesButton_Clicked;
            showNotesButton.Clicked += ShowNotesButton_Clicked;
           
        }

        private async void ShowNotesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroupNotesPage(group));
        }

        private async void NewNotesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewNotePage(group));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


        }
    }
}
