using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearManager : MonoBehaviour {
    
    [SerializeField] float _startingFearLevel=0f;
    
    [SerializeField] float _maxFearLevel=100F;

    float _currentFear;
    public float currentFear
    {
        get
        {
            return _currentFear / _maxFearLevel;
        }
    }

    [SerializeField] float _fearIncreaseSpeed=1f;


    static FearManager _Instance;
    public static FearManager Instance
    {
      get
      {
        return _Instance;
      }
    }

    private void Start()
    {
        Reset();
        GameManager.Instance.ResetEvent += Instance_ResetEvent;
        DontDestroyOnLoad(gameObject);
    }

    private void Instance_ResetEvent()
    {
        Reset();
    }

    private void Reset()
    {
        _currentFear = _startingFearLevel;
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


	// Update is called once per frame
	void Update ()
    {
	    if(LevelManager.Instance!=null&&LevelManager.Instance.monsterAggroing)
        {
            _currentFear = Mathf.Clamp(_currentFear + Time.deltaTime * _fearIncreaseSpeed, 0f, _maxFearLevel);
        }
	}
}
