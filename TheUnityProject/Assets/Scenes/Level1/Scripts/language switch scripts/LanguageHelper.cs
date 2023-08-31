using UnityEngine;
using UnityEngine.SceneManagement;
namespace Scenes.Level1.Scripts.language_switch_scripts
{
    public class LanguageHelper : MonoBehaviour
    {
        public int savedLastSwitch;
        public bool savedIsEnglish;
        public bool savedIsDanish;
        public LanguagePicker myLangPick;
        
        public static LanguageHelper Instance
        {
            get;
            set;
        }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        private void OnEnable() //sauce https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager-sceneLoaded.html
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Menu") return;
            myLangPick = GameObject.FindGameObjectWithTag("LangPick").GetComponent<LanguagePicker>();

            myLangPick.transferData(savedIsDanish, savedIsEnglish, savedLastSwitch, this);
            if (!savedIsDanish) return;
            myLangPick.IsSwitchingLang = true;
            myLangPick.switchToDanish.Invoke();
        }
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
