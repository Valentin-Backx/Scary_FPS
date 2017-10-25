using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(RigidbodyFirstPersonController))]
[RequireComponent(typeof(Rigidbody))]
public class Death : MonoBehaviour {

    Player _player;

    RigidbodyFirstPersonController _controller;

    Rigidbody _rb;

    public HeadBob _headBob;

    public event Action PlayerDeathEvent;

	// Use this for initialization
	void Start () {
        _controller = GetComponent<RigidbodyFirstPersonController>();
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
        _player.DamageEvent += _player_DamageEvent;

	}

    public float knockOverForce = 50f;

    public PhysicMaterial fallingMaterial;

    private void _player_DamageEvent(float arg1, Vector3 arg2)
    {
        if(_player.currentLife<=0f)
        {
            _controller.enabled = false;
            _rb.constraints = RigidbodyConstraints.None;
            _headBob.enabled = false;
            //_rb.drag = 0f;
            //_rb.angularDrag = 0f;
            _rb.AddTorque(
                UnityEngine.Random.value * knockOverForce,
                UnityEngine.Random.value * knockOverForce,
                UnityEngine.Random.value * knockOverForce,
                ForceMode.Impulse
            );

            
            PlayerDeathEvent();
            StartCoroutine(SwitchMaterialRoutine());

            //Rigidbody rb = _headBob.gameObject.AddComponent<Rigidbody>();

            //CharacterJoint cj =_headBob.gameObject.AddComponent<CharacterJoint>();

            //cj.connectedBody = _rb;
        }
    }

    public float timeToSwitchMaterial = 1f;

    IEnumerator SwitchMaterialRoutine()
    {
        yield return new WaitForSeconds(timeToSwitchMaterial);
        GetComponent<CapsuleCollider>().material = fallingMaterial;

    }

}
