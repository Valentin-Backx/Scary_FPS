using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(RigidbodyFirstPersonController))]
public class KnockBack : MonoBehaviour {

    Rigidbody _rb;

    RigidbodyFirstPersonController _controller;

    // Use this for initialization
    void Start () {
        GetComponent<Player>().DamageEvent += KnockBack_DamageEvent;
        _rb = GetComponent<Rigidbody>();
        _controller = GetComponent<RigidbodyFirstPersonController>();

	}

    [SerializeField]
    float _minForce=10f;

    [SerializeField]
    float _maxForce=100f;

    [SerializeField]
    float _maxDamage=50f;

    private void KnockBack_DamageEvent(float obj,Vector3 position)
    {
        float force = Mathf.Lerp(_minForce, _maxForce, obj / _maxDamage);

        if(!_routineRunning)
        {
            StartCoroutine(ToggleController());
        }
        _resetTimer = true;

        print("bump "+force);
        _rb.AddForce((transform.position - position).normalized * force);
        _controller.enabled = false;
    }

    [SerializeField]
    float _timeToReKinematize = 1f;

    bool _routineRunning = false;
    bool _resetTimer;
    IEnumerator ToggleController()
    {
        _routineRunning = true;
        float currentTimer = _timeToReKinematize;

        while(currentTimer>0f)
        {

            if(_resetTimer)
            {
                _resetTimer = false;
                currentTimer = _timeToReKinematize;
            }

            yield return null;
            currentTimer -= Time.deltaTime;
        }

        _controller.enabled = true;
    }


}
