using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Scenes.Level1.Scripts.language_switch_scripts
{
    public class ChangeToNewDecalMat : MonoBehaviour
    {
        [TextArea] [SerializeField] private string notes;
        [Serializable]
        private struct MaterialsList
        {
            public Material decalMat;
        }

        [Header("Please set these thank you")] [SerializeField]
        private DecalProjector decalProjector;
        [SerializeField] private MaterialsList[] matsToChange;

        public void changeToMat(int indexNum)
        {
            decalProjector.material = matsToChange[indexNum].decalMat;
        }
    }
}
