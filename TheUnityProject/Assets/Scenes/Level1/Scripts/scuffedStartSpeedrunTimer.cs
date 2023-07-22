using UnityEngine;
namespace Scenes.Level1.Scripts
{
    public class scuffedStartSpeedrunTimer : MonoBehaviour
    {
        private void Awake()
        {
            GameObject.FindGameObjectWithTag("Speedrunner").GetComponent<SpeedRunModeTracker>().startTimer();
        }
    }
}