using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scuffedStartSpeedrunTimer : MonoBehaviour
{
    
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Speedrunner").GetComponent<SpeedRunModeTracker>().startTimer();
    }
}
