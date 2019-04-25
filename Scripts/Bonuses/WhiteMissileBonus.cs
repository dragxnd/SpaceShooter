class WhiteMissileBonus : Bonus
{
    private readonly int heal = 60;

    public override void Execute(PlayerShip playerShip)
    {
        WhiteMissileWeapon whiteMissileWeapon = new WhiteMissileWeapon();
        whiteMissileWeapon.Init(playerShip.shootPoins);
        playerShip.CurrentWeapon = whiteMissileWeapon;
    }

}
