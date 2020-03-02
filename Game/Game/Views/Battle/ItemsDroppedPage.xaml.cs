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



        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Quit the Battle
        /// 
        /// Quitting out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    }
}