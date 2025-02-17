using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Level1.Scripts
{
    public class Level1SceneStuff : MonoBehaviour
    {
        [Header("Private")]
        [SerializeField] private CinemachineVirtualCamera vcamstart;
        [SerializeField] private float timer;
        [SerializeField] private Animator playerAnim;
        [SerializeField] private AudioSource playerSFXSauce;
        [SerializeField] private AudioClip fallsound;
        [SerializeField] private PlayerMove playerscript;

        [Header("Public")]
        public Collider playerCollider;
        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(TimerToSwitch(timer));
            StartCoroutine(IdleTailRandom());
            StartCoroutine(FallingScuffed());
            
            IEnumerator TimerToSwitch(float timer)
            {
                yield return new WaitForSeconds(timer);
                vcamstart.Priority = 0;
                StopCoroutine(TimerToSwitch(timer));
            }
            IEnumerator IdleTailRandom() //yes
            {
                var randomTime1 = new WaitForSeconds(Random.Range(4, 64));
                var randomTime2 = new WaitForSeconds(Random.Range(10, 64));
                var randomTime3 = new WaitForSeconds(Random.Range(4, 64));

                yield return new WaitForSeconds(10); //wait for falling to be done before doing idle tails
                
                while (true)
                {
                    playerAnim.Play("Idle Tail", 2);
                    yield return randomTime1;
                    playerAnim.Play("Idle Tail",2);
                    yield return randomTime2;
                    playerAnim.Play("Idle Tail", 2);
                    yield return randomTime3;
                }
            }
            IEnumerator FallingScuffed()
            {
                playerscript.AllowMovement = false;
                playerAnim.Play("Falling", 4);
                yield return new WaitForSeconds(1.59f);
                playerAnim.Play("Falling_Impact", 4);
                playerSFXSauce.PlayOneShot(fallsound);
                yield return new WaitForSeconds(2f);
                playerscript.AllowMovement = true;
                StopCoroutine(FallingScuffed());
            }
        }
    }
}
