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
        public GenericViewModel<CharacterModel> ViewModel { get; set; }

        // Empty Constructor for UTs
        public CharacterCreatePage(bool UnitTest) { }

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
        public async void Save_Clicked(object sender, EventArgs e)
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
                MessagingCenter.Send(this, "Create", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Range and update MaxHealth accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            LevelValue.Text = String.Format("{0}", e.NewValue);
            var level = LevelStepper.Value + 1;
            int Templevel = Convert.ToInt32(level);

            // Roll the Dice and reset the Health
            ViewModel.Data.MaxHealth = DiceHelper.RollDice(Templevel, 10);

            MaxHealthValue.Text = string.Format(" {0:G}", ViewModel.Data.MaxHealth);
            ViewModel.Data.CurrentHealth = ViewModel.Data.MaxHealth;
            setCharacterAttributes(Templevel);
        }
        public void setCharacterAttributes(int level)
        {
            switch (level)
            {
                case 1:
                    ViewModel.Data.Speed = 1;
                    ViewModel.Data.ExperienceTotal = 0;
                    ViewModel.Data.Defense = 1;
                    ViewModel.Data.Attack = 1;
                    break;
                case 2:
                    ViewModel.Data.Speed = 1;
                    ViewModel.Data.ExperienceTotal = 300;
                    ViewModel.Data.Defense = 2;
                    ViewModel.Data.Attack = 1;
                    break;
                case 3:
                    ViewModel.Data.Speed = 1;
                    ViewModel.Data.ExperienceTotal = 900;
                    ViewModel.Data.Defense = 3;
                    ViewModel.Data.Attack = 2;
                    break;
                case 4:
                    ViewModel.Data.Speed = 1;
                    ViewModel.Data.ExperienceTotal = 2700;
                    ViewModel.Data.Defense = 3;
                    ViewModel.Data.Attack = 2;
                    break;
                case 5:
                    ViewModel.Data.Speed = 2;
                    ViewModel.Data.ExperienceTotal = 6500;
                    ViewModel.Data.Defense = 4;
                    ViewModel.Data.Attack = 2;
                    break;
                case 6:
                    ViewModel.Data.Speed = 2;
                    ViewModel.Data.ExperienceTotal = 14000;
                    ViewModel.Data.Defense = 4;
                    ViewModel.Data.Attack = 3;
                    break;
                case 7:
                    ViewModel.Data.Speed = 2;
                    ViewModel.Data.ExperienceTotal = 23000;
                    ViewModel.Data.Defense = 5;
                    ViewModel.Data.Attack = 3;
                    break;
                case 8:
                    ViewModel.Data.Speed = 2;
                    ViewModel.Data.ExperienceTotal = 34000;
                    ViewModel.Data.Defense = 5;
                    ViewModel.Data.Attack = 3;
                    break;
                case 9:
                    ViewModel.Data.Speed = 2;
                    ViewModel.Data.ExperienceTotal = 48000;
                    ViewModel.Data.Defense = 5;
                    ViewModel.Data.Attack = 3;
                    break;
                case 10:
                    ViewModel.Data.Speed = 3;
                    ViewModel.Data.ExperienceTotal = 64000;
                    ViewModel.Data.Defense = 6;
                    ViewModel.Data.Attack = 4;
                    break;
                case 11:
                    ViewModel.Data.Speed = 3;
                    ViewModel.Data.ExperienceTotal = 85000;
                    ViewModel.Data.Defense = 6;
                    ViewModel.Data.Attack = 4;
                    break;
                case 12:
                    ViewModel.Data.Speed = 3;
                    ViewModel.Data.ExperienceTotal = 100000;
                    ViewModel.Data.Defense = 6;
                    ViewModel.Data.Attack = 4;
                    break;
                case 13:
                    ViewModel.Data.Speed = 3;
                    ViewModel.Data.ExperienceTotal = 120000;
                    ViewModel.Data.Defense = 7;
                    ViewModel.Data.Attack = 4;
                    break;
                case 14:
                    ViewModel.Data.Speed = 3;
                    ViewModel.Data.ExperienceTotal = 140000;
                    ViewModel.Data.Defense = 7;
                    ViewModel.Data.Attack = 5;
                    break;
                case 15:
                    ViewModel.Data.Speed = 4;
                    ViewModel.Data.ExperienceTotal = 165000;
                    ViewModel.Data.Defense = 7;
                    ViewModel.Data.Attack = 5;
                    break;
                case 16:
                    ViewModel.Data.Speed = 4;
                    ViewModel.Data.ExperienceTotal = 195000;
                    ViewModel.Data.Defense = 8;
                    ViewModel.Data.Attack = 5;
                    break;
                case 17:
                    ViewModel.Data.Speed = 4;
                    ViewModel.Data.ExperienceTotal = 225000;
                    ViewModel.Data.Defense = 8;
                    ViewModel.Data.Attack = 5;
                    break;
                case 18:
                    ViewModel.Data.Speed = 4;
                    ViewModel.Data.ExperienceTotal = 265000;
                    ViewModel.Data.Defense = 8;
                    ViewModel.Data.Attack = 6;
                    break;
                case 19:
                    ViewModel.Data.Speed = 4;
                    ViewModel.Data.ExperienceTotal = 305000;
                    ViewModel.Data.Defense = 9;
                    ViewModel.Data.Attack = 7;
                    break;
                case 20:
                    ViewModel.Data.Speed = 5;
                    ViewModel.Data.ExperienceTotal = 355000;
                    ViewModel.Data.Defense = 10;
                    ViewModel.Data.Attack = 8;
                    break;

            }
        }




    }
}