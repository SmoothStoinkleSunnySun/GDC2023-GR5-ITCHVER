using System;
using UnityEngine;
namespace Scenes.Level1.Scripts
{
    public class ChangeToNewMat : MonoBehaviour
    {
        [TextArea] [SerializeField] private string notes;
        [Serializable]
        private struct MaterialsList
        {
            public Material[] matArray;
        }

        [Header("Please set these thank you")]
        [SerializeField] private MeshRenderer mesRend;
        [SerializeField] private MaterialsList[] matsToChange;

        public void changeToMat(int indexNum)
        {
            mesRend.materials = matsToChange[indexNum].matArray;
        }
    }
}