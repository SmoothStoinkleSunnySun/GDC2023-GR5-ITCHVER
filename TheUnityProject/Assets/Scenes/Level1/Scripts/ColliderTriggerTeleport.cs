using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Level1.Scripts
{
    public class ColliderTriggerTeleport : MonoBehaviour
    {
        public enum WhichColliderIsThis
        {
            A,
            B
        }

        [FormerlySerializedAs("ThisCollider")] [SerializeField] WhichColliderIsThis thisCollider;
        [FormerlySerializedAs("MyRoomTeleport")] [SerializeField] RoomTeleporting myRoomTeleport;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && myRoomTeleport.teleportEnabled)
            {
                myRoomTeleport.Teleport(thisCollider);
            }
        }
    }
}