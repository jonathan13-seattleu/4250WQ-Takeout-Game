using Game.Services;
using SQLite;

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
    public class CharacterModel : BasePlayerModel<CharacterModel>
    {
        //Default Character. The model will get a guid, name, and description
        public CharacterModel() 
        {
            Guid = Id;
            Name = "New Character";
            Description = "Character Description";
            Level = 1;
            ImageURI = "icon_new.png";
        }
    }
}