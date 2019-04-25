using DG.Tweening;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform objectTransform;  
    public RectTransform RectTransformBar;
    public float StartHealth { get; set; }

    private RectTransform baseBarTransform;
    private Camera camera;
    private Vector2 startSizeDelta;
    private Tweener barMoveTween;
    private float moveDuration = 1;
    private bool isInit = false;
    private Vector2 anchorMinOffset = new Vector2(0, 0.05f);

    private void Start()
    {
        startSizeDelta = RectTransformBar.sizeDelta;
    }


    public void Init()
    {
        camera = Extensions.MainCamera;
        baseBarTransform = GetComponent<RectTransform>();
        baseBarTransform.SetSiblingIndex(0);
        isInit = true;
    }

    void Update()
    {
        if (!isInit) return;
        if (baseBarTransform != null && camera != null)
        {
            Vector2 screenPos = camera.WorldToViewportPoint(objectTransform.position);
            baseBarTransform.anchorMin = screenPos + anchorMinOffset;
            baseBarTransform.anchorMax = screenPos + anchorMinOffset;           
        }
    }

    public void UpdateBar(float newHp)
    {
        if (!isInit) return;
        if (barMoveTween != null) barMoveTween.Kill();
        float percents = newHp / (StartHealth / 100);
        barMoveTween = RectTransformBar.DOSizeDelta(new Vector2(startSizeDelta.x - (startSizeDelta.x - (startSizeDelta.x / 100) * percents), startSizeDelta.y), moveDuration);
    }


    public void Remove()
    {      
        if (barMoveTween != null) barMoveTween.Kill();
        StartHealth = 0;
        isInit = false;
        GameObject.Destroy(gameObject);
    }
}

