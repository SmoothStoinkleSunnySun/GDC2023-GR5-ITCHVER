using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerTeleport : MonoBehaviour
{
    public enum WhichColliderIsThis
    {
        A,
        B
    }

    [SerializeField] WhichColliderIsThis ThisCollider;
    [SerializeField] RoomTeleporting MyRoomTeleport;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && MyRoomTeleport.TeleportEnabled)
        {
            MyRoomTeleport.Teleport(ThisCollider);
        }
    }
}