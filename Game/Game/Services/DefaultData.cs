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
                    Name = "Headband",
                    Description = "Accessory to keep sweat from running down your face",
                    ImageURI = "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png",
                    Range = 0,
                    Damage = 9,
                    Value = 3,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Wristband",
                    Description = "Acessory for your wrist",
                    ImageURI = "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Performance Socks",
                    Description = "Socks that help athletic performance",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Mouthguard",
                    Description = "To prevent face damage",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Ankle Brace",
                    Description = "Helps protect your ankles",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Arm Sleeve",
                    Description = "Provides strength to your arm",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Knee Brace",
                    Description = "Protects your knees",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Air Jordans",
                    Description = "Fashionable accessory with a cool boost",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Feet,
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