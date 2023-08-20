using System;
using TMPro;
using UnityEngine;
namespace Scenes.Level1.Scripts.language_switch_scripts
{
    public class LS_TMP : MonoBehaviour
    {
        [Serializable]
        struct TXTchange
        {
            public TextMeshProUGUI TMPComp;
           [TextArea] public string DanishVer;
           [TextArea] public string EnglishVer;
        }

        [SerializeField] private TXTchange[] Text;

        private bool _switchToDanish;
        private bool _switchToEnglish;

        public void TXT_ToDanish()
        {
            if (_switchToDanish) _switchToDanish = false;
            for (int i = 0; i < Text.Length; i++) Text[i].TMPComp.text = Text[i].DanishVer;
            if (!enabled) _switchToDanish = true;
        }
        public void TXT_ToEnglish()
        {
            if (_switchToEnglish) _switchToEnglish = false;
            for (int i = 0; i < Text.Length; i++) Text[i].TMPComp.text = Text[i].EnglishVer;
            if (!enabled) _switchToEnglish = true;
        }
    }
}
