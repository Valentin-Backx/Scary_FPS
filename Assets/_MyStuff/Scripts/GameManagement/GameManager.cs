using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

    [SerializeField] string[] _levels;
    
    static GameManager _Instance;
    public static GameManager Instance
    {
      get
      {
        return _Instance;
      }
    }

    /// <summary>
    /// event shot right before a level is loaded
    /// </summary>
    public event Action ResetEvent;

    void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
            return;
        }
        _Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    int currentLevel=0;


    void Start()
    {
        GoToScene(0);
    }

    void GoToScene(int levelIndex)
    {
        ResetEvent();
        SceneManager.LoadScene(_levels[levelIndex]);
    }

    public void GoToNexLevel()
    {
        currentLevel++;
        GoToScene(currentLevel);
    }
}
