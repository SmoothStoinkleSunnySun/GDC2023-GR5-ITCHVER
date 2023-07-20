using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] Animator dark_anim;
    [SerializeField] Image img;
    [SerializeField] PlayerMove playerScript;
    [SerializeField] GameObject ambiences; //lmao
    [SerializeField] AudioSource sfxA;
    [SerializeField] AudioClip endFlash;
    [SerializeField] GameObject audSource;
    public int cutsceneToPlay { get; set; }

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
        for (int i = 0; i < coolCutscenes[cutsceneToPlay].sprites.Length; i++)
        {
            dark_anim.Play("fade in");
            yield return new WaitUntil(() => dark_anim.GetCurrentAnimatorStateInfo(0).IsName("black"));

            if (!img.enabled)
            {
                img.enabled = true;
            }

            img.sprite = coolCutscenes[cutsceneToPlay].sprites[i];
            dark_anim.Play("fade out");
            yield return new WaitForSeconds(timer);
        }
        dark_anim.Play("fade in");
        yield return new WaitUntil(() => dark_anim.GetCurrentAnimatorStateInfo(0).IsName("black"));
        img.enabled = false;
        dark_anim.Play("fade out");
        playerScript.AllowMovement = true;
        sfxA.PlayOneShot(endFlash);
        ambiences.SetActive(true);
        audSource.SetActive(false);

        StopCoroutine(imageIntervals(timer));
    }
}
