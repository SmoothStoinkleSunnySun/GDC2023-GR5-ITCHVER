using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectToStart : MonoBehaviour
{
    bool notPressed = true;
    
    public void redirectNow()
    {
        if (notPressed)
        {
            StartCoroutine(RedirectToAScene());
            notPressed = false;
            Cursor.visible = false;
        }
        
    }
    private IEnumerator RedirectToAScene()
    {
        //from https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html        
        //using scenebuildindex is for nerds

        //load the scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync($"StartCutscene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
