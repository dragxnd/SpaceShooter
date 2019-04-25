class SmallHealth : Bonus
{
    private readonly int heal = 20;

    public override void Execute(PlayerShip playerShip)
    {
        playerShip.CurrentHealth += heal;
    }

}
