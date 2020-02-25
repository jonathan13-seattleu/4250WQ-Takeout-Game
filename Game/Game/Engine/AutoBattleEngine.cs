﻿using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;
using System.Threading.Tasks;

namespace Game.Engine
{
    /// <summary>
    /// AutoBattle Engine
    /// 
    /// Runs the engine simulation with no user interaction
    /// 
    /// </summary>
    public class AutoBattleEngine
    {
        #region Algrorithm
        // Prepare for Battle
        // Pick 6 Characters
        // Initialize the Battle
        // Start a Round

        // Fight Loop
        // Loop in the round each turn
        // If Round is over, Start New Round
        // Check end state of round/game

        // Wrap up
        // Get Score
        // Save Score
        // Output Score
        #endregion Algrorithm

        #region ScoreInformation
        public int GetScoreValue() { return 0; }
        public ScoreModel GetScoreObject() { return new ScoreModel(); }
        public int GetRoundsValue() { return 0; }
        public string GetResultsOutput() { return "done"; }
        #endregion ScoreInformation

        #region RunAutoBattleFight
        public async Task<bool> RunAutoBattle()
        {
            // Auto Battle, does all the steps that a human would do.

            // Perpare for Battle
            var Engine = new BattleEngine();

            // Picks 6 Characters
            Engine.PopulateCharacterList();

            // Start Battle in AutoBattle mode
            Engine.StartBattle(true);

            // Initialize the Rounds
            Engine.NewRound();

            // Fight

            // Fight Loop. Continue until Game is Over...
            bool GameContinueCondition = false;
            do
            {
                // Do the turn...
                // If the round is over start a new one...
            } while (GameContinueCondition);

            // Wrap up
            Engine.EndBattle();

            return true;
        }
        #endregion RunAutoBattleFight
    }
}