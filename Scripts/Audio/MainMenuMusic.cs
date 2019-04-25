using UnityEngine;

public class MainMenuMusic : Sound
{
    public MainMenuMusic()
    {
        Path = "Sounds/Music/MainMenuMusic";
        Loop = true;

        Clip = Resources.Load(Path) as AudioClip;
    }
}

