using Scenes.Level1.Scripts.language_switch_scripts;
using UnityEngine;
using UnityEngine.UI;
namespace Scenes.Menu
{
    public class UISwitchButton : MonoBehaviour
    {
        [Header("need these")]
        [SerializeField] private LanguagePicker languagePicker;
        [SerializeField] private Button applyButton;
        [SerializeField] private GameObject txtEng;
        [SerializeField] private GameObject txtDan;
        
        // -1 = not switched, 0 = danish, 1 = english
        private int _lastSwitch = -1;

        private void Start()
        {
            applyButton.interactable = false;
        }
        public void Switch()
        {
            if (languagePicker.IsSwitchingLang) return;
            
            switch (_lastSwitch)
            {
                case -1:
                    SwitchToDanish();
                    applyButton.interactable = true;
                    break;
                case 0:
                {
                    SwitchToEnglish();
                    applyButton.interactable = languagePicker.isDanish;
                    break;
                }
                case 1:
                {
                    SwitchToDanish();
                    applyButton.interactable = languagePicker.isEnglish;
                    break;
                }
            }

            void SwitchToDanish()
            {
                _lastSwitch = 0;
                txtEng.SetActive(false);
                txtDan.SetActive(true);
            }
            void SwitchToEnglish()
            {
                _lastSwitch = 1;
                txtEng.SetActive(true);
                txtDan.SetActive(false);
            }
        }
    }
}
