using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl3_ColliderTrigger : MonoBehaviour
{
    [SerializeField] CodeManage codeScript;
    [SerializeField] MeshRenderer meshRend;
    [SerializeField] MeshRenderer meshRendLock;
    public bool ShouldICheck { get; set; } = true;

    [Header("colliders")]
    [SerializeField] Collider triggerThing;
    [SerializeField] Collider boxCollider;


    bool stuffInTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (ShouldICheck)
        {
            if (other == boxCollider)
            {
                stuffInTrigger = true;
                meshRend.materials = codeScript.GreenMat;
                meshRendLock.materials = codeScript.GreenMat;
            }
            else foreach (var Collider in codeScript.boxColliders)
                {
                    stuffInTrigger = true;
                    meshRend.materials = codeScript.RedMat;
                    meshRendLock.materials = codeScript.RedMat;
                }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (stuffInTrigger && ShouldICheck)
        {
            if (other == boxCollider)
            {
                stuffInTrigger = false;
                meshRend.materials = codeScript.OffMat;
                meshRendLock.materials = codeScript.OffMat;
            }
            else foreach (var collider in codeScript.boxColliders)
                {
                    stuffInTrigger = false;
                    meshRend.materials = codeScript.OffMat;
                    meshRendLock.materials = codeScript.OffMat;
                }
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (ShouldICheck)
        {
            if (other == boxCollider)
            {
                stuffInTrigger = true;
                meshRend.materials = codeScript.GreenMat;
                meshRendLock.materials = codeScript.GreenMat;
            }
            else foreach (var Collider in codeScript.boxColliders)
                {
                    stuffInTrigger = true;
                    meshRend.materials = codeScript.RedMat;
                    meshRendLock.materials = codeScript.RedMat;
                }
        }
    }
}
