using UnityEngine;

public static class Extensions
{
    private static Camera mainCamera;
    public static Camera MainCamera
    {
        get
        {
            if(mainCamera==null)
            {
                mainCamera = Camera.main;
            }
            return mainCamera;
        }
        set
        {
            mainCamera = value;
        }
    }

    public static Bounds OrthographicBounds()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = MainCamera.orthographicSize * 2;
        Bounds bounds = new Bounds(MainCamera.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}

