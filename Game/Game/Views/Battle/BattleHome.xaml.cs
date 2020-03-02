using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleHome : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public BattleHome()
		{
			InitializeComponent();
		}

		async private void CharacterButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PickCharactersPage());
		}
		async private void BattleButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new BattlePage());
		}

		async private void ItemPool_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ItemsDroppedPage());
		}
	}
}