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
        //helo this is cutscene be nice idk
        [TextArea][SerializeField] string notes;

        [Serializable]
        private struct Cutscenes
        {
            public Sprite[] sprites;
        }

        [SerializeField] Cutscenes[] coolCutscenes;

        [SerializeField] float imageTimer;
        [FormerlySerializedAs("dark_anim")] [SerializeField] Animator darkAnim;
        [SerializeField] Image img;
        [SerializeField] PlayerMove playerScript;
        [SerializeField] bool isStartCutscene;
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
            yield return new WaitUntil(() => darkAnim.GetCurrentAnimatorStateInfo(0).IsName("black"));
            img.enabled = false;
            darkAnim.Play("fade out");
            if (!isStartCutscene)
            {
                playerScript.AllowMovement = true;
            }

            if (isStartCutscene)
            {
                StartCoroutine(RedirectToAScene());
                //called a total of 1 times in the game
                GameObject.FindGameObjectWithTag("Speedrunner").GetComponent<SpeedRunModeTracker>().stopTimer();
            }
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
