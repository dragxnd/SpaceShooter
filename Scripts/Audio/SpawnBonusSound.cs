using UnityEngine;

public class SpawnBonus : Sound
{
    public SpawnBonus()
    {
        Path = "Sounds/Sfx/SpawnBonus";
        Loop = false;

        Clip = Resources.Load(Path) as AudioClip;
    }
}

