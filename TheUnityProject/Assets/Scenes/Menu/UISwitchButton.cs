using Scenes.Level1.Scripts.language_switch_scripts;
using UnityEngine;
using UnityEngine.EventSystems;
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
        [SerializeField] private EventSystem eventSystem;
        
        // -1 = not switched, 0 = danish, 1 = english
        private int _lastSwitch = -1;
        private void Start()
        {
            applyButton.interactable = false;
            if (!languagePicker.isDanish) return;
            _lastSwitch = 0;
            txtEng.SetActive(false);
            txtDan.SetActive(true);
        }
        public void deselectEverything()
        {
            //you can't hover a button which is selected and that makes it feel janky :C
            eventSystem.SetSelectedGameObject(null);
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
