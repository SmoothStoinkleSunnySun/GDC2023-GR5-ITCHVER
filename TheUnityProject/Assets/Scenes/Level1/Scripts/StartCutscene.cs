using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCutscene: MonoBehaviour
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
    [SerializeField] bool isStartCutscene;
    public int cutsceneToPlay { get; set; }

    public void startCutscene()
    {
        StartCoroutine(imageIntervals(imageTimer));
        if (!isStartCutscene)
        {
            playerScript.AllowMovement = false;
        }        
    }
    IEnumerator imageIntervals(float timer)
    {       
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
        if (!isStartCutscene)
        {
            playerScript.AllowMovement = true;
        }

        if (isStartCutscene)
        {
            StartCoroutine(RedirectToAScene());
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
