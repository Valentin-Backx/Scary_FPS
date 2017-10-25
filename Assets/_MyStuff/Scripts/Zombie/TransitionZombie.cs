using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZombie : MonoBehaviour {

	Animator _anim;

	// Use this for initialization
	void Start () {
		_anim = GetComponent<Animator>();

		_anim.Play("Crouching");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
