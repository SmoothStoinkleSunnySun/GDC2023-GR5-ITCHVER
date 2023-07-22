using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace Scenes.Level1.Scripts
{
    public class CodeManage : MonoBehaviour
    {
        public Collider[] boxColliders;
        public MeshRenderer[] lightMeshes;
        [SerializeField] private lvl3_ColliderTrigger[] scripto;
        public Material[] offMat;
        public Material[] redMat;
        public Material[] greenMat;
        [SerializeField] private Material[] lockGoldenMat;
        [SerializeField] private MeshRenderer lockMesh;
        [SerializeField] private GameObject interact;
        //incredibly scuffed
        [SerializeField] private GameObject originalDoor;
        [SerializeField] private GameObject newDoor;
        //audii
        [SerializeField] private AudioSource @as;
        [SerializeField] private AudioClip cageFall;
        [SerializeField] private UnityEvent codePuzzleSolved;

        private bool _puzzleSolved;
        private int _greens;

        private void FixedUpdate()
        {
            if (_puzzleSolved) return; //holy
            foreach (var meshRenderer in lightMeshes)
                if (meshRenderer.material.name == "GreenLight (Instance)")
                    _greens++;

            switch (_greens)
            {
                case 4:
                {
                    codePuzzleSolved.Invoke();
                    _puzzleSolved = true;
                    interact.SetActive(true);
                    foreach (var t in scripto) t.ShouldICheck = false;

                    break;
                }
                case < 4:
                    _greens = 0;
                    break;
            }
        }

        public void startCage()
        {
            StartCoroutine(messWithCageDoor());
        }

        public void changeLock()
        {
            lockMesh.materials = lockGoldenMat;
        }

        private IEnumerator messWithCageDoor()
        {
            yield return new WaitForSeconds(14.5f); //yes.
            @as.PlayOneShot(cageFall);
            originalDoor.SetActive(false);
            newDoor.SetActive(true);
            StopCoroutine(messWithCageDoor());
        }
    }
}