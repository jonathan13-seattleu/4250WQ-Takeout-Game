using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Game.Models;
using Game.Helpers;
using Game.ViewModels;
using System;

namespace Game.Engine
{
    /* 
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     * 
     */

    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// 
    /// </summary>
    public class TurnEngine : BaseEngine
    {
        #region Algrorithm
        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over
        #endregion Algrorithm

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool TakeTurn(PlayerInfoModel Attacker)
        {
            // Choose Action.  Such as Move, Attack etc.

            // INFO: Teams, if you have other actions they would go here.

            bool result = false;

            // Determine What Monsters can do...
            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                /*
                 * Order of Priority
                 * If can attack Then Attack
                 * Next use Ability or Move
                 */

                // Assume Move if nothing else happens
                //CurrentAction = ActionEnum.Move;

                /*// See if Desired Target is within Range, and if so attack away
                if (MapModel.IsTargetInRange(Attacker, AttackChoice(Attacker)))
                {
                    CurrentAction = ActionEnum.Attack;
                }*/

                // Simple Logic is Roll to Try Ability. 50% says try
                /*else*/ if (DiceHelper.RollDice(1, 10) > 5)
                {
                    CurrentAction = ActionEnum.Ability;
                }
            }/*
            else
            {
                if (false)
                {
                    if (BattleScore.AutoBattle)
                    {
                        /*
                         * Order of Priority
                         * If can attack Then Attack
                         * Next use Ability or Move
                         */

                        // Assume Move if nothing else happens
                        /*CurrentAction = ActionEnum.Move;

                        // See if Desired Target is within Range, and if so attack away
                        if (MapModel.IsTargetInRange(Attacker, AttackChoice(Attacker)))
                        {
                            CurrentAction = ActionEnum.Attack;
                        }

                        // Simple Logic is Roll to Try Ability. 50% says try
                        else if (DiceHelper.RollDice(1, 10) > 5)
                        {
                            CurrentAction = ActionEnum.Ability;
                        }
                    }
                }
            }*/

            switch (CurrentAction)
            {
                case ActionEnum.Unknown:
                case ActionEnum.Attack:
                    result = Attack(Attacker);
                    break;

                /*case ActionEnum.Ability:
                    result = UseAbility(Attacker);
                    break;*/

                /*case ActionEnum.Move:
                    result = MoveAsTurn(Attacker);
                    break;*/
            }

            BattleScore.TurnCount++;

            return result;
        }

