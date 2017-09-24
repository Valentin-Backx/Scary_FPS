using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyTools.SoundManager;

public class MySoundManager :  SoundManager{


    static MySoundManager _Instance;
    public static MySoundManager Instance
    {
        get
        {
            return _Instance;
        }
    }


    override protected void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
            return;
        }
        base.Awake();
        _Instance = this;
    }


    int _currentMusic = 0;
    [SerializeField]
    AudioClip[] musics;


    void Start()
    {
        //SoundManager.PlayMusic(musics[_currentMusic], 1f, false, true);

    }


    [SerializeField]
    AudioClip hit;

    internal static void PlayHit()
    {
        SoundManager.PlaySound(Instance.hit);
    }

	[SerializeField]
	Psycho_master psychoMaster;

	public static bool IsAggro;

	internal static void AggroMusic()
	{
		IsAggro=!IsAggro;
		Instance.psychoMaster.Terror_Bass_onClick();
		Instance.psychoMaster.Terror_Percs_onClick();
		Instance.psychoMaster.Terror_Intense_onClick();
	}


}
