using System.Collections;
using TMPro;
using UnityEngine;
namespace Scenes.Level1.Scripts
{
    public class SpeechMachine : MonoBehaviour
    {
        [TextArea] [SerializeField] private string notes;

        [Header("you need to set these")] [SerializeField]
        private TextMeshProUGUI thonker;
        [SerializeField] private PlayerMove playerScript;
        [SerializeField] private AudioSource sauceToPlayThonkingSounds;
        [SerializeField] private GameObject textCanvas;
        [Header("you can change these")] [SerializeField]
        private float thonkTime;
        [SerializeField] private float thonkLetterTime;
        [TextArea][SerializeField] private string textToThink;
        [SerializeField] private AudioClip[] thonkingSounds;
        private char[] _textButChar;
        private bool _isSkipping;

        public void thinkingText()
        {
            textCanvas.SetActive(true);
            _textButChar = textToThink.ToCharArray(0, textToThink.Length);
            StartCoroutine(thonk(thonkTime));

            IEnumerator thonk(float timer)
            {
                var thonkingLetterPause = new WaitForSeconds(thonkLetterTime);
                
                foreach (var t in _textButChar)
                {
                    if (t == '<') _isSkipping = true;

                    if (_isSkipping) thonker.text += t;

                    if (t == '>') _isSkipping = false;

                    if (_isSkipping) continue;
                    thonker.text += t;

                    //random sound from array
                    sauceToPlayThonkingSounds.PlayOneShot(thonkingSounds[Random.Range(0, thonkingSounds.Length)]);

                    yield return thonkingLetterPause;
                }

                yield return new WaitForSeconds(timer);
                thonker.text = "";
                textCanvas.SetActive(false);

                playerScript.AllowMovement = true;
                StopCoroutine(thonk(timer));

            }
        }
    }
}