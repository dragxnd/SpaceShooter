using UnityEngine;

public class WhiteMissleSound : Sound
{
    public WhiteMissleSound()
    {
        Path = "Sounds/Sfx/Sfx 3";
        Loop = false;

        Clip = Resources.Load(Path) as AudioClip;
    }
}

