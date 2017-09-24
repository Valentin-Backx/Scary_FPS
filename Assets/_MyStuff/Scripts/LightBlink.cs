using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightBlink : MonoBehaviour {


    [SerializeField]
    float _minPeriod=0.05f;


    [SerializeField]
    float _maxPeriod=0.8f;



    [SerializeField]
    Light[] _lights;


    [SerializeField]
    MeshRenderer[] _renderers;


	[SerializeField]
	Material _offMaterial;

	[SerializeField]
	Material onMaterial;

    bool _currentlyToggled = true;

    void Start()
    {
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        float time = UnityEngine.Random.value * (_maxPeriod - _minPeriod) + _minPeriod;
        float currentTimer=0f;
        Toggle();
        while(currentTimer<time)
        {
            yield return null;
            currentTimer += Time.deltaTime;
        }
        StartCoroutine(BlinkRoutine());
    }

    void Toggle()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].enabled = !_currentlyToggled;
        }
		for (int i = 0; i < _renderers.Length; i++)
        {
			_renderers[i].material = _currentlyToggled? _offMaterial:onMaterial;
//            _runTimeMats[i].SetColor("_EmissionColor", _currentlyToggled?Color.black:Color.white);

        }
        _currentlyToggled = !_currentlyToggled;
    }

}
