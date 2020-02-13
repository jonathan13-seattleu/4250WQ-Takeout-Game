using Game.Services;
using System;

namespace Game.Models
{
    /// <summary>
    /// Item for the Game
    /// 
    /// The Items that a character can use, a Monster may drop, or may be randomly available.
    /// The items are stored in the DB, and during game time a random item is selected.
    /// The system supports CRUDi operatoins on the items
    /// When in test mode, a test set of items is loaded
    /// When in run mode the items from from the database
    /// When in online mode, the items come from an api call to a webservice
    /// 
    /// When characters or monsters die, they drop items into the Items Pool for the Battle
    /// 
    /// </summary>
    public class MonsterModel : BaseModel<MonsterModel>
    {
        public override string Name { get; set; } = "Unkown";
        public override string Description { get; set; } = "Unknown";

        public bool Alive { get; set; } = true;
        // Total Score
        public int Level { get; set; }

        // The Date the game played, and when the score was saved
        public int ExperienceTotal { get; set; }

        // Tracks if auto battle is true, or if user battle = false
        public int Speed { get; set; }

        // The number of turns the battle took to finish
        public int Defense { get; set; }

        // The number of rounds the battle took to finish
        public int Attack { get; set; }

        // The count of monsters slain during battle
        public int CurrentHealth { get; set; }

        // The total experience points all the characters received during the battle
        public int MaxHealth { get; set; }

        public ItemModel Head { get; set; } = null;

        // The Item equipped to the Character's body
        public ItemModel Body { get; set; } = null;

        // The Item equipped to the Character's primary hand
        public ItemModel PrimaryHand { get; set; } = null;

        // The Item equipped to the Character's off hand
        public ItemModel OffHand { get; set; } = null;

        // The Item equipped to the Character's left finger
        public ItemModel LeftFinger { get; set; } = null;

        // The Item equipped to the Character's right finger
        public ItemModel RightFinger { get; set; } = null;

        // The Item equipped to the Character's feet
        public ItemModel Feet { get; set; } = null;

        public MonsterModel()
        {
            ImageURI = ItemService.DefaultImageURI;
        }
        public MonsterModel(MonsterModel data)
        {
            Update(data);
        }

        /// <summary>
        /// Update the Record
        /// </summary>
        /// <param name="newData">The new data</param>
        public override void Update(MonsterModel newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Description = newData.Description;
            Alive = newData.Alive;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            Speed = newData.Speed;
            Defense = newData.Defense;
            Attack = newData.Attack;
            CurrentHealth = newData.CurrentHealth;
            MaxHealth = newData.MaxHealth;
            Head = newData.Head;
            Body = newData.Body;
            PrimaryHand = newData.PrimaryHand;
            OffHand = newData.OffHand;
            LeftFinger = newData.LeftFinger;
            RightFinger = newData.RightFinger;
            Feet = newData.Feet;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var myReturn = Name /*+ " , " +
                            Description + " for " +
                            Location.ToString() + " with " +
                            Attribute.ToString() +
                            "+" + Value + " , " +
                            "Damage : " + Damage + " , " +
                            "Range : " + Range*/;

            return myReturn.Trim();
        }
    }
}