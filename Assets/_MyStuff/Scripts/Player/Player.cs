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

    public event Action<float,Vector3> DamageEvent;

    void Awake()
{
    if (_Instance != null && _Instance != this)
    {
        Destroy(this);
        return;
    }
    _Instance = this;
    }

    private void Start()
    {
        _currentLife = maxLife;
    }

    [SerializeField]
    float maxLife=100f;

    public float MaxLife
    {
        get
        {
            return maxLife;
        }
    }
    
    float _currentLife;
    public float currentLife
    {
        get
        {
            return _currentLife;
        }
    }
    internal void Damage(float damage)
    {
        this._currentLife -= damage;
        DamageEvent(damage,this.transform.position);
    }
}
