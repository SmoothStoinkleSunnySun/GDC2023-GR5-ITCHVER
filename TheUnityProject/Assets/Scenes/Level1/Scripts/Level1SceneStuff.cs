using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Scenes.Level1.Scripts
{
    public class Level1SceneStuff : MonoBehaviour
    {
        [Header("Private")]
        [SerializeField]
        private CinemachineVirtualCamera vcamstart;
        [SerializeField] private float timer;

        [Header("Public")]
        public Collider playerCollider;
        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(TimerToSwitch(timer));
        }

        private IEnumerator TimerToSwitch(float timer)
        {
            yield return new WaitForSeconds(timer);
            vcamstart.Priority = 0;
            StopCoroutine(TimerToSwitch(timer));
        }
    }
}
