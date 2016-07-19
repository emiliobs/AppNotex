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
        private List<MyStudentResponse> myStudentsResponse;
        float percentage;
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

            saveButton.Clicked += SaveButton_Clicked;
      

                }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty( percentajeName.Text))
            {
                await DisplayAlert("Error","Debe ingresar un porcentaje de Notas.","Aceptar");
                percentajeName.Focus();
                return;
            }

             percentage = float.Parse(percentajeName.Text) / 100;

            if (percentage < 0 || percentage > 1)
            {
                await DisplayAlert("Error","Debe ingresar un porcentaje entre 1 y 100%","Aceptar");
                percentajeName.Focus();
                return;
            }

            foreach (var myStudentResponse in myStudentsResponse)
            {
                if (myStudentResponse.Note < 0 || myStudentResponse.Note > 5)
                {
                    await DisplayAlert("Error",$"El Estudiante { myStudentResponse.Student.FullName} no tiene un a Nota Valida ",
                                       "Aceptar");
                    return;
                }
            }

            SaveNote();
        }

        private  void SaveNote()
        {
            waitActivityIndicator.IsRunning = true;

            var body = new
            {
                Percentage = percentage,
                Students = myStudentsResponse,
            };

            var jsonRequest = JsonConvert.SerializeObject(body);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var respkinse = string.Empty;



            waitActivityIndicator.IsRunning = false;
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

            myStudentsResponse = JsonConvert.DeserializeObject<List<MyStudentResponse>>(response);
            myStudentsListView.ItemsSource = myStudentsResponse;

            waitActivityIndicator.IsRunning = false;
        }
    }
}
