﻿using Game.Helpers;

namespace Game.Models
{
    /// <summary>
    /// Player for the game.
    /// 
    /// Either Monster or Character
    /// 
    /// Constructor Player to Player used a T in Round
    /// </summary>
    public class PlayerInfoModel : BasePlayerModel<PlayerInfoModel>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PlayerInfoModel() { }

        /// <summary>
        /// Copy from one PlayerInfoModel into Another
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(PlayerInfoModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetCurrentHealthTotal;
            MaxHealth = data.GetMaxHealthTotal;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;
        }

        /// <summary>
        /// Create PlayerInfoModel from character
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(CharacterModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetCurrentHealthTotal;
            MaxHealth = data.GetMaxHealthTotal;
            Attack = data.GetAttack();
            Defense = data.GetDefense();

            // Set the Base Attributes
            Speed = data.Speed;
            Defense = data.Defense;
            Attack = data.Attack;
            MaxHealth = data.MaxHealth;
            CurrentHealth = data.CurrentHealth;


            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            // Give the copy a differet quid, so it can be used in the battles as a copy
            Guid = System.Guid.NewGuid().ToString();

            // Set current experience to be 1 above minimum.
            ExperienceTotal = LevelTableHelper.Instance.LevelDetailsList[Level - 1].Experience + 1;

        }

        /// <summary>
        /// Crate PlayerInfoModel from Monster
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(MonsterModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            ExperiencePoints = data.ExperienceTotal;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetCurrentHealthTotal;
            MaxHealth = data.GetMaxHealthTotal;
            Attack = data.GetAttack();
            Defense = data.GetDefense();
            CurrentHealth = data.GetCurrentHealthTotal;

            Speed = data.Speed;
            Defense = data.Defense;
            Attack = data.Attack;
            MaxHealth = data.MaxHealth;
            CurrentHealth = data.CurrentHealth;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            // Give the copy a differet quid, so it can be used in the battles as a copy
            Guid = System.Guid.NewGuid().ToString();

            // Set amount to give to be 1 below max for that level.
            ExperienceRemaining = LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience - 1;

        }

        public override string FormatOutput()
        {
            var myReturn = string.Empty;
            myReturn += Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();

            if (PlayerType == PlayerTypeEnum.Character)
            {
                myReturn += " , Total Experience : " + ExperienceTotal;
                myReturn += " , Damage : " + GetDamageTotalString;
                myReturn += " , Attack :" + GetAttackTotal;
                myReturn += " , Defense :" + GetDefenseTotal;
                myReturn += " , Speed :" + GetSpeedTotal;
            }

            myReturn += " , Items : " + ItemSlotsFormatOutput();

            return myReturn;
        }
    }
}