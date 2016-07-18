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
    public partial class NewNotePage : ContentPage
    {
        private Group group;
        public NewNotePage(Group group)
        {
            InitializeComponent();

            this.Padding = Device.OnPlatform
                (
                    new Thickness(10,20,10,10),
                    new Thickness(10),
                    new Thickness(10)
                );

            this.group = group;

            myStudentsListView.ItemTemplate = new DataTemplate(typeof(MyNewNoteCell));
            myStudentsListView.RowHeight = 110;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadStudents();
        }

        private async void LoadStudents()
        {
            waitActivityIndicator.IsRunning = true;

            var response = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.zulu-software.com");
                var url = $"/Notes/API/Groups/GetStudents/{group.GroupId}";
                var result = await client.GetAsync(url);

                if (!result.IsSuccessStatusCode)
                {
                    waitActivityIndicator.IsRunning = false;
                    await DisplayAlert("Error", result.StatusCode.ToString(), "Aceptar");
                    return;

                }

                response = await result.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

                waitActivityIndicator.IsRunning = false;
                await DisplayAlert("Error", ex.Message, "Aceptar");
                return;
            }

            var myStudentsResponse = JsonConvert.DeserializeObject<List<MyStudentResponse>>(response);
            myStudentsListView.ItemsSource = myStudentsResponse;

            waitActivityIndicator.IsRunning = false;
        }
    }
}
