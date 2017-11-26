using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&&!LevelManager.Instance.monsterAggroing)
        {
            End();
        }
    }

    private void End()
    {
        LevelManager.Instance.EndLevel();
    }

    

    private void Update()
    {
        if(LevelManager.Instance.levelEnded)
        {
            if(Input.GetMouseButton(0))
            {
                LevelManager.Instance.Next();
            }
        }
    }
}
