using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Teams
{
    public sealed class BattleGroup
    {
        private readonly int _groupSize;
        private readonly Character[] _members;

        public IEnumerable<Character> Members => _members.ToList();

        public BattleGroup(int groupSize = 3)
        {
            _groupSize = groupSize;
            _members = new Character[_groupSize];
        }

        public void SetMember(int position, Character member)
        {
            if(position<0 || position>=_groupSize) return;
            _members[position] = member;
        }
    }
}
