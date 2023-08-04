using System;
using UnityEngine;
using UnityEngine.Events;
namespace Scenes.Level1.Scripts
{
    public class TrashInteract : MonoBehaviour
    {
        [TextArea] [SerializeField] private string notes;

        public UnityEvent onInteract;

        [Header("private")] [SerializeField] private Level1SceneStuff sceneScript;
        [SerializeField] private Collider interactionCollider;
        private bool _hasInteracted;
        public void OnTriggerStay(Collider other)
        {
            if (_hasInteracted || other != sceneScript.playerCollider || !Input.GetKey(KeyCode.E))
                return; //keycode = wincode
            _hasInteracted = true;
            onInteract.Invoke(); //Make the event happen (see inspector for what the event does)
        }
    }
}