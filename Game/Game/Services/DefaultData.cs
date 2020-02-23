using Game.Models;
using Game.ViewModels;
using System.Collections.Generic;
using System.Linq;

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
                    ImageURI = "headband.png",
                    Range = 0,
                    Damage = 9,
                    Value = 3,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Wristband",
                    Description = "Acessory for your wrist",
                    ImageURI = "wristband.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Performance Socks",
                    Description = "Socks that help athletic performance",
                    ImageURI = "performance_socks.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Mouthguard",
                    Description = "To prevent face damage",
                    ImageURI = "mouthguard.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Ankle Brace",
                    Description = "Helps protect your ankles",
                    ImageURI = "ankle_brace.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Arm Sleeve",
                    Description = "Provides strength to your arm",
                    ImageURI = "arm_sleeve.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Knee Brace",
                    Description = "Protects your knees",
                    ImageURI = "knee_brace.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Air Jordans",
                    Description = "Fashionable accessory with a cool boost",
                    ImageURI = "air_jordan.png",
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
            string HeadString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.Head).FirstOrDefault().Id;
            string PrimaryHandString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.PrimaryHand).FirstOrDefault().Id;
            string OffHandString = null;
            string FeetString = null;
            string RightFingerString = null;
            string LeftFingerString = null;

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel
                {
                    Name = "Bugs Bunny",
                    Description = "Grey Bunny",
                    ImageURI= "bugs_bunny.png",
                    Level = 0,
                    Head = HeadString,

                },
                new CharacterModel
                {
                    Name = "Tweety Bird",
                    Description = "Yellow Bird",
                    ImageURI = "tweety_bird.png",
                    Level = 4,
                    OffHand = OffHandString,

                },
                new CharacterModel
                {
                    Name = "Porky Pig",
                    Description = "Baby Pig",
                    ImageURI = "porky_pig.png",
                    Level = 2,
                    PrimaryHand = PrimaryHandString

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