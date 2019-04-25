using UnityEngine;

public class DestroyShipSound : Sound
{
    public DestroyShipSound()
    {
        Path = "Sounds/Sfx/Sfx 1";
        Loop = false;

        Clip = Resources.Load(Path) as AudioClip;
    }
}

