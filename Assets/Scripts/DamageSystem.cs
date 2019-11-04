namespace Assets.Scripts
{
    public static class DamageSystem
    {
        public static bool ApplyDamage(IBall attacker, IBall defender)
        {
            defender.Health.CurrentValue -= attacker.Damage;
            return defender.Health.CurrentValue <= 0;
        }
    }
}