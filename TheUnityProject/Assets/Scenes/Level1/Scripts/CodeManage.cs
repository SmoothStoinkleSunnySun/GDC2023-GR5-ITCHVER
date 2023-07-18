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
}
