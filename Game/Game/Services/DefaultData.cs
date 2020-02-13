﻿using Game.Models;
using System.Collections.Generic;

namespace Game.Services
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "Gold Sword",
                    Description = "Sword made of Gold, really expensive looking",
                    ImageURI = "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png",
                    Range = 0,
                    Damage = 9,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Strong Shield",
                    Description = "Enough to hide behind",
                    ImageURI = "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Bunny Hat",
                    Description = "Pink hat with fluffy ears",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed},
            };

            return datalist;
        }

        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

            return datalist;
        }
        public static List<CharacterModel> LoadData(CharacterModel Temp)
        {
            var datalist = new List<CharacterModel>()
            {
                new CharacterModel
                {
                    Name = "Bugs Bunny",
                    Description = "Grey Bunny",
                    ImageURI= "bugs_bunny.png",
                    Level = 0,
                },
                new CharacterModel
                {
                    Name = "Tweety Bird",
                    Description = "Yellow Bird",
                    ImageURI = "tweety_bird.png",
                    Level = 4,
                },
                new CharacterModel
                {
                    Name = "Porky Pig",
                    Description = "Baby Pig",
                    ImageURI = "porky_pig.png",
                    Level = 2
                }
            };
            return datalist;
        }
        public static List<MonsterModel> LoadData(MonsterModel Temp)
        {
            var datalist = new List<MonsterModel>()
            {
                new MonsterModel
                {
                    Name = "Nawt",
                    Description = "Pink Monster",
                    ImageURI = "nawt_monster.png",
                    Level = 2
                },
                
                new MonsterModel
                {
                    Name = "Bang",
                    Description = "Green Monster",
                    ImageURI = "bang_monster.png",
                    Level = 3
                },

                new MonsterModel
                {
                    Name = "Blanko",
                    Description ="Blue Monster",
                    ImageURI = "blanko_monster.png",
                    Level = 5
                }
            };
            return datalist;
        }
    }
}