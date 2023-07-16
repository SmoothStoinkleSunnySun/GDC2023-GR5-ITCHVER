using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    //helo this is cutscene be nice idk
    [TextArea][SerializeField] string notes;

    [Serializable]
    struct Cutscenes
    {
        [SerializeField] Sprite[] sprites;
    }

    [SerializeField] Cutscenes[] coolCutscenes;

    [SerializeField] GameObject tempImg;
    [HideInInspector] int cutsceneToPlay { get; set; }
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
        tempImg.SetActive(true);

    }
}
