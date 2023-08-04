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
            public AudioClip memoMusic;
        }

        [SerializeField] private Cutscenes[] coolCutscenes;

        [SerializeField] private float imageTimer;
        [SerializeField] private Animator darkAnim;
        [SerializeField] private Image img;
        [SerializeField] private PlayerMove playerScript;
        [SerializeField] private GameObject ambiences; //lmao
        [SerializeField] private AudioSource sfxA;
        [SerializeField] private GameObject audSource;
        [SerializeField] private AudioSource audSourceAudio;
        
        //private hidden
        private WaitUntil _waitForBlack;
        
        //public
        public int CutsceneToPlay { get; set; }
        public static Cutscene Instance { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            _waitForBlack = new WaitUntil(() => darkAnim.GetCurrentAnimatorStateInfo(0).IsName("black"));
        }

        public void startCutscene()
        {
            StartCoroutine(imageIntervals(imageTimer));
            playerScript.AllowMovement = false;
            ambiences.SetActive(false);
            
            IEnumerator imageIntervals(float timer)
            {
                audSource.SetActive(true);
                audSourceAudio.clip = coolCutscenes[CutsceneToPlay].memoMusic;
                audSourceAudio.Play();
            
                var imgTime = new WaitForSeconds(timer);

                for (var i = 0; i < coolCutscenes[CutsceneToPlay].sprites.Length; i++)
                {
                    darkAnim.Play("fade in");
                    yield return _waitForBlack;

                    if (!img.enabled) img.enabled = true;

                    img.sprite = coolCutscenes[CutsceneToPlay].sprites[i];
                    if (i == coolCutscenes[CutsceneToPlay].sprites.Length - 1) sfxA.Play();
                    darkAnim.Play("fade out");
                    yield return imgTime;
                }

                darkAnim.Play("fade in");
                yield return _waitForBlack;
                img.enabled = false;
                darkAnim.Play("fade out");
                playerScript.AllowMovement = true;
                ambiences.SetActive(true);
            
                //I could make this a Real Variable TM but doing this is just easier for me lmao
                var waitABit = new WaitForSeconds(0.2f);
            
                while (audSourceAudio.volume > 0)
                {
                    audSourceAudio.volume -= 0.06f;
                    yield return waitABit;
                }
                audSource.SetActive(false);

                StopCoroutine(imageIntervals(timer));
            }
        }
    }
}