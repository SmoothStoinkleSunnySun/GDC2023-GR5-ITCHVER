using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace Scenes.Level1.Scripts
{
    public class StartCutscene: MonoBehaviour
    {
        //Lmao this script is scuffed and honestly doesn't need to be optimized due to living in its own scene
        //Have fun reading
        
        [TextArea][SerializeField] string notes;

        [Serializable]
        private struct Cutscenes
        {
            public Sprite[] sprites;
        }

        [SerializeField] Cutscenes[] coolCutscenes;

        [SerializeField] float imageTimer;
        [SerializeField] Animator darkAnim;
        [SerializeField] Image img;
        [SerializeField] PlayerMove playerScript;
        [SerializeField] bool isStartCutscene;
        [SerializeField] private AudioSource audSource;
        public int CutsceneToPlay { get; set; }

        public void startCutscene()
        {
            StartCoroutine(imageIntervals(imageTimer));
            if (!isStartCutscene)
            {
                playerScript.AllowMovement = false;
            }        
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator imageIntervals(float timer)
        {
            var originalVol = audSource.volume;
            audSource.volume = 0;
            while (audSource.volume < originalVol)
            {
                audSource.volume += 0.08f;
                yield return new WaitForSeconds(0.2f);
            }
            foreach (var t in coolCutscenes[CutsceneToPlay].sprites)
            {
                darkAnim.Play("fade in");
                yield return new WaitUntil(() => darkAnim.GetCurrentAnimatorStateInfo(0).IsName("black"));

                if (!img.enabled)
                {
                    img.enabled = true;
                }

                img.sprite = t;
                darkAnim.Play("fade out");
                yield return new WaitForSeconds(timer);
            }
            darkAnim.Play("fade in");
            while (audSource.volume > 0)
            {
                audSource.volume -= 0.06f;
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitUntil(() => darkAnim.GetCurrentAnimatorStateInfo(0).IsName("black"));
            StartCoroutine(RedirectToAScene());
            //called a total of 1 times in the game
            GameObject.FindGameObjectWithTag("Speedrunner").GetComponent<SpeedRunModeTracker>().stopTimer();

            StopCoroutine(imageIntervals(timer));
        }
        private IEnumerator RedirectToAScene()
        {
            //from https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html        
            //using scenebuildindex is for nerds

            //load the scene
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync($"Level1");

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
