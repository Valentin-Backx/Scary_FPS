using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour {

    public LayerMask interactablesMask;
    
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
    

    private void Update()
    {
        if(Camera.main==null)
        {
            return;
        }
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit,Mathf.Infinity,interactablesMask))
        {
            _currentlyDetected = hit.collider;
        }else
        {
            _currentlyDetected = null;
        }
    }
}
