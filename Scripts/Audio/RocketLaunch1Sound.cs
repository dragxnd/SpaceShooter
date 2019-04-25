using UnityEngine;

public class RocketLaunch1Sound : Sound
{
    public RocketLaunch1Sound()
    {
        Path = "Sounds/Sfx/Sfx 4";
        Loop = false;

        Clip = Resources.Load(Path) as AudioClip;
    }
}

