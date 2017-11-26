using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {


    [SerializeField] Monster[] _monsters;

    /// <summary>
    /// is any monster in aggro mode 
    /// </summary>
    public bool monsterAggroing
    {
        get
        {
            for (int i = 0; i < _monsters.Length; i++)
            {
                if(_monsters[i].aggro)
                {
                    return true;
                }
            }
            return false;
        }
    }
    
    static LevelManager _Instance;
    public static LevelManager Instance
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
        _endlevelScreen = GUIManager.Instance.GetScreen("endLevelScreen");

        GameManager.Instance.ResetEvent += Instance_ResetEvent;
    }

    private void Instance_ResetEvent()
    {
        _endlevelScreen.SetActive(false);
    }

    //go to next level
    internal void Next()
    {
        GameManager.Instance.GoToNexLevel();
    }

    GameObject _endlevelScreen;

    bool _levelEnded = false;
    public bool levelEnded
    {
        get
        {
            return _levelEnded;
        }
    }

    public void EndLevel()
    {
        _levelEnded = true;
        _endlevelScreen.SetActive(true);

        Player.Instance.FreezeMovement();
    }
}
