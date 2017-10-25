using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkAppearance : MonoBehaviour {

	[SerializeField]	
	Light[] _lights;

	[SerializeField]
	GameObject[] _gameObjects;

	[SerializeField]
	float _timerBeforeLightsUp = 0.2F;

	[SerializeField]
	float _timerBeforeLightsBackOff = 1f;

	[SerializeField]
	float _timerBeforeLightsBackOn = 0.5f;

	[SerializeField]
	MeshRenderer[] _neonRenderers;

	[SerializeField]
	Material _offLamp;

	[SerializeField]
	Material _onLamp;

	bool _ran=false;

	void OnTriggerEnter()
	{
		if(_ran)
		{
			return;
		}
		_ran=true;
		ToggleLights(false);
		ActivateAppearance();
		StartCoroutine(TimerBeforeLightsOn());
	}

	void ActivateAppearance()
	{
		for (int i = 0; i < _gameObjects.Length; i++) {
			_gameObjects[i].SetActive(true);
		}
	}

	void ToggleLights(bool toggle)
	{
		for (int i = 0; i < _lights.Length; i++) {
			_lights[i].enabled = toggle;
		}

		for (int i = 0; i < _neonRenderers.Length; i++) {
			_neonRenderers[i].material = toggle?_onLamp:_offLamp;	
		}
	}

	IEnumerator TimerBeforeLightsOn()
	{
		yield return new WaitForSeconds(_timerBeforeLightsUp);

		ToggleLights(true);
		StartCoroutine(TimerBeforeLightsBackOff());
	}

	IEnumerator TimerBeforeLightsBackOff()
	{
		yield return new WaitForSeconds(_timerBeforeLightsBackOff);
		ToggleLights(false);
		StopAppearance();
		StartCoroutine(TimerBeforeLightsBackOn());
	}

	IEnumerator TimerBeforeLightsBackOn()
	{
		yield return new WaitForSeconds(_timerBeforeLightsBackOn);
		ToggleLights(true);
		Destroy(gameObject);
	}

	void StopAppearance()
	{
		for (int i = 0; i < _gameObjects.Length; i++) {
			_gameObjects[i].SetActive(false);
		}
	}

}
