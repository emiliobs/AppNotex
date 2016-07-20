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
    public partial class GroupNotesPage : ContentPage
    {
        private Group group;
        public GroupNotesPage(Group group)
        {
            InitializeComponent();

            this.Padding = Device.OnPlatform
                (
                    new Thickness(10,20,10,10),
                    new Thickness(10),
                    new Thickness(10)

                );

            this.group = group;

            myStudentsListView.ItemTemplate = new DataTemplate(typeof(MyNewNoteCell2));
            myStudentsListView.RowHeight = 110;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadStudentNotes();
        }

        private async void LoadStudentNotes()
        {
            waitActivityIndicator.IsRunning = true;

            var response = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = $"/Notes/API/Groups/GetNotesGroup/{this.group.GroupId}";
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

            var myStudents = JsonConvert.DeserializeObject<List<StudentNoteResponse>>(response);
            //await CalculateNotes(myGroupsResponse.MySubjects);
            myStudentsListView.ItemsSource = myStudents;

            waitActivityIndicator.IsRunning = false;
        }
    }
}
