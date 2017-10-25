using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCam : MonoBehaviour {
    [SerializeField]
    Player _player;

    Transform _myTransform;

	// Use this for initialization
	void Start () {
        _player.DamageEvent += _player_DamageEvent;
        _myTransform = transform;
	}

    private void _player_DamageEvent(float arg1, Vector3 arg2)
    {
        StartCoroutine(Shake());
    }

    [SerializeField]
    AnimationCurve _leftRightShake;

    [SerializeField]
    AnimationCurve _topBottomShake;

    [SerializeField]
    float _shakeDuration = 0.7f;

    [SerializeField]
    float _amplitude=10f;

    [SerializeField]
    float _recoveryRate = 1f;

    IEnumerator Shake()
    {
        float currentTimer = _shakeDuration;

        while(currentTimer>0f)
        {

            _myTransform.Rotate(Vector3.up, _leftRightShake.Evaluate((_shakeDuration - currentTimer)%lastFrameTimeY) * _amplitude,Space.Self);
            
            _myTransform.Rotate(Vector3.right, _topBottomShake.Evaluate((_shakeDuration - currentTimer)%lastFrameTimeX) * _amplitude, Space.Self);

            currentTimer -= Time.deltaTime;
            yield return null;
        }
        while(!Mathf.Approximately(0f,Quaternion.Angle(_myTransform.localRotation,Quaternion.identity)))
        {
            yield return null;
            _myTransform.localRotation = Quaternion.RotateTowards(_myTransform.localRotation, Quaternion.identity, Time.deltaTime * _recoveryRate);
        }
    }

    float lastFrameTimeY
    {
        get
        {
            return _leftRightShake[_leftRightShake.length - 1].time;
        }
    }

    float lastFrameTimeX
    { get
        {
            return _topBottomShake[_topBottomShake.length - 1].time;
        }
    }
}
