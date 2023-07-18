using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CodeManage : MonoBehaviour
{
    public Collider[] boxColliders;
    public MeshRenderer[] lightMeshes;
    [SerializeField] private lvl3_ColliderTrigger[] scripto;
    public Material[] OffMat;
    public Material[] RedMat;
    public Material[] GreenMat;
    [SerializeField] Material[] LockGoldenMat;
    [SerializeField] MeshRenderer lockMesh;
    [SerializeField] GameObject interact;
    //incredibly scuffed
    [SerializeField] GameObject originalDoor;
    [SerializeField] GameObject newDoor;

    [SerializeField] UnityEvent CodePuzzleSolved;
    bool puzzleSolved = false;

    int greens;

    private void FixedUpdate()
    {
        if (!puzzleSolved)
        {
            foreach (var MeshRenderer in lightMeshes)
            {
                if (MeshRenderer.material.name == "GreenLight (Instance)")
                {
                    greens++;
                }
            }
            if (greens == 4)
            {
                CodePuzzleSolved.Invoke();
                puzzleSolved = true;
                interact.SetActive(true);
                for (int i = 0; i < scripto.Length; i++)
                {
                    scripto[i].ShouldICheck = false;
                }
            }
            else if (greens < 4)
            {
                greens = 0;
            }
        }
    }
    public void startCage()
    {
        StartCoroutine(messWithCageDoor());
    }
    public void changeLock()
    {
        lockMesh.materials = LockGoldenMat;
    }
    public IEnumerator messWithCageDoor()
    {
        yield return new WaitForSeconds(2);
        originalDoor.SetActive(false);
        newDoor.SetActive(true);
        StopCoroutine(messWithCageDoor());
    }
}
