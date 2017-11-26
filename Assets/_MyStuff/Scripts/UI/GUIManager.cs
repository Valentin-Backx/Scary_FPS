using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {
    
    [SerializeField] GameObject[] _screens;
    
    static GUIManager _Instance;
    public static GUIManager Instance
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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public GameObject GetScreen(string str)
    {
        Transform res = transform.Find(str);
        
        if(res==null)
        {
            throw new System.Exception("Pas d'ecran UI de nom " + str);
        }
        return res.gameObject;
    }

}
