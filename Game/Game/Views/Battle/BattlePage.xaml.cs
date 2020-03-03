using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage: ContentPage
	{
		// This uses the Instance so it can be shared with other Battle Pages as needed
		public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

		#region PageHandelerVariables
		// Hold the Selected Characters
		public PickCharactersPage ModalPickCharactersPage;

		// Hold the New Round Page where monsters are shown
		public NewRoundPage ModalNewRoundPage;

		// Hold Round Over Page
		public RoundOverPage ModalRoundOverPage;

		// Hold the Game Over Page where the Final Score is shown
		public ScorePage ModalShowScorePage;

		// HTML Formatting for message output box
		public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

		// Wait time before proceeding
		public int WaitTime = 1500;

		#endregion PageHandelerVariables

		/// <summary>
		/// Constructor
		/// </summary>
		public BattlePage ()
		{
			InitializeComponent ();
			// Set up the UI to Defaults
			BindingContext = EngineViewModel;
			
			// Start the Battle Engine
			EngineViewModel.Engine.StartBattle(false);

			// Ask the Game engine to select who goes first
			EngineViewModel.Engine.CurrentAttacker = null;
			EngineViewModel.Engine.CurrentAttacker = EngineViewModel.Engine.GetNextPlayerTurn();

			// Let them select the one they want to attack
			EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);

			// Show the New Round Screen
			//ShowModalNewRoundPage();

			// Add Players to Display
			DrawGameBoardAttackerDefender();

			//Grab Image URIs for the selcted character list.
			var tempList = EngineViewModel.Engine.CharacterList;
			var characterOneImage = tempList[0].ImageURI;
			var characterTwoImage = tempList[1].ImageURI;
			var characterThreeImage = tempList[2].ImageURI;
			var characterFourImage = tempList[3].ImageURI;
			var characterFiveImage = tempList[4].ImageURI;
			var characterSixImage = tempList[5].ImageURI;

			//Set images for the character columns
			CharacterOneGrid.Source = characterOneImage;
			CharacterTwoGrid.Source = characterTwoImage;
			CharacterThreeGrid.Source = characterThreeImage;
			CharacterFourGrid.Source = characterFourImage;
			CharacterFiveGrid.Source = characterFiveImage;
			CharacterSixGrid.Source = characterSixImage;

			//Grab selected players current health
			var characterOneHealth = tempList[0].GetCurrentHealth();
			var characterTwoHealth = tempList[1].GetCurrentHealth();
			var characterThreeHealth = tempList[2].GetCurrentHealth();
			var characterFourHealth = tempList[3].GetCurrentHealth();
			var characterFiveHealth = tempList[4].GetCurrentHealth();
			var characterSixHealth = tempList[5].GetCurrentHealth();
			//Set current health text for the selected characters
			CharacterOneHealth.Text = characterOneHealth.ToString();
			CharacterTwoHealth.Text = characterTwoHealth.ToString();
			CharacterThreeHealth.Text = characterThreeHealth.ToString();
			CharacterFourHealth.Text = characterFourHealth.ToString();
			CharacterFiveHealth.Text = characterFiveHealth.ToString();
			CharacterSixHealth.Text = characterSixHealth.ToString();

			var tempMonsterList = EngineViewModel.Engine.MonsterList;
			var MonsterOneImage = tempMonsterList[0].ImageURI;
			var MonsterTwoImage = tempMonsterList[1].ImageURI;
			var MonsterThreeImage = tempMonsterList[2].ImageURI;
			var MonsterFourImage = tempMonsterList[3].ImageURI;
			var MonsterFiveImage = tempMonsterList[4].ImageURI;
			var MonsterSixImage = tempMonsterList[5].ImageURI;

			MonsterOneGrid.Source = MonsterOneImage;
			MonsterTwoGrid.Source = MonsterTwoImage;
			MonsterThreeGrid.Source = MonsterThreeImage;
			MonsterFourGrid.Source = MonsterFourImage;
			MonsterFiveGrid.Source = MonsterFiveImage;
			MonsterSixGrid.Source = MonsterSixImage;


		}

		/// <summary>
		/// Draws the Game Board Attacker and Defender
		/// </summary>
		public void DrawGameBoardAttackerDefender()
		{
			//DrawGameBoardClear();

			if (EngineViewModel.Engine.CurrentAttacker != null)
			{
				AttackerImage.Source = EngineViewModel.Engine.CurrentAttacker.ImageURI;
				AttackerName.Text = EngineViewModel.Engine.CurrentAttacker.Name;
				AttackerHealth.Text = EngineViewModel.Engine.CurrentAttacker.GetCurrentHealthTotal.ToString() + " / " + EngineViewModel.Engine.CurrentAttacker.GetMaxHealthTotal.ToString();
			}

			if (EngineViewModel.Engine.CurrentDefender != null)
			{
				DefenderImage.Source = EngineViewModel.Engine.CurrentDefender.ImageURI;
				DefenderName.Text = EngineViewModel.Engine.CurrentDefender.Name;
				DefenderHealth.Text = EngineViewModel.Engine.CurrentDefender.GetCurrentHealthTotal.ToString() + " / " + EngineViewModel.Engine.CurrentDefender.GetMaxHealthTotal.ToString();


			}

		}

		/// <summary>
		/// Attack Action
		/// When the Attack button is pressed, the battle engine will be called. Based on the result of the attack,
		/// the result will be resiplayed in the AttackResult text field. In addition, if a character or monster dies,
		/// that text field will become visible. A character leveling up will also result in that text field becomming
		/// visible.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void AttackButton_Clicked(object sender, EventArgs e)
		{
			//Battle engine will be called and appropriate text will be displayed based on the result.
			DisplayAlert("Temporary Attack dialog for battle", "Attack !!!", "OK");
			AttackResult.Text = "The result of the attack";
			AttackResult.IsVisible = true;
			MonsterDied.IsVisible = true;
			CharacterDied.IsVisible = true;
			CharacterLevelUp.IsVisible = true;
			
		}

		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void RoundOverButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new RoundOverPage());
		}


		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void NewRoundButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NewRoundPage());
		}
		

		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void BattleOverButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new ScorePage());
		}

		/// <summary>
		/// Battle Over, so Exit Button
		/// Need to show this for the user to click on.
		/// The Quit does a prompt, exit just exits
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void ExitButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Quit the Battle
		/// 
		/// Quitting out
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void QuitButton_Clicked(object sender, EventArgs e)
		{
			bool answer = await DisplayAlert("Battle", "Are you sure you want to Quit?", "Yes", "No");

			if (answer)
			{
				await Navigation.PopModalAsync();
			}
		}
	}
}