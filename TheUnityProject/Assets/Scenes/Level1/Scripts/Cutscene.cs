using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Scenes.Level1.Scripts
{
    public class Cutscene : MonoBehaviour
    {
        //helo this is cutscene be nice idk
        [TextArea] [SerializeField] private string notes;

        [Serializable]
        private struct Cutscenes
        {
            public Sprite[] sprites;
        }

        [SerializeField] private Cutscenes[] coolCutscenes;

        [SerializeField] private float imageTimer;
        [SerializeField] private Animator darkAnim;
        [SerializeField] private Image img;
        [SerializeField] private PlayerMove playerScript;
        [SerializeField] private GameObject ambiences; //lmao
        [SerializeField] private AudioSource sfxA;
        [SerializeField] private AudioClip endFlash;
        [SerializeField] private GameObject audSource;
        public int CutsceneToPlay { get; set; }
        public static Cutscene Instance { get; set; }

        private void Awake()
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

        private IEnumerator imageIntervals(float timer)
        {
            for (var i = 0; i < coolCutscenes[CutsceneToPlay].sprites.Length; i++)
            {
                darkAnim.Play("fade in");
                yield return new WaitUntil(() => darkAnim.GetCurrentAnimatorStateInfo(0).IsName("black"));

                if (!img.enabled) img.enabled = true;

                img.sprite = coolCutscenes[CutsceneToPlay].sprites[i];
                if (i == coolCutscenes[CutsceneToPlay].sprites.Length - 1) sfxA.PlayOneShot(endFlash);
                darkAnim.Play("fade out");
                yield return new WaitForSeconds(timer);
            }

            darkAnim.Play("fade in");
            yield return new WaitUntil(() => darkAnim.GetCurrentAnimatorStateInfo(0).IsName("black"));
            img.enabled = false;
            darkAnim.Play("fade out");
            playerScript.AllowMovement = true;
            ambiences.SetActive(true);
            audSource.SetActive(false);

            StopCoroutine(imageIntervals(timer));
        }
    }
}