using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace Scenes.Level1.Scripts
{
    public class QuitButton : MonoBehaviour
    {
        //lmao commented code city
        bool _amPaused;
        private bool _menu;
        private bool _ending;
        [SerializeField] GameObject pauseUi;
        [SerializeField] private Animator ratSpin;
        [SerializeField] private audioArray audScript;

        [SerializeField] private UnityEvent pauseEnter;
        [SerializeField] private UnityEvent pauseExit;
        private void Awake()
        {        
            Time.timeScale = 1.0f; //just in case, idk, lol

            if (SceneManager.GetActiveScene().buildIndex == 0) //if we are in the menu
            {
                _menu = true;
                _ending = false;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3) //if we are in the ending
            {
                _ending = true;
                _menu = false;
            }
        }

        private void Update() //fixedupdate is not called if timescale is 0
        {
            if (Input.GetKeyDown(KeyCode.T) && !_menu && !_ending) //change this to escape
            {
                pausing();
            }
            else if (Input.GetKeyDown(KeyCode.T) && (_menu || _ending))
            {
                Application.Quit();
            }
        }

        public void pausing() //what???????????????????????????
        {
            if (_amPaused && !_menu && !_ending)
            {
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                pauseExit.Invoke();
                _amPaused = false;
                pauseUi.SetActive(false);
                //"comparing to null is expensive" they said,
                if (ReferenceEquals(ratSpin, null)) return;
                var speed = ratSpin.speed;
                speed += (float)0.2;
                ratSpin.speed = speed;
                audScript.spinspeed = speed;
            }
            else if (!_menu && !_ending)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                pauseEnter.Invoke();
                _amPaused = true;
                pauseUi.SetActive(true);
            }
        }
        public void quitGame()
        {
            Debug.Log("quit application from pause button");
            Application.Quit();
        }
    }
}
