using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    bool amPaused;
    bool menu = false;
    bool ending = false;
    [SerializeField] GameObject pauseUi;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //change this to escape
        {
            Application.Quit();
        }
    }
}
/*
private void //Awake()
{        
    Time.timeScale = 1.0f; //just in case, idk, lol

    if (SceneManager.GetActiveScene().buildIndex == 0) //if we are in the menu
    {
        menu = true;
        ending = false;
    }
    else if (SceneManager.GetActiveScene().buildIndex == 3) //if we are in the ending
    {
        ending = true;
        menu = false;
    }
}
void //FixedUpdate()
{
    if (Input.GetKeyDown(KeyCode.T) && !menu && !ending) //change this to escape
    {
        pausing();
    }
    else if (menu || ending)
    {
        Application.Quit();
    }
}
private void pausing() //what???????????????????????????
{
    if (amPaused && !menu && !ending)
    {
        Time.timeScale = 1.0f;
    }
    else if (!menu && !ending)
    {
        Time.timeScale = 0f;
    }
}
}
*/