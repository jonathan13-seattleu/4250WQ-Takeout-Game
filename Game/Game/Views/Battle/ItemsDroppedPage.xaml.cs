using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsDroppedPage : ContentPage
    {
        //ViewModel used for items
        readonly ItemIndexViewModel ViewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemsDroppedPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = ItemIndexViewModel.Instance;
        }


        /// <summary>
        /// This function will automatically assign the dropped items to the characters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoAssignButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Assiging Items", "Assigning Items", "Ok");
        }

    }
}