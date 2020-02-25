﻿using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Game.Models;
using Game.Services;
using Game.Engine;
using Game.Helpers;

namespace Game.Engine
{
    /* 
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     */

    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// </summary>
    public class TurnEngine
    {

        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over

        #region Properties

        // Holds the official ScoreModel
        public ScoreModel BattleScore = new ScoreModel();

        // Holds the Battle Messages as they happen
        public BattleMessagesModel BattleMessagesModel = new BattleMessagesModel();

        // The Pool of items collected during the round as turns happen
        public List<ItemModel> ItemPool = new List<ItemModel>();

        // List of Monsters
        public List<MonsterModel> MonsterList = new List<MonsterModel>();

        // List of Characters
        public List<CharacterModel> CharacterList = new List<CharacterModel>();

        // Current Player who is the attacker
        public PlayerInfo CurrentAttacker;

        // Current Player who is the Defender
        public PlayerInfo CurrentDefender;
        #endregion Properties

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool TakeTurn(CharacterModel Attacker)
        {
            // Choose Move or Attack

            // For Attack, Choose Who
            var Target = AttackChoice(Attacker);

            if (Target == null)
            {
                return false;
            }

            // Do Attack
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            var DefenseScore = Target.GetDefense() + Target.Level;
            TurnAsAttack(Attacker, AttackScore, Target, DefenseScore);

            CurrentAttacker = new PlayerInfo(Attacker);
            CurrentDefender = new PlayerInfo(Target);

            return true;
        }

