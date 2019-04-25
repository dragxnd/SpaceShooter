using UnityEngine;

[System.Serializable]
public class StarObject
{
    public float Speed;
    public GameObject gameObj;

    [HideInInspector]
    public Renderer Renderer;
}

