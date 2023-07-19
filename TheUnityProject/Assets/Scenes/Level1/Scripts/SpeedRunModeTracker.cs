using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpeedRunModeTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmptxt;
    [SerializeField] GameObject coolCanvas;
    [SerializeField] Toggle toggler;

    public bool speedrunEnabled { get; set; }
    bool counting = false;
    float runTimer;
    private void FixedUpdate()
    {
        if (speedrunEnabled && counting)
        {
            runTimer += Time.deltaTime; //this is a very long float to display
            tmptxt.text = runTimer.ToString();
        }
    }
    public void startTimer()
    {
        if (speedrunEnabled)
        {
            coolCanvas.SetActive(true);
            counting = true;
        }
    }
    public void stopTimer()
    {
        counting = false;
    }
    public static SpeedRunModeTracker Instance
    {
        get;
        set;
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    public void checkForToggle()
    {
        if (toggler.isOn)
        {
            speedrunEnabled = true;

        }
        else if (!toggler.isOn)
        {
            speedrunEnabled = false;
        }
    }
}
