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
	public partial class ScorePage: ContentPage
	{
		public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

		/// <summary>
		/// Constructor
		/// </summary>
		public ScorePage ()
		{
			InitializeComponent ();
			RoundCount.Text = EngineViewModel.Engine.BattleScore.MonsterModelDeathList.Count().ToString();
			TurnCount.Text = EngineViewModel.Engine.BattleScore.TurnCount.ToString();
			RoundCount.Text = EngineViewModel.Engine.BattleScore.RoundCount.ToString();
			ExperienceGainedTotal.Text = EngineViewModel.Engine.BattleScore.ExperienceGainedTotal.ToString();
		}

		/// <summary>
		/// Attack Action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void HomeScreenButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new HomePage()));
		}
	}
}