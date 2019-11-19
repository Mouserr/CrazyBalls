using System.Collections.Generic;
using Assets.Scripts.Core.ConstantsContainers;

namespace Assets.Scripts.Units
{
    public class UnitType : IConstantsContainer
    {
        public const string Goki = "Goki";
        public const string Mob = "Mob";
        public const string MobBoss = "MobBoss";

        private static readonly List<string> constants = new List<string>
		{
		    Goki,
		    Mob, 
			MobBoss, 
		};

        public List<string> GetConstants()
        {
            return constants;
        }
    }
}