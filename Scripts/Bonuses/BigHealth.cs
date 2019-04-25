public class BigHealth : Bonus
{
    private readonly int heal = 60;

    public override void Execute(PlayerShip playerShip)
    {
        playerShip.CurrentHealth += heal;
    }

}
