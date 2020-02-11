﻿using Game.Services;

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
    public class CharacterModel : BaseModel<CharacterModel>
    {
        // The living status of the Character
        public bool Alive { get; set; } = true;

        // The Level of the Character based on their ExperienceTotal
        public int Level { get; set; } = 0;

        // The total amount of experience points the Character possesses
        public int ExperienceTotal { get; set; } = 0;

        // The Speed attribute of the Character
        public int Speed { get; set; } = 0;

        // The Defense attribute of the Character
        public int Defense { get; set; } = 0;

        // The Attack attribute of the Character
        public int Attack { get; set; } = 0;

        // The amount of health points the Character possesses
        public int CurrentHealth { get; set; } = 0;

        // The maximum amount of health points the Character can possess
        public int MaxHealth { get; set; } = 0;

        // The Item equipped to the Character's head
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

        // Add Unique attributes for Item

        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>
        public CharacterModel() {
            ImageURI = ItemService.DefaultImageURI;
        }

        /// <summary>
        /// Constructor to create an item based on what is passed in
        /// </summary>
        /// <param name="data"></param>
        public CharacterModel(CharacterModel data)
        {
            Update(data);
        }

        /// <summary>
        /// Update the Record
        /// </summary>
        /// <param name="newData">The new data</param>
        public override void Update(CharacterModel newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            /*Name = newData.Name;
            Description = newData.Description;
            Value = newData.Value;
            Attribute = newData.Attribute;
            Location = newData.Location;
            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;
            Range = newData.Range;
            Damage = newData.Damage;*/
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var myReturn = Name + " , " /*+
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