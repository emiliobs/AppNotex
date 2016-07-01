using AppNotex.Classes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using AppNotex.Utilities;
namespace AppNotex.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            this.Padding = Device.OnPlatform
                (
                    new Thickness(10,20,10,10),
                    new Thickness(10),
                    new Thickness(10)
                );
            enterButton.Clicked += EnterButton_Clicked;
        }

        protected override void OnAppearing()
        {
            waitActivityIndicator.IsRunning = true;
            waitActivityIndicator.IsRunning = false;
        }

        private async void EnterButton_Clicked(object sender, EventArgs e)
        {
            

            if (string.IsNullOrEmpty(emailEntry.Text))
            {
                await DisplayAlert("Error","Debe ingresar un E-Mail","Aceptar");
                emailEntry.Focus();
                return;
            }

            if (!Utilities.Utilities.IsValidEmail(emailEntry.Text))
            {
                await DisplayAlert("Error","Debe ingresar un E-Mail Válido.","Aceptar");
                emailEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                await DisplayAlert("Error","Debe ingresar una Contraseña","Aceptar");
                passwordEntry.Focus();
                return;

            }

            this.Login();
        }

        private async void Login()
        {
            waitActivityIndicator.IsRunning = true;

            var loginRequest = new LoginRequest
            {
                Email = emailEntry.Text,
                Password = passwordEntry.Text                
            };

            //aqui lo serializo para poder lo enviar en formato json:
            var jsonRequest = JsonConvert.SerializeObject(loginRequest);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = string.Empty;

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.zulu-software.com");
                var url = "/Notes/API/Users/Login";

                var result = await client.PostAsync(url, httpContent);

                if (!result.IsSuccessStatusCode)
                {
                    waitActivityIndicator.IsRunning = false;
                    await DisplayAlert("Error", "Usuario o Contrase Incorrectos." ,"Aceptar");
                    return;
                }

                response = await result.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

               await DisplayAlert("Error", ex.Message ,"Aceptar");
                waitActivityIndicator.IsEnabled = false;
                return;
            }

            var user = JsonConvert.DeserializeObject<User>(response);
            user.Password = passwordEntry.Text;

            //await DisplayAlert("Bienvenido ", $" {user.FullName} " ," Aceptar");
            waitActivityIndicator.IsRunning = false;
            //datos en persintencia:
            this.VerifyRememberme(user);

            //
            await Navigation.PushAsync(new MainPage(user));
           

        }

        private  void VerifyRememberme(User user)
        {
            if (remembermeSwich.IsToggled)//si es verdadera:
            {
                using (var db = new DataAccess())
                {
                    //lo grabo en la bd la persistencia:
                    db.Insert<User>(user);
                }
            }
        }
    }
}
