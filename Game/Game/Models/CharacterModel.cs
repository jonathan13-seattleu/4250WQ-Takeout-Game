using Game.Services;
using SQLite;

namespace Game.Models
{
    /// <summary>
    //Default Character. The model will get a guid, name, and description
    /// </summary>
    public class CharacterModel : BasePlayerModel<CharacterModel>
    {
        //Constructor
        public CharacterModel() 
        {
            Guid = Id;
            PlayerType = PlayerTypeEnum.Character;
            Name = "New Character";
            Description = "Character Description";
            Level = 1;
            ImageURI = "icon_new.png";
        }
    }
}