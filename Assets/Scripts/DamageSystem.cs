namespace Assets.Scripts
{
    public static class DamageSystem
    {
        public static bool ApplyDamage(ICharacter attacker, ICharacter defender)
        {
            defender.Health.CurrentValue -= attacker.Damage;
            return defender.Health.CurrentValue <= 0;
        }
    }
}