using System.Collections;
using UnityEngine;
using static Scenes.Level1.Scripts.ColliderTriggerTeleport;

namespace Scenes.Level1.Scripts
{
    public class RoomTeleporting : MonoBehaviour
    {
        [TextArea] [SerializeField] public string notes;

        [Header("Teleport Points")]
        //the two teleport points, teleport A & B
        public Transform ta;
        public Transform tb;

        [Header("stuff lol")] [SerializeField] private PlayerMove playerScript;
        [SerializeField] private Animator darkAnim;
        [SerializeField] private float timeInDark;

        //teleport cooldown to prevent teleport spam    
        public bool teleportEnabled; //if the player is allowed to use a teleport

        public void Teleport(WhichColliderIsThis colliderInQuestion)
        {
            playerScript.AllowMovement = false;

            teleportEnabled = false;

            StartCoroutine(doingTeleport(colliderInQuestion));
        }

        private IEnumerator doingTeleport(WhichColliderIsThis colliderInQuestion)
        {
            darkAnim.Play("fade in");
            yield return new WaitUntil(() => darkAnim.GetCurrentAnimatorStateInfo(0).IsName("black"));

            if (colliderInQuestion == WhichColliderIsThis.A)
                playerScript.rb.position = tb.position;
            else if (colliderInQuestion == WhichColliderIsThis.B) playerScript.rb.position = ta.position;
            yield return new WaitForSeconds(timeInDark);
            darkAnim.Play("fade out");

            teleportEnabled = true;
            playerScript.AllowMovement = true;
            StopCoroutine(doingTeleport(colliderInQuestion));
        }
    }
}