using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    static Player _Instance;
    public static Player Instance
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


    [SerializeField]
    float maxLife=100f;
    
    float _currentLife;
    internal void Damage(float damage)
    {
        this._currentLife -= damage;
    }
}
