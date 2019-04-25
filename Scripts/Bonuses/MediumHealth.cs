class MediumHealth : Bonus
{
    private readonly int heal = 40;

    public override void Execute(PlayerShip playerShip)
    {
        playerShip.CurrentHealth += heal;
    }

}
