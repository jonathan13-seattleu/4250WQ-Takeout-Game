using System;
using System.Collections.Generic;
using System.Linq;
using Game.Helpers;
using Game.Models;
using Game.ViewModels;

namespace Game.Engine
{
    /// <summary>
    /// Manages the Rounds
    /// </summary>
    public class RoundEngine : TurnEngine
    {
        /// <summary>
        /// Clear the List between Rounds
        /// </summary>
        /// <returns></returns>
        private bool ClearLists()
        {
            ItemPool.Clear();
            MonsterList.Clear();
            return true;
        }

        /// <summary>
        /// Call to make a new set of monsters...
        /// </summary>
        /// <returns></returns>
        public bool NewRound()
        {
            // End the existing round
            EndRound();

            // Remove Character Buffs
            RemoveCharacterBuffs();

            // Populate New Monsters...
            AddMonstersToRound();

            // Make the PlayerList
            MakePlayerList();

            // Set Order for the Round
            OrderPlayerListByTurnOrder();

            /*// Populate MapModel with Characters and Monsters
            MapModel.PopulateMapModel(PlayerList);*/

            // Update Score for the RoundCount
            BattleScore.RoundCount++;

            return true;
        }

        /// <summary>
        /// Add Monsters to the Round
        /// 
        /// Because Monsters can be duplicated, will add 1, 2, 3 to their name
        ///   
        /*
            * Hint: 
            * I don't have crudi monsters yet so will add 6 new ones...
            * If you have crudi monsters, then pick from the list
            * Consdier how you will scale the monsters up to be appropriate for the characters to fight
            * 
            */
        /// </summary>
        /// <returns></returns>
        public int AddMonstersToRound()
        {
                var monsterModel = MonsterIndexViewModel.Instance;
                Random rnd = new Random();

                // TODO: Teams, You need to implement your own Logic can not use mine.

                int TargetLevel = 1;
                int MaxLevel = 20;
                int index = rnd.Next(0, monsterModel.Dataset.Count());
                var data = monsterModel.Dataset[index];
                
                
            if (CharacterList.Count() > 0)
                {
                    // Get the Min Character Level (linq is soo cool....)
                    TargetLevel = Convert.ToInt32(CharacterList.Min(m => m.Level));
                    MaxLevel = Convert.ToInt32(CharacterList.Max(m => m.Level));    
                }
                /* Hack 31 has been implemented. If the round count is > 100
                 * then the monster's speed, defense, attack, current health, and max health 
                 * are buffed 10x
                 */
                for (var i = 0; i < MaxNumberPartyMonsters; i++)
                {
                    if(BattleScore.RoundCount >= 100)
                {
                    data.Level = MaxLevel;
                    data.Speed = (getAttributeLevel() * 10);
                    data.Defense = (getAttributeLevel() * 10);
                    data.Attack = (getAttributeLevel() * 10);
                    data.CurrentHealth = (data.CurrentHealth * 10);
                    data.MaxHealth = (data.MaxHealth * 10);

                }
                else
                {
                    data.Level = TargetLevel;
                    data.Speed = getAttributeLevel();
                    data.Defense = getAttributeLevel();
                    data.Attack = getAttributeLevel();
                    data.MaxHealth = DiceHelper.RollDice(TargetLevel, 10);
                    data.CurrentHealth = data.MaxHealth;

                }

                MonsterList.Add(new PlayerInfoModel(data));
                }

                return MonsterList.Count();
            

        }
        int getAttributeLevel()
        {
            return DiceHelper.RollDice(1, 10) - 1;

        }

        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        /// <returns></returns>
        public bool EndRound()
        {
            // In Auto Battle this happens and the characters get their items, In manual mode need to do it manualy
            if (BattleScore.AutoBattle)
            {
                PickupItemsForAllCharacters();
            }

            // Reset Monster and Item Lists
            ClearLists();

            return true;
        }

        /// <summary>
        /// For each character pickup the items
        /// </summary>
        public void PickupItemsForAllCharacters()
        {
            // In Auto Battle this happens and the characters get their items
            // When called manualy, make sure to do the character pickup before calling EndRound

            // Have each character pickup items...
            foreach (var character in CharacterList)
            {
                PickupItemsFromPool(character);
            }
        }

