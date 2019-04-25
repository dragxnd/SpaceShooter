using UnityEngine;
using System.Collections;
using Tools;

public class MonoBehaviourTools : MonoBehaviourSingleton<MonoBehaviourTools>
{
    public Coroutine StartCor(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }
}
