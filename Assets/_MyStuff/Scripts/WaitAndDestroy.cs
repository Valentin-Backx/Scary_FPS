using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndDestroy : MonoBehaviour {


    [SerializeField]
    float waitBeforeDestroy=0.1f;

    void OnTriggerEnter()
    {
        StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        float timer = 0f;
        while(timer<waitBeforeDestroy)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        Destroy(gameObject);
    }
}
