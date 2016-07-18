using AppNotex.Cells;
using AppNotex.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppNotex.Pages
{
    public partial class MyGroupsPage : ContentPage
    {
        private User user;
        public MyGroupsPage( User user)
        {
            InitializeComponent();

            this.user = user;

            this.Padding = Device.OnPlatform
                (
                    new Thickness(10,20,10,10),
                    new Thickness(10),
                    new Thickness(10)
                );

            myGroupsListView.ItemTemplate = new DataTemplate(typeof(MyGroupCell));
            myGroupsListView.ItemSelected += MyGroupsListView_ItemSelected;
        }

        private async void MyGroupsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new NewNotePage((Group)e.SelectedItem));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadGroups();
        }

        private async void LoadGroups()
        {
            waitActivityIndicator.IsRunning = true;

            var response = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = $"/Notes/API/Groups/GetGroups/{this.user.UserId}";
                var result = await client.GetAsync(url);

                if (!result.IsSuccessStatusCode)
                {
                    waitActivityIndicator.IsRunning = false;
                    await DisplayAlert("Error",result.StatusCode.ToString(),"Aceptar");
                    
                    return;
                }

                response = await result.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                waitActivityIndicator.IsRunning = false;
                await DisplayAlert("Error", ex.Message,"Aceptar");
                return;
            }

            var myGroupsResponse = JsonConvert.DeserializeObject<MyGroupsResponse>(response);

            myGroupsListView.ItemsSource = myGroupsResponse.MyGroups;
            waitActivityIndicator.IsRunning = false;


        }
    }
}
