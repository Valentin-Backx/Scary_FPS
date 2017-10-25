using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour {

    public LayerMask interactablesMask;

    Transform _cameraTransform;


    static InteractableDetector _Instance;
    public static InteractableDetector Instance
    {
      get
      {
        return _Instance;
      }
    }
    void Awake()
{
    if (_Instance != null && _Instance != this)
    {
        Destroy(this);
        return;
    }
    _Instance = this;
    }

    public static Collider CurrentlyDetected
    {
        get
        {
            return Instance._currentlyDetected;
        }
    }

    Collider _currentlyDetected;

	// Use this for initialization
	void Start () {
        _cameraTransform = Camera.main.transform;
	}

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(_cameraTransform.position,_cameraTransform.forward,out hit,Mathf.Infinity,interactablesMask))
        {
            _currentlyDetected = hit.collider;
        }else

        {
            _currentlyDetected = null;
        }
    }





}
