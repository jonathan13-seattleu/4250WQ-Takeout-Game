﻿namespace Game.Models
{
    /// <summary>
    /// The Conditions a round can have
    /// </summary>
    public enum BattleModeEnum
    {
        // Invalid State
        Unknown = 0,

        // Default, just click until the end
        SimpleNext = 1,

        // Allow user to choose ability instead of action
        SimpleAbility = 2,

        // Map that just clicks until the end, monsters move, players don't move
        MapNext = 3,

        // Map that allows characters to choose ability, monsters move, players don't ove
        MapAbility = 4,

        // Map that allows characters to move and choose ability
        MapFull = 5,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class BattleModeEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this BattleModeEnum value)
        {
            // Default String
            var Message = "Unknown";

            switch (value)
            {

                case BattleModeEnum.MapFull:
                    Message = "Map All Actions";
                    break;

                case BattleModeEnum.MapNext:
                    Message = "Map Next Button";
                    break;

                case BattleModeEnum.MapAbility:
                    Message = "Map Abilities";
                    break;

                case BattleModeEnum.SimpleNext:
                    Message = "Simple Next";
                    break;

                case BattleModeEnum.SimpleAbility:
                    Message = "Simple Abilities";
                    break;

                case BattleModeEnum.Unknown:
                default:
                    break;
            }
            return Message;
        }
    }
}