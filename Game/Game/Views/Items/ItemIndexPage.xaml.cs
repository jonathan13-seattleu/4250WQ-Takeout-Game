using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using Game.Services;

namespace Game.Views
{
    /// <summary>
    /// Index Page
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)]
    public partial class ItemIndexPage : ContentPage
    {
        // The view model, used for data binding
        public readonly ItemIndexViewModel ViewModel;
        // Empty Constructor for UTs
        public ItemIndexPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the ItemIndexView Model
        /// </summary>
        public ItemIndexPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = ItemIndexViewModel.Instance;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushAsync(new ItemReadPage(new GenericViewModel<ItemModel>(data)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        /// <summary>
        /// Call to Add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       public async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemCreatePage()));
        }

        /// <summary>
        /// Refresh the list on page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            // If no data, then set it for needing refresh
            if (ViewModel.Dataset.Count == 0)
            {
                ViewModel.SetNeedsRefresh(true);
            }

            // If the needs Refresh flag is set update it
            if (ViewModel.NeedsRefresh())
            {
                ViewModel.LoadDatasetCommand.Execute(null);
            }

            BindingContext = ViewModel;
        }

        private async void GetButton_ClickedAsync(object sender, EventArgs e)
        {
            var result = await GetItemsGet();
            await DisplayAlert("Returned List", result, "OK");
        }

        private async void PostItem_Clicked(object sender, EventArgs e)
        {
            var result = await GetItemsPost();
            await DisplayAlert("Returned List", result, "OK");

        }

        /// <summary>
        /// Get Items using the HTTP Post command
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetItemsPost()
        {
            var result = "No Results";
            var dataList = new List<ItemModel>();

            var number = Convert.ToInt32(1);
            var level = 6;  // Max Value of 6
            var attribute = AttributeEnum.Unknown;  // Any Attribute
            var location = ItemLocationEnum.Unknown;    // Any Location
            var random = true;  // Random between 1 and Level
            var updateDataBase = true;  // Add them to the DB
            var category = 0;   // What category to filter down to, 0 is all

            // will return shoes value 10 of speed.
            // Example  result = await ItemsController.Instance.GetItemsFromGame(1, 10, AttributeEnum.Speed, ItemLocationEnum.Feet, false, true);
            dataList = await ItemService.GetItemsFromServerPostAsync(number, level, attribute, location, category, random, updateDataBase);

            if (dataList == null)
            {
                return result;
            }

            if (dataList.Count == 0)
            {
                return result;
            }

            // Reset the output
            result = "";

            foreach (var ItemModel in dataList)
            {
                // Add them line by one, use \n to force new line for output display.
                result += ItemModel.FormatOutput() + "\n";
            }

            return result;
        }


        /// <summary>
        /// Call the server call for Get Items using HTTP Get
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetItemsGet()
        {
            // Call to the ItemModel Service and have it Get the Items
            // The ServerItemValue Code stands for the batch of items to get
            // as the group to request.  1, 2, 3, 100 (All), or if not specified All

            var result = "No Results";

            var value = Convert.ToInt32(1);
            var dataList = await Services.ItemService.GetItemsFromServerGetAsync(value);

            if (dataList == null)
            {
                return result;
            }

            if (dataList.Count == 0)
            {
                return result;
            }

            // Reset the output
            result = "";

            foreach (var ItemModel in dataList)
            {
                // Add them line by one, use \n to force new line for output display.
                // Build up the output string by adding formatted ItemModel Output
                result += ItemModel.FormatOutput() + "\n";
            }

            return result;
        }

    }
}