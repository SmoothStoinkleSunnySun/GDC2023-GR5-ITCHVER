using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using static ColliderTriggerTeleport;

public class RoomTeleporting : MonoBehaviour
{
    [TextArea][SerializeField] public string Notes;

    [Header("Teleport Points")]
    //the two teleport points, teleport A & B
    public Transform TA;
    public Transform TB;

    [Header("stuff lol")]
    [SerializeField] PlayerMove playerScript;
    [SerializeField] Animator dark_anim;
    [SerializeField] float timeInDark;

    //teleport cooldown to prevent teleport spam    
    public bool TeleportEnabled; //if the player is allowed to use a teleport

    public void Teleport(WhichColliderIsThis colliderInQuestion)
    {
        playerScript.AllowMovement = false;

        TeleportEnabled = false;

        StartCoroutine(doingTeleport(colliderInQuestion));
    }
    IEnumerator doingTeleport(WhichColliderIsThis colliderInQuestion)
    {
        dark_anim.Play("fade in");
        yield return new WaitUntil(() => dark_anim.GetCurrentAnimatorStateInfo(0).IsName("black"));

        if (colliderInQuestion == WhichColliderIsThis.A)
        {
            playerScript.rb.position = TB.position;
        }
        else if (colliderInQuestion == WhichColliderIsThis.B)
        {
            playerScript.rb.position = TA.position;
        }       
        yield return new WaitForSeconds(timeInDark);
        dark_anim.Play("fade out");

        TeleportEnabled = true;
        playerScript.AllowMovement = true;
        StopCoroutine(doingTeleport(colliderInQuestion));
    }

}