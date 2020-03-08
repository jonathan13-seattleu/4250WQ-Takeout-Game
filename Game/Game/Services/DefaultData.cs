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
            /// <summary>
            /// Load the Default data for scores
            /// </summary>
            /// <returns></returns>

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
            //Sets item name from ItemLocation Enum for each location.
            string HeadString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.Head).FirstOrDefault().Id;
            string PrimaryHandString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.PrimaryHand).FirstOrDefault().Id;
            string OffHandString = null;
            string FeetString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.Feet).FirstOrDefault().Id;
            string RightFingerString = null;
            string LeftFingerString = null;

            /// <summary>
            /// Load the Default data for characters
            /// </summary>
            /// <returns></returns>

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel
                {
                    Name = "Bugs Bunny",
                    Description = "Grey Bunny",
                    ImageURI= "bugs_bunny.png",
                    Level = 1,
                    MaxHealth = 40,
                    CurrentHealth =40,
                    Speed = 1,
                    Attack = 1,
                    Defense = 1,
                    Head = HeadString,

                },
                new CharacterModel
                {
                    Name = "Tweety Bird",
                    Description = "Yellow Bird",
                    ImageURI = "tweety_bird.png",
                    Level = 4,
                    MaxHealth = 5,
                    CurrentHealth = 5,
                    Speed = 1,
                    Attack = 2,
                    Defense = 3,
                    OffHand = OffHandString,

                },
                new CharacterModel
                {
                    Name = "Porky Pig",
                    Description = "Baby Pig",
                    ImageURI = "porky_pig.png",
                    Level = 2,
                    MaxHealth = 20,
                    CurrentHealth = 20,
                    Speed = 1,
                    Attack = 1,
                    Defense = 2,
                    PrimaryHand = PrimaryHandString

                },
                new CharacterModel
                {
                    Name = "Daffy Duck",
                    Description = "Daffy Duck",
                    ImageURI ="daffy_duck.png",
                    Level = 1,
                    MaxHealth = 12,
                    CurrentHealth = 12,
                    Speed = 1,
                    Attack = 1,
                    Defense = 1,
                    PrimaryHand = PrimaryHandString
                },
                new CharacterModel
                {
                    Name = "Tasmanian Devil",
                    Description = "Tasmanian Devil",
                    ImageURI = "tasmanian_devil.png",
                    Level = 1,
                    MaxHealth = 15,
                    CurrentHealth = 15,
                    Speed = 1,
                    Attack = 1,
                    Defense = 1,
                    Feet = FeetString
                },
                new CharacterModel
                {
                    Name = "Marvin the Martian",
                    Description = "Marvin the Martian",
                    Level = 1,
                    MaxHealth = 10,
                    CurrentHealth = 10,
                    Speed = 1,
                    Attack = 1,
                    Defense = 1,
                    ImageURI = "marvin_the_martian.png",
                    Feet = FeetString
                },
                new CharacterModel
                {
                    Name = "Michael Jordan",
                    Description = "Michael Jordan",
                    Level = 5,
                    MaxHealth = 25,
                    CurrentHealth = 25,
                    Speed = 2,
                    Attack = 2,
                    Defense = 4,
                    ImageURI = "michael_jordan.png",
                    Feet = FeetString

                },
            };
            return datalist;
        }
        public static List<MonsterModel> LoadData(MonsterModel Temp)
        {
            /// <summary>
            /// Load the Default data for characters
            /// </summary>
            /// <returns></returns>
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
                },
                new MonsterModel
                {
                    Name = "Bupkus",
                    Description ="Purple Monster",
                    ImageURI = "bupkus_monster.png",
                    Level = 6
                },
                new MonsterModel
                {
                    Name = "Pound",
                    Description ="Orange Monster",
                    ImageURI = "pound_monster.png",
                    Level = 8
                },
                new MonsterModel
                {
                    Name = "Mr. Swackhammer",
                    Description ="Giant Purple Monster",
                    ImageURI = "mr._swackhammer_monster.png",
                    Level = 10
                },



            };
            return datalist;
        }
    }
}