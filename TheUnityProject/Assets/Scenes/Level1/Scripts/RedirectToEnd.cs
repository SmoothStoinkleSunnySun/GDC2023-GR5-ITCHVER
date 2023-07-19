using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectToEnd : MonoBehaviour
{
    bool notEntered = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && notEntered)
        {
            StartCoroutine(RedirectToAScene());
            notEntered = false;
            GameObject.FindGameObjectWithTag("Speedrunner").GetComponent<SpeedRunModeTracker>().stopTimer();
        }
    }

    private IEnumerator RedirectToAScene()
    {
        //from https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html        
        //using scenebuildindex is for nerds

        //load the scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync($"Ending");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
