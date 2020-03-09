using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game;
using Game.Views;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;

namespace UnitTests.Views
{
    [TestFixture]
    public class BattlePageTests
    {
        App app;
        BattlePage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new BattlePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void BattlePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattlePage_AttackButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.AttackButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void BattlePage_ExitButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ExitButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }



        
        /*
        [Test]
        public void BattlePage_ClearMessages_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ClearMessages();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }*/

        /*[Test]
        public void BattlePage_GameMessage_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.GameMessage();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }*/

        /*[Test]
        public void BattlePage_GameMessage_LevelUp_Default_Should_Pass()
        {
            // Arrange
            BattleEngineViewModel.Instance.Engine.BattleMessagesModel.LevelUpMessage = "me";

            // Act
            page.GameMessage();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }*/
        [Test]
        public void BattlePage_DrawGameBoardAttackerDefender_CurrentAttacker_Null_CurrentDefender_Null_Should_Pass()
        {
            // Arrange
            page.EngineViewModel.Engine.CurrentAttacker = null;
            page.EngineViewModel.Engine.CurrentDefender = null;

            // Act
            page.DrawGameBoardAttackerDefender();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_DrawGameBoardAttackerDefender_CurrentAttacker_InValid_Null_Should_Pass()
        {
            // Arrange

            var PlayerInfo = new PlayerInfoModel(new CharacterModel());

            page.EngineViewModel.Engine.CurrentAttacker = PlayerInfo;
            page.EngineViewModel.Engine.CurrentDefender = null;

            // Act
            page.DrawGameBoardAttackerDefender();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_DrawGameBoardAttackerDefender_CurrentDefender_InValid_Null_Should_Pass()
        {
            // Arrange

            var PlayerInfo = new PlayerInfoModel(new CharacterModel());

            page.EngineViewModel.Engine.CurrentAttacker = null;
            page.EngineViewModel.Engine.CurrentDefender = PlayerInfo;

            // Act
            page.DrawGameBoardAttackerDefender();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_DrawGameBoardAttackerDefender_CurrentDefender_Valid_Should_Pass()
        {
            // Arrange

            page.EngineViewModel.Engine.CurrentAttacker = new PlayerInfoModel(new CharacterModel());
            page.EngineViewModel.Engine.CurrentDefender = new PlayerInfoModel(new CharacterModel { Alive=false });

            // Act
            page.DrawGameBoardAttackerDefender();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        /*[Test]
        public void BattlePage_NextAttackExample_NextRound_Should_Pass()
        {
            // Arrange

            page.EngineViewModel.Engine.CharacterList.Add(new PlayerInfoModel(new CharacterModel()));

            page.EngineViewModel.Engine.MonsterList.Clear();

            page.EngineViewModel.Engine.MakePlayerList();

            // Has no monster, so should show next round.

            // Act
            page.NextAttackExample();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }*/

        /*[Test]
        public void BattlePage_NextAttackExample_GameOver_Should_Pass()
        {
            // Arrange

            page.EngineViewModel.Engine.CharacterList.Clear();
            page.EngineViewModel.Engine.MonsterList.Clear();
            page.EngineViewModel.Engine.PlayerList.Clear();

            page.EngineViewModel.Engine.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));

            page.EngineViewModel.Engine.MakePlayerList();

            // Has no Character, so should show end game

            // Act
            page.NextAttackExample();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }*/

        /*[Test]
        public void BattlePage_SetAttackerAndDefender_Character_vs_Monster_Should_Pass()
        {
            // Arrange
            page.EngineViewModel.Engine.CharacterList.Clear();
            page.EngineViewModel.Engine.MonsterList.Clear();
            page.EngineViewModel.Engine.PlayerList.Clear();

            // Make Character
            page.EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 100,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            page.EngineViewModel.Engine.CharacterList.Add(CharacterPlayer);

            // Make Monster

            page.EngineViewModel.Engine.MaxNumberPartyMonsters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = -1,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            page.EngineViewModel.Engine.PlayerList.Add(CharacterPlayer);
            page.EngineViewModel.Engine.PlayerList.Add(MonsterPlayer);

            page.EngineViewModel.Engine.CurrentAttacker = MonsterPlayer;

            page.EngineViewModel.Engine.CurrentAttacker = null;

            // Act
            page.SetAttackerAndDefender();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }*/

        /*[Test]
        public void BattlePage_SetAttackerAndDefender_Monster_vs_Character_Should_Pass()
        {
            // Arrange

            page.EngineViewModel.Engine.CharacterList.Clear();
            page.EngineViewModel.Engine.MonsterList.Clear();
            page.EngineViewModel.Engine.PlayerList.Clear();

            // Make Character
            page.EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            page.EngineViewModel.Engine.CharacterList.Add(CharacterPlayer);

            // Make Monster

            page.EngineViewModel.Engine.MaxNumberPartyMonsters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = 100,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            page.EngineViewModel.Engine.MonsterList.Add(MonsterPlayer);

            page.EngineViewModel.Engine.PlayerList.Add(CharacterPlayer);
            page.EngineViewModel.Engine.PlayerList.Add(MonsterPlayer);

            page.EngineViewModel.Engine.CurrentAttacker = CharacterPlayer;

            // Act
            page.SetAttackerAndDefender();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }*/

        /*[Test]
        public void BattlePage_SetAttackerAndDefender_Character_vs_Unknown_Should_Pass()
        {
            // Arrange
            page.EngineViewModel.Engine.CharacterList.Clear();
            page.EngineViewModel.Engine.MonsterList.Clear();
            page.EngineViewModel.Engine.PlayerList.Clear();

            // Make Character
            page.EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            page.EngineViewModel.Engine.CharacterList.Add(CharacterPlayer);

            // Make Monster

            page.EngineViewModel.Engine.MaxNumberPartyMonsters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = 100,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                                PlayerType = PlayerTypeEnum.Unknown
                            });

            page.EngineViewModel.Engine.MonsterList.Add(MonsterPlayer);

            page.EngineViewModel.Engine.PlayerList.Add(CharacterPlayer);
            page.EngineViewModel.Engine.PlayerList.Add(MonsterPlayer);

            page.EngineViewModel.Engine.CurrentAttacker = CharacterPlayer;

            // Act
            page.SetAttackerAndDefender();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }*/

        /*[Test]
        public void BattlePage_GameOver_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.GameOver();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }*/
        
    }
}