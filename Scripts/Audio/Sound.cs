using UnityEngine;

public class Sound
{
    public Sound(){}

    public Sound(AudioClip clip, bool loop)
    {
        Clip = clip;
        Loop = loop;
    }

    public string Path { get; set; }
    public AudioClip Clip { get; set; }
    public AudioSource AudioSource { get; set; }
    public GameObject AssignedGameObject { get; set; }
    public bool Loop { get; set; }

}

