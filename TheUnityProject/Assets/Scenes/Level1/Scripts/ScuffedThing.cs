using UnityEngine;
namespace Scenes.Level1.Scripts
{
    public class ScuffedThing : MonoBehaviour
    {
        [SerializeField] private StartCutscene cutsceneScript;

        private void Start()
        {
            cutsceneScript.CutsceneToPlay = 0;
            cutsceneScript.startCutscene();
        }
    }
}