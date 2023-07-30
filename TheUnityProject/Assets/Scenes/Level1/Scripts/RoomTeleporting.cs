using System;
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
        [Header("TeleportExitDirection")] 
        [SerializeField] private ExitD AexitDirection;
        [SerializeField] private ExitD BexitDirection;
        private enum ExitD
        {
            None,
            LeftL,
            Left,
            LeftR,
            Forward,
            RightR,
            Right,
            RightL,
            Backwards
        }

        [Header("stuff lol")] [SerializeField] private PlayerMove playerScript;
        [SerializeField] private Animator darkAnim;
        [SerializeField] private float timeInDark;

        //teleport cooldown to prevent teleport spam    
        public bool teleportEnabled; //if the player is allowed to use a teleport
        private static readonly int AnimMoveX = Animator.StringToHash("AnimMoveX");
        private static readonly int AnimMoveY = Animator.StringToHash("AnimMoveY");

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
            
            //Entering from point A to exitdirection point B
            if (AexitDirection != ExitD.None && colliderInQuestion == WhichColliderIsThis.A) CheckDirection(BexitDirection);
            if (BexitDirection != ExitD.None && colliderInQuestion == WhichColliderIsThis.B) CheckDirection(AexitDirection);
            StopCoroutine(doingTeleport(colliderInQuestion));
        }

        private void CheckDirection(ExitD pointDirection)
        {
            float xVal = 0;
            float yVal = 0;
            
            switch (pointDirection)
            {
                case ExitD.LeftL:
                    xVal = -1;
                    yVal = -1;
                    break;
                case ExitD.Left:
                    xVal = -1;
                    yVal = 0;
                    break;
                case ExitD.LeftR:
                    xVal = -1;
                    yVal = 1;
                    break;
                case ExitD.Forward:
                    xVal = 0;
                    yVal = 1;
                    break;
                case ExitD.RightR:
                    xVal = 1;
                    yVal = 1;
                    break;
                case ExitD.Right:
                    xVal = 1;
                    yVal = 0;
                    break;
                case ExitD.RightL:
                    xVal = 1;
                    yVal = -1;
                    break;
                case ExitD.Backwards:
                    xVal = 0;
                    yVal = -1;
                    break;
                case ExitD.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // "String based property lookup is inefficient" ok
            playerScript.anim.SetFloat(AnimMoveX, xVal);
            playerScript.anim.SetFloat(AnimMoveY, yVal);
        }
    }
}