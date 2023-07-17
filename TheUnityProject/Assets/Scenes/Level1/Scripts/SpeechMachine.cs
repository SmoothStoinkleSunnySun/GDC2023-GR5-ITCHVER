using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechMachine : MonoBehaviour
{
    [SerializeField] float thonkTime;
    [SerializeField] GameObject thonker;
    [SerializeField] PlayerMove playerScript;
    public void thinkingText()
    {
        StartCoroutine(thonk(thonkTime));
    }
    IEnumerator thonk(float timer)
    {
        thonker.SetActive(true);
        yield return new WaitForSeconds(timer);
        thonker.SetActive(false);
        StopCoroutine(thonk(timer));
        playerScript.AllowMovement = true;
    }
}