        /// <summary>
        /// Find a Desired Target
        /// Move close to them
        /// Get to move the number of Speed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        /*public bool MoveAsTurn(PlayerInfoModel Attacker)
        {

            /*
             * TODO: TEAMS Work out your own move logic if you are implementing move
             * 
             * Mike's Logic
             * The monster or charcter will move to a different square if one is open
             * Get X,Y for destired Target
             * Take 1 square closer to Monster X if needed and open 
             * Take 1 square closer to Monster Y if needed and open
             * If desired square is occupied, give up
             */

            /*if (BattleScore.AutoBattle)
            {
                // For Attack, Choose Who
                CurrentDefender = AttackChoice(Attacker);

                if (CurrentDefender == null)
                {
                    return false;
                }

                /*//*/ Get X, Y for Defender
                var locationDefender = MapModel.GetLocationForPlayer(CurrentDefender);
                if (locationDefender == null)
                {
                    return false;
                }

                var locationAttacker = MapModel.GetLocationForPlayer(Attacker);
                if (locationAttacker == null)
                {
                    return false;
                }*/

                // Move toward them

                /*
                 * First Try moving X closer to them by one square
                 * 
                 * If that can't be done, then try moving Y closer to them by one square
                 * 
                 */

                /*var MoveX = locationAttacker.Column + (locationDefender.Column - locationAttacker.Column);

                // see if location MoveX, Attacker.Y is empty, if so go there
                if (MapModel.IsEmptySquare(MoveX, locationAttacker.Row))
                {
                    Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, MoveX, locationAttacker.Row));
                    return MapModel.MovePlayerOnMap(locationAttacker, MoveX, locationAttacker.Row);
                }

                var MoveY = locationAttacker.Row + (locationDefender.Row - locationAttacker.Row);

                // see if location MoveX, Attacker.Y is empty, if so go there
                if (MapModel.IsEmptySquare(locationAttacker.Column, MoveY))
                {
                    Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, locationAttacker.Column, MoveY));
                    return MapModel.MovePlayerOnMap(locationAttacker, locationAttacker.Column, MoveY);
                }*/

                /*return false;
            }

            return true;
        }*/

        /*/// <summary>
        /// Use the Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool UseAbility(PlayerInfoModel Attacker)
        {
            var avaible = Attacker.AbilityTracker.TryGetValue(CurrentActionAbility, out int remaining);
            if (avaible == false)
            {
                // does not exist
                return false;
            }

            if (remaining < 1)
            {
                // out of tries
                return false;
            }

            switch (CurrentActionAbility)
            {
                case AbilityEnum.Heal:
                case AbilityEnum.Bandage:
                    Attacker.BuffHealth();
                    break;

                case AbilityEnum.Toughness:
                case AbilityEnum.Barrier:
                    Attacker.BuffDefense();
                    break;

                case AbilityEnum.Curse:
                case AbilityEnum.Focus:
                    Attacker.BuffAttack();
                    break;

                case AbilityEnum.Quick:
                case AbilityEnum.Nimble:
                    Attacker.BuffSpeed();
                    break;
            }

            // Reduce the count
            Attacker.AbilityTracker[CurrentActionAbility] = remaining - 1;

            return true;
        }*/

        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool Attack(PlayerInfoModel Attacker)
        {
            // INFO: Teams, AttackChoice will auto pick the target, good for auto battle
            if (BattleScore.AutoBattle)
            {
                // For Attack, Choose Who
                CurrentDefender = AttackChoice(Attacker);

                if (CurrentDefender == null)
                {
                    return false;
                }
            }

            // Do Attack
            TurnAsAttack(Attacker, CurrentDefender);

            return true;
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            switch (data.PlayerType)
            {
                case PlayerTypeEnum.Monster:
                    return SelectCharacterToAttack();

                case PlayerTypeEnum.Character:
                default:
                    return SelectMonsterToAttack();
            }
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel SelectCharacterToAttack()
        {
            if (PlayerList == null)
            {
                return null;
            }

            if (PlayerList.Count < 1)
            {
                return null;
            }

            // Select first in the list

            var Defender = PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character)
                .OrderBy(m => m.Defense).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel SelectMonsterToAttack()
        {
            if (PlayerList == null)
            {
                return null;
            }

            if (PlayerList.Count < 1)
            {
                return null;
            }

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first 

            var Defender = PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster)
                .OrderByDescending(m => m.Defense).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="AttackScore"></param>
        /// <param name="Target"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            // Set Messages to empty
            BattleMessagesModel.ClearMessages();

            // Do the Attack
            CalculateAttackStatus(Attacker, Target);

            // Hackathon
            // Hackathon Scenario 2, Bob alwasys misses
            if (Attacker.Name.Equals("Bob"))
            {
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                BattleMessagesModel.TurnMessage = "Bob always Misses";
                Debug.WriteLine(BattleMessagesModel.TurnMessage);
                return true;
            }

            // See if the Battle Settings Overrides the Roll
            BattleMessagesModel.HitStatus = BattleSettingsOverride(Attacker);

            switch (BattleMessagesModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss

                    break;

                case HitStatusEnum.Hit:
                    // It's a Hit

                    //Calculate Damage
                    BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                    // Apply the Damage
                    ApplyDamage(Target);

                    BattleMessagesModel.TurnMessageSpecial = BattleMessagesModel.GetCurrentHealthMessage();

                    // Check if Dead and Remove
                    RemoveIfDead(Target);

                    // If it is a character apply the experience earned
                    CalculateExperience(Attacker, Target);

                    break;
            }

            BattleMessagesModel.TurnMessage = Attacker.Name + BattleMessagesModel.AttackStatus + Target.Name + BattleMessagesModel.TurnMessageSpecial + BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// See if the Battle Settings will Override the Hit
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public HitStatusEnum BattleSettingsOverride(PlayerInfoModel Attacker)
        {
            // See if Monsters always Hit or Miss
            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                return BattleSettingsOverrideHitStatusEnum();
            }

            // Character
            return BattleSettingsOverrideHitStatusEnum();
        }

        /// <summary>
        /// Return the Override for the HitStatus
        /// </summary>
        /// <returns></returns>
        public HitStatusEnum BattleSettingsOverrideHitStatusEnum()
        {
            switch (BattleSettingsModel.MonsterHitEnum)
            {
                case HitStatusEnum.Hit:
                    BattleMessagesModel.AttackStatus = " somehow Hit ";
                    return HitStatusEnum.Hit;

                case HitStatusEnum.CriticalHit:
                    BattleMessagesModel.AttackStatus = " somehow Critical Hit ";
                    return HitStatusEnum.CriticalHit;

                case HitStatusEnum.Miss:
                    BattleMessagesModel.AttackStatus = " somehow Missed ";
                    return HitStatusEnum.Miss;

                case HitStatusEnum.CriticalMiss:
                    BattleMessagesModel.AttackStatus = " somehow Critical Missed ";
                    return HitStatusEnum.CriticalMiss;

                case HitStatusEnum.Unknown:
                case HitStatusEnum.Default:
                default:
                    // Return what it was
                    return BattleMessagesModel.HitStatus;
            }
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        /// <param name="Target"></param>
        private void ApplyDamage(PlayerInfoModel Target)
        {
            Target.TakeDamage(BattleMessagesModel.DamageAmount);
            BattleMessagesModel.CurrentHealth = Target.CurrentHealth;
        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public HitStatusEnum CalculateAttackStatus(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            // Remember Current Player
            BattleMessagesModel.PlayerType = PlayerTypeEnum.Monster;

            // Choose who to attack
            BattleMessagesModel.TargetName = Target.Name;
            BattleMessagesModel.AttackerName = Attacker.Name;

            // Set Attack and Defense
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            var DefenseScore = Target.GetDefense() + Target.Level;

            BattleMessagesModel.HitStatus = RollToHitTarget(AttackScore, DefenseScore);

            return BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        public bool CalculateExperience(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                var experienceEarned = Target.CalculateExperienceEarned(BattleMessagesModel.DamageAmount);
                BattleMessagesModel.ExperienceEarned = " Earned " + experienceEarned + " points";

                var LevelUp = Attacker.AddExperience(experienceEarned);
                if (LevelUp)
                {
                    BattleMessagesModel.LevelUpMessage = Attacker.Name + " is now Level " + Attacker.Level + " With Health Max of " + Attacker.GetMaxHealthTotal;
                    Debug.WriteLine(BattleMessagesModel.LevelUpMessage);
                }

                // Add Experinece to the Score
                BattleScore.ExperienceGainedTotal += experienceEarned;
            }

            return true;
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        /// <param name="Target"></param>
        public bool RemoveIfDead(PlayerInfoModel Target)
        {
            // Check for alive
            if (Target.Alive == false)
            {
                TargetDied(Target);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        /// <param name="Target"></param>
        public bool TargetDied(PlayerInfoModel Target)
        {
            bool found;

            // Mark Status in output
            BattleMessagesModel.TurnMessageSpecial = " and causes death. ";

            /*// Removing the 
            MapModel.RemovePlayerFromMap(Target);*/

            // INFO: Teams, Hookup your Boss if you have one...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // Add the Character to the killed list
                    BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    BattleScore.CharacterModelDeathList.Add(Target);

                    DropItems(Target);

                    found = CharacterList.Remove(CharacterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = PlayerList.Remove(PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;

                case PlayerTypeEnum.Monster:
                default:
                    // Add one to the monsters killed count...
                    BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    BattleScore.MonsterModelDeathList.Add(Target);

                    DropItems(Target);

                    found = MonsterList.Remove(MonsterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = PlayerList.Remove(PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;
            }
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        /// <param name="Target"></param>
        public int DropItems(PlayerInfoModel Target)
        {
            var DroppedMessage = "\nItems Dropped : \n";

            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            myItemList.AddRange(GetRandomMonsterItemDrops(BattleScore.RoundCount));

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            BattleMessagesModel.DroppedMessage = DroppedMessage;

            BattleScore.ItemModelDropList.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            var d20 = DiceHelper.RollDice(1, 20);

            if (d20 == 1)
            {
                BattleMessagesModel.AttackStatus = " rolls 1 to completly miss ";

                // Force Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                return BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {
                BattleMessagesModel.AttackStatus = " rolls 20 for lucky hit ";

                // Force Hit
                BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
                return BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                BattleMessagesModel.AttackStatus = " rolls " + d20 + " and misses ";

                // Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                BattleMessagesModel.DamageAmount = 0;
                return BattleMessagesModel.HitStatus;
            }

            BattleMessagesModel.AttackStatus = " rolls " + d20 + " and hits ";

            // Hit
            BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            return BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            // TODO: Teams, You need to implement your own modification to the Logic cannot use mine as is.

            // You decide how to drop monster items, level, etc.

            // The Number drop can be Up to the Round Count, but may be less.  
            // Negative results in nothing dropped
            var NumberToDrop = (DiceHelper.RollDice(1, round + 1) - 1);

            var result = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                // Get a random Unique Item
                var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                result.Add(data);
            }

            return result;
        }
    }
}