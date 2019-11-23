namespace Assets.Scripts
{
    public static class DamageSystem
    {
        public static bool ApplyPassiveDamage(UnitController attacker, UnitController defender)
        {
            return ApplyDamage(attacker, attacker.Character.Stats[CharacterStatType.PassiveDamage].CurrentValue, defender);
        }

        public static bool ApplyDamage(UnitController attacker, int damageValue, UnitController defender)
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