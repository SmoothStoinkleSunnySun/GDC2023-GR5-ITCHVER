using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScuffedThing : MonoBehaviour
{
    [SerializeField] StartCutscene cutsceneScript;

    private void Start()
    {
        cutsceneScript.cutsceneToPlay = 0;
        cutsceneScript.startCutscene();
    }
}
