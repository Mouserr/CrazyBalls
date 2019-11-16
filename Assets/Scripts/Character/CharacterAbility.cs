using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
   public abstract class CharacterAbility
   {
       private readonly CharacterAbilityType _type;
       public CharacterAbilityType Type => _type;

       public CharacterAbility(CharacterAbilityType type)
       {
           _type = type;
       }
   }
}
