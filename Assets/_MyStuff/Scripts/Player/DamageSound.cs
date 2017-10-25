using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class DamageSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Player>().DamageEvent += DamageSound_DamageEvent;
	}

    [SerializeField]
    AudioSource[] _damageSounds;

    [SerializeField]
    AudioSource[] _fleshHits;

    private void DamageSound_DamageEvent(float obj,Vector3 damagePos)
    {
        int hurtIndexRandom = (int)(Random.value * _damageSounds.Length);
        _damageSounds[hurtIndexRandom].Play();

        int indexRandom = (int)(Random.value * _fleshHits.Length);
        _fleshHits[indexRandom].Play();

    }
}
