using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using Game.Helpers;
using System.Linq;
using Game.ViewModels;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        AutoBattleEngine AutoBattleEngine;
        BattleEngine BattleEngine;

        [SetUp]
        public void Setup()
        {
            AutoBattleEngine = EngineViewModel.AutoBattleEngine;
            BattleEngine = EngineViewModel.Engine;

        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void HakathonScenario_Constructor_0_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task HackathonScenario_Scenario_1_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters


            //Act
            var result = await AutoBattleEngine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, AutoBattleEngine.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, AutoBattleEngine.BattleScore.RoundCount);
        }

        [Test]
        public void HackathonScenario_Scenario_2_Character_Bob_Should_Miss()
        {
            /* 
             * Scenario Number:  
             *  2
             *  
             * Description: 
             *      Make a Character called Bob
             *      Bob Always Misses
             *      Other Characters Always Hit
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Turn Engine
             *      Changed TurnAsAttack method
             *      Check for Name of Bob and return miss
             *                 
             * Test Algrorithm:
             *  Create Character named Bob
             *  Create Monster
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with Character of Named Bob
             *  Test with Character of any other name
             * 
             * Validation:
             *      Verify Enum is Miss
             *  
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 10,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                ExperienceRemaining = 1,
                                Name = "Bob",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Have dice rull 19
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(19);

            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Miss, BattleEngine.BattleMessagesModel.HitStatus);
        }

        [Test]
        public void HackathonScenario_Scenario_2_Character_Not_Bob_Should_Hit()
        {
            /* 
             * Scenario Number:  
             *      2
             *      
             * Description: 
             *      See Default Test
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Defualt Test
             *                 
             * Test Algrorithm:
             *      Create Character named Mike
             *      Create Monster
             *      Call TurnAsAttack so Mike can attack Monster
             * 
             * Test Conditions:
             *      Control Dice roll so natural hit
             *      Test with Character of not named Bob
             *  
             *  Validation
             *      Verify Enum is Hit
             *      
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 10,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Hit, BattleEngine.BattleMessagesModel.HitStatus);
        }

        [Test]
        public void HackathonScenario_Scenario_30_First_Character_Should_Have_Buff()
        {
            /* 
             * Scenario Number:  
             *      30
             *      
             * Description: 
             *      The first player in the player list gets their base attack, speed, defense 
             *      values buffed by 2X for the first time they are first in the list.
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Defualt Test
             *                 
             * Test Algrorithm:
             *      Create 2 Characters named Bugs and Michael.
             *      Add them to the Player List.
             *      Order the Player List. 
             * Test Conditions:
             *      Control Dice roll so natural hit
             *      Test with Character of not named Bob
             *  
             *  Validation
             *      Verify Enum is Hit
             *      
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 2;
            int tempSpeed = 200;
            int tempAttack = 1;
            int tempDefense = 1;

            var CharacterPlayer1 = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 1,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                ExperienceRemaining = 1,
                                Defense = 1,
                                Attack = 1,
                                Name = "Bugs",
                            });
            var CharacterPlayer2 = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 200,
                    Level = 1,
                    CurrentHealth = 100,
                    ExperienceTotal = 100,
                    ExperienceRemaining = 1,
                    Attack = 1,
                    Defense = 1,
                    Name = "Michael",
                });

            BattleEngine.PlayerList.Add(CharacterPlayer1);
            BattleEngine.PlayerList.Add(CharacterPlayer2);

            // Set Monster Conditions



            //Act
            var result = BattleEngine.OrderPlayerListByTurnOrder();

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(((tempSpeed + result[0].GetSpeedLevelBonus) * 2), result[0].Speed);
            Assert.AreEqual(((tempAttack + result[0].GetAttackLevelBonus) * 2), result[0].Attack);
            Assert.AreEqual(((tempDefense + result[0].GetDefenseLevelBonus) * 2), result[0].Defense);
        }

        [Test]
        public void HackathonScenario_Scenario_30_Second_Character_Should__Not_Have_Buff()
        {
            /* 
             * Scenario Number:  
             *      30
             *      
             * Description: 
             *      The first player in the player list gets their base attack, speed, defense 
             *      values buffed by 2X for the first time they are first in the list.
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Defualt Test
             *                 
             * Test Algrorithm:
             *      Create 2 Characters named Bugs and Michael.
             *      Add them to the Player List.
             *      Order the Player List. 
             * Test Conditions:
             *      Control Dice roll so natural hit
             *      Test with Character of not named Bob
             *  
             *  Validation
             *      Verify Enum is Hit
             *      
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 2;
            int tempSpeed = 200;
            int tempAttack = 1;
            int tempDefense = 1;

            var CharacterPlayer1 = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 1,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                ExperienceRemaining = 1,
                                Defense = 1,
                                Attack = 1,
                                Name = "Bugs",
                            });
            var CharacterPlayer2 = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 200,
                    Level = 1,
                    CurrentHealth = 100,
                    ExperienceTotal = 100,
                    ExperienceRemaining = 1,
                    Attack = 1,
                    Defense = 1,
                    Name = "Michael",
                });

            BattleEngine.PlayerList.Add(CharacterPlayer1);
            BattleEngine.PlayerList.Add(CharacterPlayer2);

            //Act
            var result = BattleEngine.OrderPlayerListByTurnOrder();

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(((tempSpeed + result[1].GetSpeedLevelBonus)), result[1].Speed);
            Assert.AreEqual(((tempAttack + result[1].GetAttackLevelBonus)), result[1].Attack);
            Assert.AreEqual(((tempDefense + result[1].GetDefenseLevelBonus)), result[1].Defense);
        }

        [Test]
        public void HackathonScenario_Scenario_32_First_Player_Should_Be_Character()
        {
            /* 
             * Scenario Number:  
             *      32
             *      
             * Description: 
             *      Every 5th round, the sort order for turn order changes and list is sorted by Characters first, 
             *      then lowest health, then lowest speed.
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Default Test
             *                 
             * Test Algrorithm:
             *      Create 1 Character and 1 Monster.
             *      Add them to the Player List.
             *      Order the Player List. 
             * Test Conditions:
             *      Test with Characters with low speed and Monsters with high speed.
             *      RoundCount is 5.
             *  
             *  Validation
             *      Verify Characters come before Monsters in the PlayerList.
             *      
             */

            //Arrange

            // Set Round Count

            BattleEngine.BattleScore.RoundCount = 5;

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 2;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 100,
                                Level = 1,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                ExperienceRemaining = 1,
                                Defense = 1,
                                Attack = 1,
                                Name = "Bugs",
                            });

            BattleEngine.PlayerList.Add(CharacterPlayer);

            // Set Monster Conditions

            var MonsterPlayer = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = 500,
                                Level = 1,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                ExperienceRemaining = 1,
                                Defense = 1,
                                Attack = 1,
                                Name = "Daffy",
                            });

            BattleEngine.PlayerList.Add(MonsterPlayer);

            //Act
            var result = BattleEngine.OrderPlayerListByTurnOrder();

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(PlayerTypeEnum.Character, result[0].PlayerType);
        }

        [Test]
        public void HackathonScenario_Scenario_32_First_Player_Should_Not_Be_Character()
        {
            /* 
             * Scenario Number:  
             *      32
             *      
             * Description: 
             *      Every 5th round, the sort order for turn order changes and list is sorted by Characters first, 
             *      then lowest health, then lowest speed.
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Default Test
             *                 
             * Test Algrorithm:
             *      Create 1 Character and 1 Monster.
             *      Add them to the Player List.
             *      Order the Player List. 
             * Test Conditions:
             *      Test with Characters with low speed and Monsters with high speed.
             *      RoundCount is 5.
             *  
             *  Validation
             *      Verify Monsters come before Characters in the PlayerList.
             *      
             */

            //Arrange

            // Set Round Count

            BattleEngine.BattleScore.RoundCount = 4;

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 2;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 100,
                                Level = 1,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                ExperienceRemaining = 1,
                                Defense = 1,
                                Attack = 1,
                                Name = "Bugs",
                            });

            BattleEngine.PlayerList.Add(CharacterPlayer);

            // Set Monster Conditions

            var MonsterPlayer = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = 500,
                                Level = 1,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                ExperienceRemaining = 1,
                                Defense = 1,
                                Attack = 1,
                                Name = "Daffy",
                            });

            BattleEngine.PlayerList.Add(MonsterPlayer);

            //Act
            var result = BattleEngine.OrderPlayerListByTurnOrder();

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(PlayerTypeEnum.Monster, result[0].PlayerType);
        }
        [Test]
        public void HackathonScenario_Scenario_31_Monsters_Should_Be_Buffed()
        {
            /* 
             * Scenario Number:  
             *      31
             *      
             * Description: 
             *      After round 100, the monster's attributes should be buffed 10x what they normally are. 
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Default Test
             *                 
             * Test Algrorithm:
             *      Create 1 Character and 1 Monster.
             *      Add them to the Player List.
             *      Order the Player List. 
             * Test Conditions:
             *      Test with Characters with low speed and Monsters with high speed.
             *      RoundCount is 5.
             *  
             *  Validation
             *      Verify Monsters come before Characters in the PlayerList.
             *      
             */

            //Arrange

            // Set Round Count
            int tempSpeed = 200;
            int tempAttack = 1;
            int tempDefense = 1;

            BattleEngine.BattleScore.RoundCount = 100;

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 4;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 1,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                ExperienceRemaining = 1,
                                Defense = 1,
                                Attack = 1,
                                Name = "Bugs",
                            });

            BattleEngine.PlayerList.Add(CharacterPlayer);

            // Set Monster Conditions

            var MonsterPlayer1 = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = 200,
                                Level = 1,
                                CurrentHealth = 100,
                                ExperienceTotal = 100,
                                ExperienceRemaining = 1,
                                Defense = 1,
                                Attack = 1,
                                Name = "Daffy",
                            });
            var MonsterPlayer2 = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 200,
                    Level = 1,
                    CurrentHealth = 100,
                    ExperienceTotal = 100,
                    ExperienceRemaining = 1,
                    Defense = 1,
                    Attack = 1,
                    Name = "Duck",
                });

            BattleEngine.PlayerList.Add(MonsterPlayer1);
            BattleEngine.PlayerList.Add(MonsterPlayer2);

            //Act
            var result = BattleEngine.AddMonstersToRound();
            var playerList = BattleEngine.PlayerList;
            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(((tempSpeed + playerList[1].GetSpeedLevelBonus) * 10), playerList[1].Speed);
            Assert.AreEqual(((tempAttack + playerList[1].GetAttackLevelBonus) * 10), playerList[1].Attack);
            Assert.AreEqual(((tempDefense + playerList[1].GetDefenseLevelBonus) * 10), playerList[1].Defense);

        }
    }
}