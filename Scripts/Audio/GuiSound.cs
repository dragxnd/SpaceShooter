using UnityEngine;

public class GuiSound : Sound
{
    public GuiSound()
    {
        Path = "Sounds/Gui/Click";
        Loop = false;

        Clip = Resources.Load(Path) as AudioClip;
    }
}

