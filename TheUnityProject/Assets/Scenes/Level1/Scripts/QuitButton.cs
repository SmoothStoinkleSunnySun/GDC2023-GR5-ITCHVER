using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    bool amPaused;
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void pausing()
    {
        if (amPaused)
        {
            Time.timeScale = 1.0f;
        }else 
        {

        }
    }
}
