using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Level1.Scripts
{
    public class Cutscene : MonoBehaviour
    {
        //helo this is cutscene be nice idk
        [TextArea][SerializeField] string notes;

        [Serializable]
        struct Cutscenes
        {
            public Sprite[] sprites;
        }

        [SerializeField] Cutscenes[] coolCutscenes;

        [SerializeField] float imageTimer;
        [SerializeField] Animator darkAnim;
        [SerializeField] Image img;
        [SerializeField] PlayerMove playerScript;
        [SerializeField] GameObject ambiences; //lmao
        [SerializeField] AudioSource sfxA;
        [SerializeField] AudioClip endFlash;
        [SerializeField] GameObject audSource;
        public int CutsceneToPlay { get; set; }
        public static Cutscene Instance
        {
            get;
            set;
        }
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        public void startCutscene()
        {
            StartCoroutine(imageIntervals(imageTimer));
            playerScript.AllowMovement = false;
            ambiences.SetActive(false);

        }
        IEnumerator imageIntervals(float timer)
        {
            audSource.SetActive(true);
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
            playerScript.AllowMovement = true;
            sfxA.PlayOneShot(endFlash);
            ambiences.SetActive(true);
            audSource.SetActive(false);

            StopCoroutine(imageIntervals(timer));
        }
    }
}
