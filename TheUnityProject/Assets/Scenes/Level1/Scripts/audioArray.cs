using UnityEngine;
namespace Scenes.Level1.Scripts
{
    public class audioArray : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [HideInInspector] public float spinspeed;

        [SerializeField] private AudioClip[] musics;

        public void updateMusic()
        {
            //today I discovered that this exists https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression
            audioSource.clip = spinspeed switch
            {
                > (float)4.6 => musics[4],
                > (float)2.5 => musics[3],
                > (float)1.8 => musics[2],
                > 1 => musics[1],
                _ => audioSource.clip
            };
        }
    }
}
