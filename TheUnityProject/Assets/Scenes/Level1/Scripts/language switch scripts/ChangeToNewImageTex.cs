using System;
using UnityEngine;
using UnityEngine.UI;
namespace Scenes.Level1.Scripts.language_switch_scripts
{
    public class ChangeToNewImageTex : MonoBehaviour
    {
        [TextArea] [SerializeField] private string notes;
        [Serializable]
        private struct SpriteList
        {
            public Sprite sprite;
        }

        [Header("Please set these thank you")] [SerializeField]
        private Image imageComp;
        [SerializeField] private SpriteList[] spritesToChange;

        public void changeToSprite(int indexNum)
        {
            imageComp.sprite = spritesToChange[indexNum].sprite;
        }
    }
}
