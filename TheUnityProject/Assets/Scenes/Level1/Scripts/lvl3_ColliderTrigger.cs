using UnityEngine;

namespace Scenes.Level1.Scripts
{
    public class lvl3_ColliderTrigger : MonoBehaviour
    {
        [SerializeField] private CodeManage codeScript;
        [SerializeField] private MeshRenderer meshRend;
        [SerializeField] private MeshRenderer meshRendLock;
        [SerializeField] private Light myLight;
        [SerializeField] private Light myLight2;
        public bool ShouldICheck { get; set; } = true;

        [Header("colliders")]
        [SerializeField] private Collider boxCollider;

        private bool _stuffInTrigger;
        //controlling light intensity & color
        //0 = green, 1 = red, 2 = off
        private bool[] _lightModes = new bool[3];
        //to prevent unnecessary setting of the light colors
        private Color _lastColor;
        //other light stuff
        private const float ColorChange = 1f;
        private float _r;
        private float _g;
        private void OnTriggerEnter(Collider other)
        {
            if (!ShouldICheck) return;
            if (other == boxCollider)
            {
                _stuffInTrigger = true;
                resetBools(0);
                meshRend.materials = codeScript.greenMat;
                meshRendLock.materials = codeScript.greenMat;
            }
            else foreach (var collider in codeScript.boxColliders)
            {
                _stuffInTrigger = true;
                resetBools(1);
                meshRend.materials = codeScript.redMat;
                meshRendLock.materials = codeScript.redMat;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (!_stuffInTrigger || !ShouldICheck) return;
            if (other == boxCollider)
            {
                _stuffInTrigger = false;
                resetBools(2);
                meshRend.materials = codeScript.offMat;
                meshRendLock.materials = codeScript.offMat;
            }
            else foreach (var collider in codeScript.boxColliders)
            {
                _stuffInTrigger = false;
                resetBools(2);
                meshRend.materials = codeScript.offMat;
                meshRendLock.materials = codeScript.offMat;
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (!ShouldICheck) return;
            if (other == boxCollider)
            {
                _stuffInTrigger = true;
                resetBools(0);
                meshRend.materials = codeScript.greenMat;
                meshRendLock.materials = codeScript.greenMat;
            }
            else foreach (var collider in codeScript.boxColliders)
            {
                _stuffInTrigger = true;
                resetBools(1);
                meshRend.materials = codeScript.redMat;
                meshRendLock.materials = codeScript.redMat;
            }
        }

        private void resetBools(int trueBool)
        {
            for (int i = 0; i < _lightModes.Length; i++)
            {
                if (i != trueBool) _lightModes[i] = false;
                else _lightModes[i] = true;
            }
        }

        private void FixedUpdate()
        {
            if (!ShouldICheck) return;
            
            if (!_lightModes[2])
            {
                myLight.intensity = 0.001f;
                myLight2.intensity = 0.001f;
            }
            var myLightColor = myLight.color;
            
            // NO
            // """"""Equality comparison of floating point numbers. Possible loss of precision while rounding values"""""""
            if (_lightModes[0])
            {
                if (myLightColor.g < 112 && myLightColor.g != 112) _g = myLightColor.g += ColorChange;
                else _g = 112;

                if (myLightColor.r > 18 && myLightColor.r != 18) _r = myLightColor.r -= ColorChange;
                else _r = 18;

                SetColor(_r,_g);
            }
            else if (_lightModes[1])
            {
                if (myLightColor.g > 18 && myLightColor.g != 18) _g = myLightColor.g -= ColorChange;
                else _g = 18;

                if (myLightColor.r < 112 && myLightColor.r != 112) _r = myLightColor.r += ColorChange;
                else _r = 112;

                SetColor(_r,_g);
            }
            else if (_lightModes[2])
            {
                myLight.intensity = 0;
                myLight2.intensity = 0;
            }

            void SetColor(float rCal, float gCal)
            {
                var calculatedColor = new Color(rCal, gCal, myLightColor.b);
                if (calculatedColor == _lastColor) return;
                myLight.color = calculatedColor;
                myLight2.color = calculatedColor;
                _lastColor = calculatedColor;
            }
        }
    }
}
