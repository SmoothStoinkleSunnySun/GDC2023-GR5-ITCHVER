using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Scenes.Level1.Scripts
{
    public class RedirectToStart : MonoBehaviour
    {
        bool _notPressed = true;
    
        public void redirectNow()
        {
            if (!_notPressed) return;
            StartCoroutine(RedirectToAScene());
            _notPressed = false;
            Cursor.visible = false;
            
            IEnumerator RedirectToAScene()
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
    }
}
