using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace Scenes.Level1.Scripts
{
    public class DeathHue : MonoBehaviour
    {
        [SerializeField] private Volume volume;
        private bool _redhueInUse;
        [SerializeField] private AudioSource pauseAmbience;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || _redhueInUse || !volume.profile.TryGet(out ColorAdjustments _)) return;
            _redhueInUse = true;
            StartCoroutine(redHueTime());
        }
        private IEnumerator redHueTime()
        {
            volume.profile.TryGet(out ColorAdjustments colorAdjustments);
            colorAdjustments.active = true;
            pauseAmbience.Pause();
            //this code doesn't work and idk why
            /*
            colorAdjustments.active = true;
            colorAdjustments.contrast.value = 0f;
            colorAdjustments.colorFilter.value = new Color(255f, 255f, 255f);   
                for (int i = 0; i < 6; i++)
                {
                    colorAdjustments.contrast.value -= 6.9f;
                    colorAdjustments.colorFilter.value = new Color(colorAdjustments.colorFilter.value.r - 10.66666667f,
                        colorAdjustments.colorFilter.value.g - 41.5f, colorAdjustments.colorFilter.value.b - 41.5f);
                    yield return new WaitForSeconds(0.1f);
                }
            
            colorAdjustments.contrast.value = -41.4f;
            colorAdjustments.colorFilter.value = new Color(191, 6, 6);
            yield return new WaitForSeconds(5.5f);
            
            colorAdjustments.contrast.value = 0f;
            colorAdjustments.colorFilter.value = new Color(255f, 255f, 255f);
            colorAdjustments.active = false;
            */
            yield return new WaitForSeconds(3);
            colorAdjustments.active = false;
            pauseAmbience.UnPause();
            _redhueInUse = false;
            StopCoroutine(redHueTime());
        }
    }
}

