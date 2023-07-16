using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashInteract : MonoBehaviour
{
    [TextArea][SerializeField] string notes;

    public UnityEvent OnInteract;

    [Header("private")]
    [SerializeField] Level1SceneStuff sceneScript;
    [SerializeField] Collider interactionCollider; //possibly needs to be public in order for virtual void ontriggerenter to work
    bool hasInteracted = false;
    public virtual void OnTriggerStay(Collider other) //virtual in case we need this for another interactable
    {
        if (!hasInteracted && other == sceneScript.playerCollider && Input.GetKey(KeyCode.E)) //keycode = wincode
        {
            hasInteracted = true;
            OnInteract.Invoke(); //Make the event happen (see inspector for what the event does)
        }
    }
}