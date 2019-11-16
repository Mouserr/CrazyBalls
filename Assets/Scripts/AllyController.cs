using UnityEngine;

namespace Assets.Scripts
{
    public class AllyController : MonoBehaviour
    {
        private Character _character;

        public Character Character => _character;

        public CharacterStat Health => _character.Stats[CharacterStatType.Health];
        public CharacterStat Energy => _character.Stats[CharacterStatType.Energy];
        public CharacterStat PassiveDamage => _character.Stats[CharacterStatType.PassiveDamage];

        public void SetCharacter(Character character)
        {
            _character = _character ?? character;
        }

    }
}