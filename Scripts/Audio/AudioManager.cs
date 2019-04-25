using UnityEngine;
using System.Collections;
using Tools;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{

    private GameObject soundObject;
    private readonly string soundObjectPath = "Prefabs/Audio/Sound";
    private Camera mainCamera;

    public void Start()
    {
        mainCamera = Camera.main;
        soundObject = Resources.Load(soundObjectPath) as GameObject;
        PoolManager.WarmPool(soundObject, 1);
    }

    public void PlaySound(Sound sound)
    {
        AudioSource.PlayClipAtPoint(sound.Clip, mainCamera.transform.position, 1);
    }

    public void PlayMusic(Sound music)
    {
        GameObject playSound = PoolManager.SpawnObject(soundObject);
        AudioSource source = playSound.GetComponent<AudioSource>();
        music.AudioSource = source;
        music.AssignedGameObject = playSound;
        source.clip = music.Clip;
        source.loop = music.Loop;
        source.Play();
    }

    public void StopMusic(Sound music)
    {
        music.AudioSource.Stop();
        PoolManager.ReleaseObject(music.AssignedGameObject);
    }
}
