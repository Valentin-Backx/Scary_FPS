using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {


    static Interactor _Instance;
    public static Interactor Instance
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

    IInteractable _currentInteract;

    public static void SetInteract(IInteractable i)
    {
        Instance._currentInteract = i;
    }
    
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.E))
        {
            _currentInteract.Activate(true);
        }
	}
}
