using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FearUI : MonoBehaviour {


    [SerializeField] Image _fearBar;
    

    // Update is called once per frame
    void Update () {
        _fearBar.fillAmount = FearManager.Instance.currentFear;    	
	}
}
