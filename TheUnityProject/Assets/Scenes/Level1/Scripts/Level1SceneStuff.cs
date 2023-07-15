using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1SceneStuff : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcamstart;
    [SerializeField] float timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timertoswitch(timer));
    }
    IEnumerator timertoswitch(float timer)
    {
        yield return new WaitForSeconds(timer);
        vcamstart.Priority = 0;
        StopCoroutine(timertoswitch(timer));
    }
}
