using UnityEngine;

public class TakeBonus : Sound
{
    public TakeBonus()
    {
        Path = "Sounds/Sfx/Sfx 5";
        Loop = false;

        Clip = Resources.Load(Path) as AudioClip;
    }
}

