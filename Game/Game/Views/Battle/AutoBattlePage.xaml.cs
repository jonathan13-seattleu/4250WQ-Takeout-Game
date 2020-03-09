﻿using Game.Engine;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AutoBattlePage : ContentPage
	{
		// Hold the Engine, so it can be swapped out for unit testing
		public AutoBattleEngine Engine = new AutoBattleEngine();

		/// <summary>
		/// Constructor
		/// </summary>
		public AutoBattlePage()
		{
			InitializeComponent();
		}

		public async void AutobattleButton_Clicked(object sender, EventArgs e)
		{
			// Call into Auto Battle from here to do the Battle...
			var Engine = new Game.Engine.AutoBattleEngine();

			string BattleMessage = "";

			var result = await Engine.RunAutoBattle();

			var Score = Engine.GetScoreObject();

			BattleMessage = string.Format("Done {0} Rounds", Score.RoundCount);

			// Error Occured
			if (result == false)
			{
				BattleMessage = "Error Occured";
			}

			BattleMessageValue.Text = BattleMessage;
		}
	}
}