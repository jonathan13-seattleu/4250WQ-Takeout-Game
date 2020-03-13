using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using Game.Models;
using System.Diagnostics;
using System.Threading.Tasks;

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

			//EngineViewModel.Engine.MonsterList.Add(new PlayerInfoModel )
			

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

			//Grab the Image URI for the Monsters
			var tempMonsterList = EngineViewModel.Engine.MonsterList;
			var monsterOneImage = tempMonsterList[0].ImageURI;
			var monsterTwoImage = tempMonsterList[1].ImageURI;
			var monsterThreeImage = tempMonsterList[2].ImageURI;
			var monsterFourImage = tempMonsterList[3].ImageURI;
			var monsterFiveImage = tempMonsterList[4].ImageURI;
			var monsterSixImage = tempMonsterList[5].ImageURI;

			//Set Monster Image URIs for selected Monsters
			MonsterOneGrid.Source = monsterOneImage;
			MonsterTwoGrid.Source = monsterTwoImage;
			MonsterThreeGrid.Source = monsterThreeImage;
			MonsterFourGrid.Source = monsterFourImage;
			MonsterFiveGrid.Source = monsterFiveImage;
			MonsterSixGrid.Source = monsterSixImage;

			//Grab health for the selected monsters
			var monsterOneHealth = tempMonsterList[0].GetCurrentHealth();
			var monsterTwoHealth = tempMonsterList[1].GetCurrentHealth();
			var monsterThreeHealth = tempMonsterList[2].GetCurrentHealth();
			var monsterFourHealth = tempMonsterList[3].GetCurrentHealth();
			var monsterFiveHealth = tempMonsterList[4].GetCurrentHealth();
			var monsterSixHealth = tempMonsterList[5].GetCurrentHealth();

			//Set health of selected monsters.
			MonsterOneHealth.Text = monsterOneHealth.ToString();
			MonsterTwoHealth.Text = monsterTwoHealth.ToString();
			MonsterThreeHealth.Text = monsterThreeHealth.ToString();
			MonsterFourHealth.Text = monsterFourHealth.ToString();
			MonsterFiveHealth.Text = monsterFiveHealth.ToString();
			MonsterSixHealth.Text = monsterSixHealth.ToString();
		}

		/// <summary>
		/// Draws the Game Board Attacker and Defender
		/// </summary>
		public void DrawGameBoardAttackerDefender()
		{
			//DrawGameBoardClear();

			if (EngineViewModel.Engine.CurrentAttacker != null)
			{
				AttackerImage.Source = "icon_new.png";
				AttackerName.Text = "Attacker";
				AttackerHealth.Text = "Attacker Health";
			}

			if (EngineViewModel.Engine.CurrentDefender != null)
			{
				DefenderImage.Source = "icon_new.png";
				DefenderName.Text = "Defender";
				DefenderHealth.Text = "Defender Health";


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
		public void AttackButton_Clicked(object sender, EventArgs e)
		{
			EngineViewModel.Engine.BattleStateEnum = BattleStateEnum.Battling;

			// Get the turn, set the current player and attacker to match
			SetAttackerAndDefender();

			// Hold the current state
			var RoundCondition = EngineViewModel.Engine.RoundNextTurn();

			// Output the Message of what happened.
			GameMessage();

			// Show the outcome on the Board
			DrawGameAttackerDefenderBoard();

			if (RoundCondition == RoundEnum.NewRound)
			{
				EngineViewModel.Engine.BattleStateEnum = BattleStateEnum.NewRound;

				// Pause
				Task.Delay(WaitTime);

				Debug.WriteLine("New Round");

				// Show the Round Over, after that is cleared, it will show the New Round Dialog
				ShowModalRoundOverPage();
				return;
			}

			// Check for Game Over
			if (RoundCondition == RoundEnum.GameOver)
			{
				EngineViewModel.Engine.BattleStateEnum = BattleStateEnum.GameOver;

				// Wrap up
				EngineViewModel.Engine.EndBattle();

				// Pause
				Task.Delay(WaitTime);

				Debug.WriteLine("Game Over");

				GameOver();
				return;
			}

		}

		/// <summary>
		/// Game is over
		/// 
		/// Show Buttons
		/// 
		/// Clean up the Engine
		/// 
		/// Show the Score
		/// 
		/// Clear the Board
		/// 
		/// </summary>
		public async void GameOver()
		{
			// Save the Score to the Score View Model, by sending a message to it.
			var Score = EngineViewModel.Engine.BattleScore;
			MessagingCenter.Send(this, "AddData", Score);
			await Navigation.PushModalAsync(new ScorePage());
		}

		/// <summary>
		/// Show the Round Over page
		/// 
		/// Round Over is where characters get items
		/// 
		/// </summary>
		public async void ShowModalRoundOverPage()
		{
			await Navigation.PushModalAsync(new NewRoundPage());
		}

		/// <summary>
		/// Draw the UI for
		///
		/// Attacker vs Defender Mode
		/// 
		/// </summary>
		public void DrawGameAttackerDefenderBoard()
		{
			AttackerHealth.Text = EngineViewModel.Engine.CurrentAttacker.GetCurrentHealthTotal.ToString() + " / " + EngineViewModel.Engine.CurrentAttacker.GetMaxHealthTotal.ToString();
			DefenderHealth.Text = EngineViewModel.Engine.CurrentDefender.GetCurrentHealthTotal.ToString() + " / " + EngineViewModel.Engine.CurrentDefender.GetMaxHealthTotal.ToString();

		}

		/// <summary>
		/// Decide The Turn and who to Attack
		/// </summary>
		public void SetAttackerAndDefender()
		{
			EngineViewModel.Engine.CurrentAttacker = EngineViewModel.Engine.GetNextPlayerTurn();
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

			switch (EngineViewModel.Engine.CurrentAttacker.PlayerType)
			{
				case PlayerTypeEnum.Character:
					// User would select who to attack

					// for now just auto selecting
					EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);
					break;

				case PlayerTypeEnum.Monster:
				default:

					// Monsters turn, so auto pick a Character to Attack
					EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);
					break;
			}
		}

		/// <summary>
		/// Builds up the output message
		/// </summary>
		/// <param name="message"></param>
		public void GameMessage()
		{
			ClearMessages();

			AttackResult.IsVisible = true;
			
			// Output The Message that happened.
			AttackResult.Text = string.Format("{0} \n{1}", EngineViewModel.Engine.BattleMessagesModel.TurnMessage, AttackResult.Text);

			Debug.WriteLine(AttackResult.Text);

			if (!string.IsNullOrEmpty(EngineViewModel.Engine.BattleMessagesModel.LevelUpMessage))
			{
				AttackResult.Text = string.Format("{0} \n{1}", EngineViewModel.Engine.BattleMessagesModel.LevelUpMessage, AttackResult.Text);
			}

			//htmlSource.Html = EngineViewModel.Engine.BattleMessagesModel.GetHTMLFormattedTurnMessage();
			//HtmlBox.Source = HtmlBox.Source = htmlSource;
		}

		/// <summary>
		///  Clears the messages on the UX
		/// </summary>
		public void ClearMessages()
		{
			AttackResult.Text = "";
			htmlSource.Html = EngineViewModel.Engine.BattleMessagesModel.GetHTMLBlankMessage();
			//HtmlBox.Source = htmlSource;
		}

		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void RoundOverButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new RoundOverPage());
		}


		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void NewRoundButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NewRoundPage());
		}
		

		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void BattleOverButton_Clicked(object sender, EventArgs e)
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
		public async void ExitButton_Clicked(object sender, EventArgs e)
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
		public async void QuitButton_Clicked(object sender, EventArgs e)
		{
			bool answer = await DisplayAlert("Battle", "Are you sure you want to Quit?", "Yes", "No");

			if (answer)
			{
				await Navigation.PopModalAsync();
			}
		}
	}
}