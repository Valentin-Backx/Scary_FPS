using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpookTrigger : MonoBehaviour {

    [SerializeField]
    Rigidbody bodyToMove;


    [SerializeField]
    float forceIntensity=1f;


    [SerializeField]
    Transform forceTarget;

    Vector3 _forceDirection;


    [SerializeField]
    float delayToSpook=0f;

	// Use this for initialization
	void Start () {
        _forceDirection = (forceTarget.position - bodyToMove.transform.position).normalized;
	}
	
    void OnTriggerEnter()
    {
        if(delayToSpook>0)
        {
            StartCoroutine(WaitSpook(Spook));
        }else
        {
            Spook();
        }
    }

    IEnumerator WaitSpook(Action callback)
    {
        float timer = 0f;
        while(timer < delayToSpook)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        callback();
    }


    void Spook()
    {
        bodyToMove.AddForce(_forceDirection * forceIntensity);
    }

}
