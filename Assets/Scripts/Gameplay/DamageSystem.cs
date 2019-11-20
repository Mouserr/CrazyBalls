namespace Assets.Scripts
{
    public static class DamageSystem
    {
        public static bool ApplyPassiveDamage(ICharacter attacker, UnitController defender)
        {
            return ApplyDamage(attacker, attacker.Stats[CharacterStatType.PassiveDamage].CurrentValue, defender);
        }

        public static bool ApplyDamage(ICharacter attacker, int damageValue, UnitController defender)
        {
            defender.Character.Stats[CharacterStatType.Health].AddValue( - damageValue);
            if (defender.Character.Stats[CharacterStatType.Health].CurrentValue <= 0)
            {
                defender.Die();
                return true;
            }

            return false;
        }
    }
}