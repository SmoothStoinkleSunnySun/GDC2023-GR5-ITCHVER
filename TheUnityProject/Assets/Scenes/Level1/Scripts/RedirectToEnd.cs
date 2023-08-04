using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Scenes.Level1.Scripts
{
    public class RedirectToEnd : MonoBehaviour
    {
        bool _notEntered = true;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || !_notEntered) return;
            StartCoroutine(RedirectToAScene());
            _notEntered = false;
            GameObject.FindGameObjectWithTag("Speedrunner").GetComponent<SpeedRunModeTracker>().stopTimer();
            
            IEnumerator RedirectToAScene()
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
        
    }
}
