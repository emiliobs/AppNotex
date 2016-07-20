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
    public partial class MySubjectsPage : ContentPage
    {
        private User user;
        public MySubjectsPage( User user)
        {
            InitializeComponent();

            this.user = user;

            this.Padding = Device.OnPlatform
                (
                    new Thickness(10,20,10,10),
                    new Thickness(10),
                    new Thickness(10)
                );

            mySubjectListView.ItemTemplate = new DataTemplate(typeof(MySubjectCell));
            mySubjectListView.RowHeight = 160;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadSubjects();
        }

        private async void LoadSubjects()
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

            var myGroupsResponse = JsonConvert.DeserializeObject<MyGroupsResponse>(response);
            await CalculateNotes(myGroupsResponse.MySubjects);
            mySubjectListView.ItemsSource = myGroupsResponse.MySubjects;
            waitActivityIndicator.IsRunning = false;
        }

        private async Task CalculateNotes(List<Teacher> mySubjects)
        {
            foreach (var mySubject in mySubjects)
            {
                mySubject.Note = await GetDefinitive(user.UserId, mySubject.GroupId);
            }
        }

        private async Task<float> GetDefinitive(int userId, int groupId)
        {

            var response = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.zulu-software.com");
                var url = $"/Notes/API/Groups/GetNote/{groupId}/{userId}";
                var result = await client.GetAsync(url);

                if (!result.IsSuccessStatusCode)
                {
                    return 0;
                }

                response = await result.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                //waitActivityIndicator.IsRunning = false;
                //await DisplayAlert("Error", ex.Message, "Aceptar");
                return 0;
                
            }

            var myNoteResponse = JsonConvert.DeserializeObject<MyNoteResponse>(response);
            return myNoteResponse.Note;
        }
    }
}
