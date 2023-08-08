using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace Scenes.Level1.Scripts.language_switch_scripts
{
    public class LanguagePicker : MonoBehaviour
    {
        [SerializeField] private UnityEvent switchToDanish;
        [SerializeField] private UnityEvent switchToEnglish;
        
        public bool IsSwitchingLang { get; set; }
        //these could be 1 bool, but idc they look prettier this way
        [HideInInspector] public bool isDanish;
        [HideInInspector] public bool isEnglish = true;
        // -1 = not switched, 0 = danish, 1 = english
        private int _lastSwitch = -1;
        public static LanguagePicker Instance
        {
            get;
            set;
        }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "Menu") return;
            GameObject.FindGameObjectWithTag("LangPick").GetComponent<LanguagePicker>()
                .transferData(isDanish, isEnglish, _lastSwitch);
            Destroy(this);
        }
        public void transferData(bool danish, bool english, int lastSwitch)
        {
            isDanish = danish;
            isEnglish = english;
            _lastSwitch = lastSwitch;
        }

        public void ApplySwitch()
        {
            isEnglish = !isEnglish;
            isDanish = !isDanish;

            if (IsSwitchingLang) return;
            
            var sceneName = SceneManager.GetActiveScene().name;
            
            if (_lastSwitch == -1 ^ _lastSwitch == 1 && (sceneName == "Level1") ^  (sceneName == "StartCutscene")&& isDanish)
            {
                switchToDanish.Invoke();
                _lastSwitch = 0;
                SwitchingStuff();

            }else if (_lastSwitch != -1 && _lastSwitch == 0 && (sceneName == "Level1") ^ (sceneName == "StartCutscene") && isEnglish)
            {
                switchToEnglish.Invoke();
                _lastSwitch = 1;
                SwitchingStuff();
            }

            void SwitchingStuff()
            {
                IsSwitchingLang = true;
                //todo: disable button
            }
        }
    }
}
