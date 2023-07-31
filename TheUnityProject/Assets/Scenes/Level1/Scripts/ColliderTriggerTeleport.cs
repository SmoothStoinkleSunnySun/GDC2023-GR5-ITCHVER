using UnityEngine;

namespace Scenes.Level1.Scripts
{
    public class ColliderTriggerTeleport : MonoBehaviour
    {
        public enum WhichColliderIsThis
        {
            A,
            B
        }

        [SerializeField] WhichColliderIsThis thisCollider;
        [SerializeField] RoomTeleporting myRoomTeleport;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && myRoomTeleport.teleportEnabled)
            {
                myRoomTeleport.Teleport(thisCollider);
            }
        }
    }
}