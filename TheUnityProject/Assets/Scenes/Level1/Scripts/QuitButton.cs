using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Cursor = UnityEngine.Cursor;
namespace Scenes.Level1.Scripts
{
    public class QuitButton : MonoBehaviour
    {
        
        //lmao commented code city
        bool _amPaused;
        private bool _menu;
        private bool _ending;
        [SerializeField] private GameObject[] pauseObjects;
        [SerializeField] private Animator ratSpin;
        [SerializeField] private audioArray audScript;

        [SerializeField] private UnityEvent pauseEnter;
        [SerializeField] private UnityEvent pauseExit;

        private bool IsNull<T>(T myObject)
        {
            if (myObject is Object obj)
            {
                if (!obj) return false;
            }
            else
            {
                if (myObject == null) return false;
            }
            return true;
        }
        private void Awake()
        {        
            Time.timeScale = 1.0f; //just in case, idk, lol

            var buildIndex = SceneManager.GetActiveScene().buildIndex;
            
            switch (buildIndex)
            {
                //if we are in the menu
                case 0:
                    _menu = true;
                    _ending = false;
                    break;
                //if we are in the ending
                case 3:
                    _ending = true;
                    _menu = false;
                    break;
            }
        }

        private void Update() //fixedupdate is not called if timescale is 0
        {
            var inputGetKeyDown = Input.GetKeyDown(KeyCode.T); //change this to escape
            
            switch (inputGetKeyDown)
            {
                case true when !_menu && !_ending:
                    pausing();
                    break;
                case true when (_menu || _ending):
                    Application.Quit();
                    break;
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
                foreach (var t in pauseObjects) t.SetActive(false);
                if (IsNull(ratSpin)) return; //"comparing to null is expensive" they said,
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
                foreach (var t in pauseObjects) t.SetActive(true);
            }
        }
        public void quitGame()
        {
            Debug.Log("quit application from pause button");
            Application.Quit();
        }
    }
}
    
    

