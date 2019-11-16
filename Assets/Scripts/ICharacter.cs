namespace Assets.Scripts
{
    public interface ICharacter
    {
        Stat Health { get; set; }
        int Damage { get; }
    }
}