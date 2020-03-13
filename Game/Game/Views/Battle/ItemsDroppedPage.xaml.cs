using Game.ViewModels;
using System;
using System.Linq;
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
            getItemDetails();
        }
        
        public void getItemDetails()
        {
            foreach(var data in BattleEngineViewModel.Instance.Engine.BattleScore.ItemModelDropList.Distinct())
            {
                ItemImage.Source = data.ImageURI;
                ItemName.Text = data.Name;
                ItemValue.Text = data.Value.ToString();
                ItemAttribute.Text = data.Attribute.ToString();
            }
        }


        /// <summary>
        /// This function will automatically assign the dropped items to the characters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AutoAssignButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.PickupItemsForAllCharacters();
            BattleEngineViewModel.Instance.Engine.NewRound();
            await Navigation.PopModalAsync();
        }

    }
}