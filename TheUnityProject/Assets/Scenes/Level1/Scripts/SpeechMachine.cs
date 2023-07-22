using System.Collections;
using UnityEngine;
namespace Scenes.Level1.Scripts
{
    public class SpeechMachine : MonoBehaviour
    {
        [SerializeField] private float thonkTime;
        [SerializeField] private GameObject thonker;
        [SerializeField] private PlayerMove playerScript;

        public void thinkingText()
        {
            StartCoroutine(thonk(thonkTime));
        }

        private IEnumerator thonk(float timer)
        {
            thonker.SetActive(true);
            yield return new WaitForSeconds(timer);
            thonker.SetActive(false);
            StopCoroutine(thonk(timer));
            playerScript.AllowMovement = true;
        }
    }
}