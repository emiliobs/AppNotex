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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            this.Padding = Device.OnPlatform
                (
                    new Thickness(10,20,10,10),
                    new Thickness(10),
                    new Thickness(10)
                );

            saveButton.Clicked += SaveButton_Clicked;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userNameEntry.Text))
            {
                await DisplayAlert("Error","Debe ingresar  un Email.","Aceptar.");
                userNameEntry.Focus();
                return;
            }

            if (!Utilities.Utilities.IsValidEmail(userNameEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar  un Email Valido.", "Aceptar.");
                userNameEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(firstNameEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar al menos un Nombre.", "Aceptar.");
                firstNameEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(lastNameEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar al meos un Apellido.", "Aceptar.");
                lastNameEntry.Focus();
                return;
            }

            if (!Utilities.Utilities.IsValidPassword(passwordEntry.Text))
            {
                await DisplayAlert("Error", "La contraseña debe tener almenos 8 carácteres; 1 mayúscula, 1 número y 1 carácter especial ", "Aceptar.");
                passwordEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(confirmEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar una confirmación de Contraseña.", "Aceptar.");
                confirmEntry.Focus();
                return;
            }

            if (passwordEntry.Text != confirmEntry.Text)
            {
                await DisplayAlert("Error", "La contraseña y la confirmación no son iguales.","Aceptar.");
                confirmEntry.Focus();
                return;
            }

            this.RegisterStudent();
        }

        private async void RegisterStudent()
        {
            waitActivityIndicator.IsRunning = true;
            saveButton.IsEnabled = false;

            var user = new User
            {
                Address = addresEntry.Text,
                FirstName = firstNameEntry.Text,
                IsStudent=true,
                IsTeacher=false,
                LastName = lastNameEntry.Text,
                Password = passwordEntry.Text,
                Phone=phoneEntry.Text,
                UserName = userNameEntry.Text,
            };

            //aqui genero el body que le envio al llamado de  la api:
            var jsonRequest = JsonConvert.SerializeObject(user);
            var htttpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                var url = "/Notes/API/Users";
                var result = await client.PostAsync(url, htttpContent);

                if (!result.IsSuccessStatusCode)
                {
                    waitActivityIndicator.IsRunning = false;
                    saveButton.IsEnabled = true;
                    await DisplayAlert("Error",result.Content.ToString(),"Aceptar");
                    return;
                }

                response = await result.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                waitActivityIndicator.IsRunning = false;
                saveButton.IsEnabled = true;
                await DisplayAlert("Error",ex.Message,"Aceptar");
                return;
            }

            waitActivityIndicator.IsRunning = false;
            saveButton.IsEnabled = true;

            var userResponse = JsonConvert.DeserializeObject<User>(response);
            await Navigation.PushAsync(new MainPage(userResponse));
        }
    }
}