        /// <summary>
        /// Manage Next Turn
        /// 
        /// Decides Who's Turn
        /// Remembers Current Player
        /// 
        /// Starts the Turn
        /// 
        /// </summary>
        /// <returns></returns>
        public RoundEnum RoundNextTurn()
        {
            // No characters, game is over...
            if (CharacterList.Count < 1)
            {
                // Game Over
                RoundStateEnum = RoundEnum.GameOver;
                return RoundStateEnum;
            }

            // Check if round is over
            if (MonsterList.Count < 1)
            {
                // If over, New Round
                RoundStateEnum = RoundEnum.NewRound;
                return RoundEnum.NewRound;
            }

            if (BattleScore.AutoBattle)
            {
                // Decide Who gets next turn
                // Remember who just went...
                CurrentAttacker = GetNextPlayerTurn();

                // Only Attack for now
                CurrentAction = ActionEnum.Attack;
            }

            // Do the turn....
            TakeTurn(CurrentAttacker);

            RoundStateEnum = RoundEnum.NextTurn;

            return RoundStateEnum;
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel GetNextPlayerTurn()
        {
            // Remove the Dead
            RemoveDeadPlayersFromList();

            // Get Next Player
            var PlayerCurrent = GetNextPlayerInList();

            return PlayerCurrent;
        }

        /// <summary>
        /// Remove Dead Players from the List
        /// </summary>
        /// <returns></returns>
        public List<PlayerInfoModel> RemoveDeadPlayersFromList()
        {
            PlayerList = PlayerList.Where(m => m.Alive == true).ToList();
            return PlayerList;
        }

        /// <summary>
        /// Order the Players in Turn Sequence
        /// </summary>
        public List<PlayerInfoModel> OrderPlayerListByTurnOrder()
        {
            // Order is based by... 
            // Order by Speed (Desending)
            // Then by Highest level (Descending)
            // Then by Highest Experience Points (Descending)
            // Then by Character before MonsterModel (enum assending)
            // Then by Alphabetic on Name (Assending)
            // Then by First in list order (Assending

            /*
             * Every 5th round, the sort order for turn order changes and list is sorted by Characters first, 
             * then lowest health, then lowest speed.
             */
            if (BattleScore.RoundCount % 5 == 0)
            {
                PlayerList = PlayerList.OrderBy(a => a.PlayerType)
                .ThenBy(a => a.CurrentHealth)
                .ThenBy(a => a.Speed)
                .ToList();
            }
            else
            {
                PlayerList = PlayerList.OrderByDescending(a => a.GetSpeed())
                    .ThenByDescending(a => a.Level)
                    .ThenByDescending(a => a.ExperienceTotal)
                    .ThenByDescending(a => a.PlayerType)
                    .ThenBy(a => a.Name)
                    .ThenBy(a => a.ListOrder)
                    .ToList();
            }

            /*
             * The first character in the player list gets their base Attack, Speed, Defense 
             * values buffed by 2x for the time they are the first in the list.
             */
            int index = 0;

            var firstCharacter = PlayerList[index];
            int currentSpeed = firstCharacter.Speed;
            int currentAttack = firstCharacter.Attack;
            int currentDefense = firstCharacter.Defense;

            firstCharacter.Speed = (currentSpeed * 2);
            firstCharacter.Attack = (currentAttack * 2);
            firstCharacter.Defense = (currentDefense * 2);

            PlayerList[index] = firstCharacter;

            return PlayerList;
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public List<PlayerInfoModel> MakePlayerList()
        {
            // Start from a clean list of players
            PlayerList.Clear();

            // Remember the Insert order, used for Sorting
            var ListOrder = 0;

            foreach (var data in CharacterList)
            {
                if (data.Alive)
                {
                    PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = ListOrder
                        });

                    ListOrder++;
                }
            }

            foreach (var data in MonsterList)
            {
                if (data.Alive)
                {
                    PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = ListOrder
                        });

                    ListOrder++;
                }
            }

            return PlayerList;
        }

        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // If List is empty, return null
            if (PlayerList.Count == 0)
            {
                return null;
            }

            // No current player, so set the first one
            if (CurrentAttacker == null)
            {
                return PlayerList.FirstOrDefault();
            }

            // Find current player in the list
            var index = PlayerList.FindIndex(m => m.Guid.Equals(CurrentAttacker.Guid));

            // If at the end of the list, return the first element
            if (index == PlayerList.Count() - 1)
            {
                return PlayerList.FirstOrDefault();
            }

            // Return the next element
            return PlayerList[index + 1];
        }

        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        /// <param name="character"></param>
        public bool PickupItemsFromPool(PlayerInfoModel character)
        {

            // TODO: Teams, You need to implement your own Logic if not using auto apply

            // I use the same logic for Auto Battle as I do for Manual Battle

            //if (BattleScore.AutoBattle)
            {
                // Have the character, walk the items in the pool, and decide if any are better than current one.

                GetItemFromPoolIfBetter(character, ItemLocationEnum.Head);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.Necklass);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.PrimaryHand);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.OffHand);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.RightFinger);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.LeftFinger);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.Feet);
            }
            return true;
        }

        /// <summary>
        /// Swap out the item if it is better
        /// 
        /// Uses Value to determine
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        public bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation)
        {
            var myList = ItemPool.Where(a => a.Location == setLocation)
                .OrderByDescending(a => a.Value)
                .ToList();

            // If no items in the list, return...
            if (!myList.Any())
            {
                return false;
            }

            var CharacterItem = character.GetItemByLocation(setLocation);
            if (CharacterItem == null)
            {
                SwapCharacterItem(character, setLocation, myList.FirstOrDefault());
                return true;
            }

            foreach (var PoolItem in myList)
            {
                if (PoolItem.Value > CharacterItem.Value)
                {
                    SwapCharacterItem(character, setLocation, PoolItem);
                    return true;
                }
            }

            return true;
        }

        /// <summary>
        /// Swap the Item the character has for one from the pool
        /// 
        /// Drop the current item back into the Pool
        /// 
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        /// <param name="PoolItem"></param>
        /// <returns></returns>
        public ItemModel SwapCharacterItem(PlayerInfoModel character, ItemLocationEnum setLocation, ItemModel PoolItem)
        {
            // Put on the new ItemModel, which drops the one back to the pool
            var droppedItem = character.AddItem(setLocation, PoolItem.Id);

            // Add the PoolItem to the list of selected items
            BattleScore.ItemModelSelectList.Add(PoolItem);

            // Remove the ItemModel just put on from the pool
            ItemPool.Remove(PoolItem);

            if (droppedItem != null)
            {
                // Add the dropped ItemModel to the pool
                ItemPool.Add(droppedItem);
            }

            return droppedItem;
        }

        /// <summary>
        /// For all characters in player list, remove their buffs
        /// </summary>
        /// <returns></returns>
        public bool RemoveCharacterBuffs()
        {
            foreach (var data in PlayerList)
            {
                data.ClearBuffs();
            }

            foreach (var data in CharacterList)
            {
                data.ClearBuffs();
            }
            return true;
        }
    }
}