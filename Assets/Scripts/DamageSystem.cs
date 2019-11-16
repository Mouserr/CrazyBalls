namespace Assets.Scripts
{
    public static class DamageSystem
    {
        public static bool ApplyDamage(ICharacter attacker, ICharacter defender)
        {
            defender.Stats[CharacterStatType.Health].AddValue( - attacker.Stats[CharacterStatType.PassiveDamage].CurrentValue);
            return defender.Stats[CharacterStatType.Health].CurrentValue <= 0;
        }
    }
}