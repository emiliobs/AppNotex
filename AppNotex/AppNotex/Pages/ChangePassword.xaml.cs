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
    public partial class ChangePassword : ContentPage
    {
        private User user;

        public ChangePassword(User user)
        {
            InitializeComponent();

            this.user= user;

            this.Padding =  Device.OnPlatform(
                    new Thickness(10,20,10,10),
                    new Thickness(10),
                    new Thickness(10)
                );

            saveButton.Clicked += SaveButton_Clicked;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            photoImage.Source = this.user.PhotoFullPath;
            photoImage.HeightRequest = 280;
            photoImage.WidthRequest = 280;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty (currentPasswordEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar  Contraseña Actual ", "Aceptar.");
                currentPasswordEntry.Focus();
                return;
            }

            if (currentPasswordEntry.Text !=  user.Password )
            {
                await DisplayAlert("Error", "La contraseña actual no es Correcta ", "Aceptar.");
                currentPasswordEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(newPasswordEntry.Text))
            {
                await DisplayAlert("Error", "Deb ingresar un Nuevo Password.", "Aceptar.");
                currentPasswordEntry.Focus();
                return;
            }

            if (!Utilities.Utilities.IsValidPassword(newPasswordEntry.Text))
            {
                await DisplayAlert("Error", "La contraseña debe tener almenos 8 carácteres; 1 mayúscula, 1 número y 1 carácter especial ", "Aceptar.");
                newPasswordEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(confirmEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar una confirmación de Contraseña.", "Aceptar.");
                confirmEntry.Focus();
                return;
            }

            if (newPasswordEntry.Text != confirmEntry.Text)
            {
                await DisplayAlert("Error", "La contraseña y la confirmación no son iguales.", "Aceptar.");
                confirmEntry.Focus();
                return;
            }

            this.changePassword();
        }

        private async void changePassword()
        {
            waitActivityIndicator.IsRunning = true;
            saveButton.IsEnabled = false;

            //genero un objeto al vuelo:
            var request = new
            {
                UserName = user.UserName,
                OldPassword = currentPasswordEntry.Text,
                NewPassword = newPasswordEntry.Text,
            };

            //a qui ya serializo en json y luego enviarlos por un hhtp content:
            var jsonRequest = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = string.Empty;


            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.zulu-software.com");
                var url = $"/Notes/API/Users/ChangePassword";
                var result = await client.PutAsync(url, httpContent);

                if (!result.IsSuccessStatusCode)
                {
                    waitActivityIndicator.IsRunning = false;
                    saveButton.IsEnabled = true;
                    await DisplayAlert("Error", result.Content.ToString(), "Aceptar");
                    return;
                }

                //Actualizo el paswword:
                //datos en persistencia:
                using (var db = new DataAccess())
                {
                    user.Password = newPasswordEntry.Text;
                    db.Update(user);

                }
            }
            catch (Exception ex)
            {

                waitActivityIndicator.IsRunning = false;
                saveButton.IsEnabled = true;
                await DisplayAlert("Error", ex.Message, "Aceptar");
                return;
            }

            waitActivityIndicator.IsRunning = false;
            saveButton.IsEnabled = true;
            await DisplayAlert("Confirmación","Contraseña Modificada Correctamente","Aceptar");
            //Lo envio a la ventana anterior:
            await Navigation.PopAsync();
        }
    }
}
