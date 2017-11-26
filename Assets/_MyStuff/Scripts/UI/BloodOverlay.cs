using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodOverlay : MonoBehaviour {

    public Material bloodOverlayMat;

    public float maxCutoff = 1f;
    public float minCutoff = 0.558f;
    
    public Image bloodOverlay;

    IEnumerator Start()
    {
        while(Player.Instance==null)
        {
            yield return null;
        }
        Player.Instance.DamageEvent += Player_DamageEvent;
        bloodOverlay.enabled = false;
    }

    private void Player_DamageEvent(float arg1, Vector3 arg2)
    {
        bloodOverlay.enabled = true;

        float calculatedCutoff = Mathf.Lerp(minCutoff, maxCutoff, Player.Instance.currentLife / Player.Instance.MaxLife);
        bloodOverlayMat.SetFloat("_Cutoff", calculatedCutoff);
    }

    private void OnApplicationQuit()
    {
        bloodOverlayMat.SetFloat("_Cutoff", 1f);
    }
}
