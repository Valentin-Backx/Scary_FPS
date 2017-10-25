using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoorUI : MonoBehaviour {


    static InteractableDoorUI _Instance;
    public static InteractableDoorUI Instance
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
    
    public RectTransform hand;
    public GameObject visual;

    public void DisplayDoor()
    {
        visual.SetActive(true);
    }

    internal void HideDoor()
    {
        visual.SetActive(false);
    }
    
}
