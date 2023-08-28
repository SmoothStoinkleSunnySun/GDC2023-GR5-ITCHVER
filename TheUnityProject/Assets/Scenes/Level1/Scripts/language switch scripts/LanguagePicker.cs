using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace Scenes.Level1.Scripts.language_switch_scripts
{
    public class LanguagePicker : MonoBehaviour
    {
        public UnityEvent switchToDanish;
        public UnityEvent switchToEnglish;

        public LanguageHelper myLangHelpy;
        //for scene StartCutscene ONLY
        private SpeedRunModeTracker _speedRunModeTracker;
        private bool _speedTrackerActive;
        public bool IsSwitchingLang { get; set; }
        //these could be 1 bool, but idc they look prettier this way
        [HideInInspector] public bool isDanish;
        [HideInInspector] public bool isEnglish = true;
        // -1 = not switched, 0 = danish, 1 = english
        private int _lastSwitch = -1;
        
        private void OnEnable() //sauce https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager-sceneLoaded.html
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        public void transferData(bool danish, bool english, int lastSwitch, LanguageHelper langHelpy)
        {
            isDanish = danish;
            isEnglish = english;
            _lastSwitch = lastSwitch;
            myLangHelpy = langHelpy;
        }
        public void setSpeedRunTracker(SpeedRunModeTracker speedTracker)
        {
            _speedRunModeTracker = speedTracker;
            _speedTrackerActive = true;
        }
        // So you see, these two methods are here because SpeedRunTracker is from the Menu scene,
        // and therefore we can't tell it to do anything before we've set it
        public void skipToDanish()
        {
            if (!_speedTrackerActive) StartCoroutine(waitingForScript());
            else _speedRunModeTracker.toDanish();

            IEnumerator waitingForScript()
            {
                yield return new WaitUntil(() => _speedTrackerActive);
                _speedRunModeTracker.toDanish();
                StopCoroutine(waitingForScript());
            }
        }
        public void skipToEnglish()
        {
            if (!_speedTrackerActive) StartCoroutine(waitingForScript());
            else _speedRunModeTracker.toEnglish();

            IEnumerator waitingForScript()
            {
                yield return new WaitUntil(() => _speedTrackerActive);
                _speedRunModeTracker.toEnglish();
                StopCoroutine(waitingForScript());
            }
        }
        public void ApplySwitch()
        {
            isEnglish = !isEnglish;
            isDanish = !isDanish;

            if (IsSwitchingLang) return;
            
            if (_lastSwitch != 0 && isDanish)
            {
                IsSwitchingLang = true;
                switchToDanish.Invoke();
                _lastSwitch = 0;

            }else if (_lastSwitch != 1 && isEnglish)
            {
                IsSwitchingLang = true;
                switchToEnglish.Invoke();
                _lastSwitch = 1;
            }
        }
        void OnSceneUnloaded(Scene current)
        {
            myLangHelpy.savedLastSwitch = _lastSwitch;
            myLangHelpy.savedIsDanish = isDanish;
            myLangHelpy.savedIsEnglish = isEnglish;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}
