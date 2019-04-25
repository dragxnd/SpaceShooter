using UnityEngine;

public class HitShipSound : Sound
{
    public HitShipSound()
    {
        Path = "Sounds/Sfx/Sfx 2";
        Loop = false;

        Clip = Resources.Load(Path) as AudioClip;
    }
}

