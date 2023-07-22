using UnityEngine;

namespace Scenes.Level1.Scripts
{
    public class lvl3_ColliderTrigger : MonoBehaviour
    {
        [SerializeField] private CodeManage codeScript;
        [SerializeField] private MeshRenderer meshRend;
        [SerializeField] private MeshRenderer meshRendLock;
        public bool ShouldICheck { get; set; } = true;

        [Header("colliders")]
        [SerializeField] private Collider boxCollider;

        bool stuffInTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (!ShouldICheck) return;
            if (other == boxCollider)
            {
                stuffInTrigger = true;
                meshRend.materials = codeScript.greenMat;
                meshRendLock.materials = codeScript.greenMat;
            }
            else foreach (var Collider in codeScript.boxColliders)
            {
                stuffInTrigger = true;
                meshRend.materials = codeScript.redMat;
                meshRendLock.materials = codeScript.redMat;
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (!stuffInTrigger || !ShouldICheck) return;
            if (other == boxCollider)
            {
                stuffInTrigger = false;
                meshRend.materials = codeScript.offMat;
                meshRendLock.materials = codeScript.offMat;
            }
            else foreach (var collider in codeScript.boxColliders)
            {
                stuffInTrigger = false;
                meshRend.materials = codeScript.offMat;
                meshRendLock.materials = codeScript.offMat;
            }


        }
        private void OnTriggerStay(Collider other)
        {
            if (!ShouldICheck) return;
            if (other == boxCollider)
            {
                stuffInTrigger = true;
                meshRend.materials = codeScript.greenMat;
                meshRendLock.materials = codeScript.greenMat;
            }
            else foreach (var Collider in codeScript.boxColliders)
            {
                stuffInTrigger = true;
                meshRend.materials = codeScript.redMat;
                meshRendLock.materials = codeScript.redMat;
            }
        }
    }
}
