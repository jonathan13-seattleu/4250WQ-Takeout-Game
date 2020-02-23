using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// Create Item
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class CharacterCreatePage : ContentPage
    {
        // The item to create
        GenericViewModel<CharacterModel> ViewModel { get; set; }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public CharacterCreatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            data.Data = new CharacterModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";

            MaxHealthValue.Text = string.Format("{0:G}", ViewModel.Data.MaxHealth);
        }

        /// <summary>
        /// Save by calling for Create
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

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Range and update MaxHealth accordingly
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