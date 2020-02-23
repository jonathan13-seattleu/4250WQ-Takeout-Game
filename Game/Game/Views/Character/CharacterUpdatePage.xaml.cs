using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using Game.Models;
using Game.Helpers;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class CharacterUpdatePage : ContentPage
    {
        // View Model for Item
        readonly GenericViewModel<CharacterModel> ViewModel;

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            MaxHealthValue.Text = string.Format("{0:G}", ViewModel.Data.MaxHealth);

        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }
            //If the name is empty, then display an alert and have users enter a valid string.
            if (string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                await DisplayAlert("Alert", "Name needs to have a string in it", "Ok");
            }
            //If the description is null, then display an alert and have users enter a valid string
            if (string.IsNullOrEmpty(ViewModel.Data.Description))
            {
                await DisplayAlert("Alert", "Description needs to have a string in it", "Ok");
            }
            else
            {
                MessagingCenter.Send(this, "Update", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            LevelValue.Text = String.Format("{0}", e.NewValue);

            var level = LevelStepper.Value + 1;
            int Templevel = Convert.ToInt32(level);

            // Roll the Dice and reset the Health
            ViewModel.Data.MaxHealth = DiceHelper.RollDice(Templevel, 10);

            MaxHealthValue.Text = string.Format(" {0:G}", ViewModel.Data.MaxHealth);

        }


    }
}