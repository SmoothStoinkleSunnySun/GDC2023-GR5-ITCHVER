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

        public void TXT_ToDanish()
        {
            for (int i = 0; i < Text.Length; i++) Text[i].TMPComp.text = Text[i].DanishVer;
        }
        public void TXT_ToEnglish()
        {
            for (int i = 0; i < Text.Length; i++) Text[i].TMPComp.text = Text[i].EnglishVer;
        }
    }
}
