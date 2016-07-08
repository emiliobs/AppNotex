﻿using AppNotex.Classes;
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
    public partial class MySettingsPage : ContentPage
    {
        private  User user;

        public MySettingsPage(User user)
        {
            InitializeComponent();

            this.user = user;

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
                await DisplayAlert("Error", "Debe ingresar  un Email.", "Aceptar.");
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


            this.SaveChange();

        }

        private async void SaveChange()
        {
            waitActivityIndicator.IsRunning = true;
            saveButton.IsEnabled = false;

            this.user.UserName = userNameEntry.Text;
            this.user.FirstName = firstNameEntry.Text;
            this.user.LastName = lastNameEntry.Text;
            this.user.Phone = phoneEntry.Text;
            this.user.Address = addresEntry.Text;

            //a qui ya serializo en json y luego enviarlos por un hhtp content:
            var jsonRequest = JsonConvert.SerializeObject(this.user);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = string.Empty;

            // to do??
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            waitActivityIndicator.IsRunning = true;
            saveButton.IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            photoImage.Source = this.user.PhotoFullPath;
            photoImage.HeightRequest = 280;
            photoImage.WidthRequest = 280;
            userNameEntry.Text = this.user.UserName;
            firstNameEntry.Text = this.user.FirstName;
            lastNameEntry.Text = this.user.LastName;
            phoneEntry.Text = this.user.Phone;
            addresEntry.Text = this.user.Address;

        }
    }
}
