using UnityEngine;

public class Stars : MonoBehaviour
{
    public StarObject[] StarObjects;
    private float offset;
    private bool isInit = false;

    void Awake()
    {
        for (int i = 0; i < StarObjects.Length; i++)
        {
            StarObjects[i].Renderer = StarObjects[i].gameObj.GetComponent<Renderer>();
        }
        isInit = true;
    }


    void Update()
    {
        if (!isInit) return;
        for(int i=0;i<StarObjects.Length;i++)
        {
            offset = Time.time * StarObjects[i].Speed;
            StarObjects[i].Renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        }
    }
}



