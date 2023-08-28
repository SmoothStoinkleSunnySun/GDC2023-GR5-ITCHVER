using System.Collections;
using System.Globalization;
using Scenes.Level1.Scripts.language_switch_scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Scenes.Level1.Scripts
{
    public class SpeedRunModeTracker : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmptxt;
        [SerializeField] private GameObject coolCanvas;
        [SerializeField] private GameObject skipCutsceneCanvas;
        [SerializeField] Toggle toggler;
        [Header("Skip ui text languages")] [SerializeField]
        private TextMeshProUGUI skipTxt;
        [TextArea] [SerializeField]
        private string english;
        [TextArea] [SerializeField] private string danish;

        private bool SpeedrunEnabled { get; set; }
        private bool _counting;
        private float _runTimer;
        private float _deltaTime;
        private LanguagePicker _myLangPick;
        public static SpeedRunModeTracker Instance
        {
            get;
            set;
        }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            _deltaTime = Time.deltaTime;
        }
        private void FixedUpdate()
        {
            if (!SpeedrunEnabled || !_counting) return;
            _runTimer += _deltaTime; //this is a very long float to display
            tmptxt.text = _runTimer.ToString(CultureInfo.InvariantCulture);
        }
        public void startTimer()
        {
            if (!SpeedrunEnabled) return;
            if (SceneManager.GetActiveScene().name == "StartCutscene") StartCoroutine(ShowSkipUI());
            
            coolCanvas.SetActive(true);
            _counting = true;

            IEnumerator ShowSkipUI()
            {
                _myLangPick = GameObject.FindGameObjectWithTag("LangPick").GetComponent<LanguagePicker>();
                _myLangPick.setSpeedRunTracker(this);
                
                skipCutsceneCanvas.SetActive(true);
                yield return new WaitForSeconds(4);
                skipCutsceneCanvas.SetActive(false);
                
                StopCoroutine(ShowSkipUI());
            }
        }
        public void toDanish()
        {
            skipTxt.text = danish;
        }
        public void toEnglish()
        {
            skipTxt.text = english;
        }
        public void stopTimer()
        {
            _counting = false;
        }
        public void checkForToggle()
        {
            SpeedrunEnabled = toggler.isOn switch //lol this looks funny
            {
                true => true,
                false => false
            };
        }
    }
}
