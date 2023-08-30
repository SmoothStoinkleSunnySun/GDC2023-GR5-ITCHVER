using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Scenes.Level1.Scripts
{
    public class IWasCrazyOnce : MonoBehaviour
    {
        [Header("you need to set these")] [SerializeField]
        private TextMeshProUGUI thonker;
        [SerializeField] private PlayerMove playerScript;
        [SerializeField] private AudioSource sauceToPlayThonkingSounds;
        [SerializeField] private GameObject textCanvas;
        [SerializeField] private CinemachineVirtualCamera vcamCrazy;
        [Header("you can change these")] [SerializeField]
        private float thonkTime;
        [SerializeField] private float thonkLetterTime;
        [TextArea] [SerializeField] private string[] textToThinkArray;
        [SerializeField] private AudioClip[] thonkingSounds;
        private char[] _textButChar;
        private bool _isSkipping;
        private bool _finished;

        [Header("Start stuff")]
        [SerializeField] private string password;
        [SerializeField] private char[] _passwordChar;
        [SerializeField] private char _currentChar;
        private int _currentIndex;
        private bool _checkForInput = true;

        private void Start()
        {
            _passwordChar = password.ToCharArray(0, password.Length);
            _currentChar = _passwordChar[0];
            _currentIndex = 0;
        }

        private void Update()
        {
            if (!_checkForInput && _currentChar == '.')
            {
               if (!_checkForInput || !Input.GetKeyDown(KeyCode.Period)) return;
               goCrazy();
               _checkForInput = false;
               
            }
            if (!_checkForInput || !Input.GetKeyDown((KeyCode)_currentChar)) return;
            if (_currentChar != password[^1])
            {
                _currentChar = _passwordChar[_currentIndex += 1];
                
                // keycode will not recognize a question mark so now it's just skipped. >:(
                if (_currentChar == '?') _currentChar = _passwordChar[_currentIndex += 1];
            }
            else if (_currentChar == password[^1])
            {
                goCrazy();
                _checkForInput = false;
            }
        }

        private void goCrazy()
        {
            _checkForInput = false;
            textCanvas.SetActive(true);
            playerScript.AllowMovement = false;
            vcamCrazy.Priority = 100;

            StartCoroutine(Repeat());

            IEnumerator Repeat()
            {
                var waitForFinish = new WaitUntil(() => _finished);
                var thinkingTextIndex = 0;
                for (int i = 0; i < (textToThinkArray.Length * 5); i++)
                {
                    _finished = false;
                    thinkingTextIndex += 1;
                    _textButChar = textToThinkArray[thinkingTextIndex].ToCharArray(0, textToThinkArray[thinkingTextIndex].Length);
                    if (thinkingTextIndex == textToThinkArray.Length - 1) thinkingTextIndex = 0;
                    StartCoroutine(thonk(thonkTime)); 
                    yield return waitForFinish;
                    if (sauceToPlayThonkingSounds.pitch < 3) sauceToPlayThonkingSounds.pitch += 0.4f;
                    thonkTime -= 0.3f;
                }
                Debug.Log("appl quit");
                Application.Quit();
                yield return Repeat();
            }
            IEnumerator thonk(float timer)
            {
                var thonkingLetterPause = new WaitForSeconds(thonkLetterTime);
                
                foreach (var t in _textButChar)
                {
                    if (t == '<') _isSkipping = true;

                    if (_isSkipping) thonker.text += t;

                    if (t == '>')
                    {
                        _isSkipping = false;
                        continue;
                    }

                    if (_isSkipping) continue;
                    thonker.text += t;

                    //random sound from array
                    sauceToPlayThonkingSounds.PlayOneShot(thonkingSounds[Random.Range(0, thonkingSounds.Length)]);

                    yield return thonkingLetterPause;
                }

                yield return new WaitForSeconds(timer);
                thonkLetterTime -= 0.003f;
                thonker.text = "";
                _finished = true;
                StopCoroutine(thonk(timer));
            }
        }
    }
}