        /// <summary>
        /// // MonsterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool TakeTurn(MonsterModel Attacker)
        {
            // Choose Move or Attack

            // For Attack, Choose Who
            var Target = AttackChoice(Attacker);

            if (Target == null)
            {
                return false;
            }

            // Do Attack
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            var DefenseScore = Target.GetDefense() + Target.Level;
            TurnAsAttack(Attacker, AttackScore, Target, DefenseScore);

            CurrentAttacker = new PlayerInfo(Attacker);
            CurrentDefender = new PlayerInfo(Target);

            return true;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="AttackScore"></param>
        /// <param name="Target"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public bool TurnAsAttack(MonsterModel Attacker, int AttackScore, CharacterModel Target, int DefenseScore)
        {
            BattleMessagesModel.TurnMessage = string.Empty;
            BattleMessagesModel.TurnMessageSpecial = string.Empty;
            BattleMessagesModel.AttackStatus = string.Empty;

            BattleMessagesModel.PlayerType = PlayerTypeEnum.Monster;

            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            BattleScore.TurnCount++;

            // Choose who to attack

            BattleMessagesModel.TargetName = Target.Name;
            BattleMessagesModel.AttackerName = Attacker.Name;

            BattleMessagesModel.HitStatus = RollToHitTarget(AttackScore, DefenseScore);

            Debug.WriteLine(BattleMessagesModel.GetTurnMessage());

            if (BattleMessagesModel.HitStatus == HitStatusEnum.Miss)
            {
                return true;
            }

            if (BattleMessagesModel.HitStatus == HitStatusEnum.CriticalMiss)
            {
                return true;
            }

            // It's a Hit or a Critical Hit
            if (BattleMessagesModel.HitStatus == HitStatusEnum.Hit || BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit)
            {
                //Calculate Damage
                BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                //BattleMessagesModel.DamageAmount += GameGlobals.ForceCharacterDamangeBonusValue;   // Add the Forced Damage Bonus (used for testing...)

                //if (GameGlobals.EnableCriticalHitDamage)
                //{
                //    if (BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit)
                //    {
                //        //2x damage
                //        BattleMessagesModel.DamageAmount += BattleMessagesModel.DamageAmount;
                //    }
                //}

                Target.TakeDamage(BattleMessagesModel.DamageAmount);
            }

            BattleMessagesModel.CurrentHealth = Target.CurrentHealth;
            BattleMessagesModel.TurnMessageSpecial = BattleMessagesModel.GetCurrentHealthMessage();

            // Check for alive
            if (Target.Alive == false)
            {
                // Remover target from list...
                CharacterList.Remove(Target);

                // Mark Status in output
                BattleMessagesModel.TurnMessageSpecial = " and causes death";

                // Add the MonsterModel to the killed list
                BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                // Drop Items to ItemModel Pool
                var myItemList = Target.DropAllItems();

                // Add to ScoreModel
                foreach (var ItemModel in myItemList)
                {
                    BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                    BattleMessagesModel.TurnMessageSpecial += " ItemModel " + ItemModel.Name + " dropped";
                }

                ItemPool.AddRange(myItemList);
            }

            BattleMessagesModel.TurnMessage = Attacker.Name + BattleMessagesModel.AttackStatus + Target.Name + BattleMessagesModel.TurnMessageSpecial;
            Debug.WriteLine(BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// CharacterModel attacks MonsterModel
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="AttackScore"></param>
        /// <param name="Target"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public bool TurnAsAttack(CharacterModel Attacker, int AttackScore, MonsterModel Target, int DefenseScore)
        {
            BattleMessagesModel.TurnMessage = string.Empty;
            BattleMessagesModel.TurnMessageSpecial = string.Empty;
            BattleMessagesModel.AttackStatus = string.Empty;
            BattleMessagesModel.LevelUpMessage = string.Empty;

            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            BattleScore.TurnCount++;

            // Choose who to attack

            BattleMessagesModel.TargetName = Target.Name;
            BattleMessagesModel.AttackerName = Attacker.Name;

            BattleMessagesModel.HitStatus = RollToHitTarget(AttackScore, DefenseScore);

            Debug.WriteLine(BattleMessagesModel.GetTurnMessage());

            if (BattleMessagesModel.HitStatus == HitStatusEnum.Miss)
            {
                return true;
            }

            if (BattleMessagesModel.HitStatus == HitStatusEnum.CriticalMiss)
            {
                //if (GameGlobals.EnableCriticalMissProblems)
                //{
                //    BattleMessagesModel.TurnMessage += DetermineCriticalMissProblem(Attacker);
                //}
                return true;
            }

            // It's a Hit or a Critical Hit
            if (BattleMessagesModel.HitStatus == HitStatusEnum.Hit || BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit)
            {
                //Calculate Damage
                BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                //BattleMessagesModel.DamageAmount += GameGlobals.ForceCharacterDamangeBonusValue;   // Add the Forced Damage Bonus (used for testing...)

                //if (GameGlobals.EnableCriticalHitDamage)
                //{
                //    if (BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit)
                //    {
                //        //2x damage
                //        BattleMessagesModel.DamageAmount += BattleMessagesModel.DamageAmount;
                //    }
                //}

                Target.TakeDamage(BattleMessagesModel.DamageAmount);

                var experienceEarned = Target.CalculateExperienceEarned(BattleMessagesModel.DamageAmount);

                var LevelUp = Attacker.AddExperience(experienceEarned);
                if (LevelUp)
                {
                    BattleMessagesModel.LevelUpMessage = Attacker.Name + " is now Level " + Attacker.Level + " With Health Max of " + Attacker.GetHealthMax();
                    Debug.WriteLine(BattleMessagesModel.LevelUpMessage);
                }

                BattleScore.ExperienceGainedTotal += experienceEarned;
            }

            BattleMessagesModel.TurnMessageSpecial = " remaining health is " + Target.CurrentHealth;

            // Check for alive
            if (Target.Alive == false)
            {
                // Remove target from list...
                MonsterList.Remove(Target);

                // Mark Status in output
                BattleMessagesModel.TurnMessageSpecial = " and causes death";

                // Add one to the monsters killd count...
                BattleScore.MonsterSlainNumber++;

                // Add the MonsterModel to the killed list
                BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                // Drop Items to ItemModel Pool
                var myItemList = Target.DropAllItems();

                // If Random drops are enabled, then add some....
                myItemList.AddRange(GetRandomMonsterItemDrops(BattleScore.RoundCount));

                // Add to ScoreModel
                foreach (var ItemModel in myItemList)
                {
                    BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                    BattleMessagesModel.TurnMessageSpecial += " ItemModel " + ItemModel.Name + " dropped";
                }

                ItemPool.AddRange(myItemList);
            }

            BattleMessagesModel.TurnMessage = Attacker.Name + BattleMessagesModel.AttackStatus + Target.Name + BattleMessagesModel.TurnMessageSpecial;
            Debug.WriteLine(BattleMessagesModel.TurnMessage);

            return true;
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

            //// Turn On UnitTestingSetRoll
            //if (DiceHelper.ForceRollsToNotRandom)
            //{
            //    // Don't let it be 0, if it was not initialized...
            //    if (DiceHelper.ForceToHitValue < 1)
            //    {
            //        DiceHelper.ForceToHitValue = 1;
            //    }

            //    d20 = DiceHelper.ForceToHitValue;
            //}

            if (d20 == 1)
            {
                // Force Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.CriticalMiss;
                return BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {
                // Force Hit
                BattleMessagesModel.HitStatus = HitStatusEnum.CriticalHit;
                return BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                BattleMessagesModel.AttackStatus = " misses ";
                // Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                BattleMessagesModel.DamageAmount = 0;
                return BattleMessagesModel.HitStatus;
            }

            // Hit
            BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            return BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public MonsterModel AttackChoice(CharacterModel data)
        {
            if (MonsterList == null)
            {
                return null;
            }

            if (MonsterList.Count < 1)
            {
                return null;
            }

            //// For now, just use a simple selection of the first in the list.
            //// Later consider, strongest, closest, with most Health etc...
            //foreach (var Defender in MonsterList)
            //{
            //    if (Defender.Alive)
            //    {
            //        return Defender;
            //    }
            //}

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first 
            var DefenderWeakest = MonsterList.OrderBy(m => m.CurrentHealth).FirstOrDefault();
            if (DefenderWeakest.Alive)
            {
                return DefenderWeakest;
            }

            return null;
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CharacterModel AttackChoice(MonsterModel data)
        {
            if (CharacterList == null)
            {
                return null;
            }

            if (CharacterList.Count < 1)
            {
                return null;
            }

            // For now, just use a simple selection of the first in the list.
            // Later consider, strongest, closest, with most Health etc...

            foreach (var Defender in CharacterList)
            {
                if (Defender.Alive)
                {
                    // Select first one to hit in the list for now...
                    return Defender;
                }
            }
            return null;
        }

        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            var myList = new List<ItemModel>();

            ////if (!GameGlobals.AllowMonsterDropItems)
            ////{
            ////    return myList;
            ////}

            //var myItemsViewModel = ItemIndexViewModel.Instance;

            //if (myItemsViewModel.Dataset.Count > 0)
            //{
            //    // Random is enabled so build up a list of items dropped...
            //    var ItemCount = DiceHelper.RollDice(1, 4);
            //    for (var i = 0; i < ItemCount; i++)
            //    {
            //        var rnd = DiceHelper.RollDice(1, myItemsViewModel.Dataset.Count);
            //        var itemBase = myItemsViewModel.Dataset[rnd - 1];
            //        var ItemModel = new ItemModel(itemBase);
            //        ItemModel.ScaleLevel(round);

            //        // Make sure the ItemModel is added to the global list...
            //        var myItem = ItemIndexViewModel.Instance.CheckIfItemExists(ItemModel);
            //        if (myItem == null)
            //        {
            //            // ItemModel does not exist, so add it to the datstore
            //            ItemIndexViewModel.Instance.Create_Sync(ItemModel);
            //        }
            //        else
            //        {
            //            // Swap them becaues it already exists, no need to create a new one...
            //            ItemModel = myItem;
            //        }

            //        // Add the ItemModel to the local list...
            //        myList.Add(ItemModel);
            //    }
            //}
            return myList;
        }
    }
}