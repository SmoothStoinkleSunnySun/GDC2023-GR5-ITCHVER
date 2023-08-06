using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Scenes.Level1.Scripts
{
    public class SpeedRunModeTracker : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmptxt;
        [SerializeField] GameObject coolCanvas;
        [SerializeField] Toggle toggler;

        private bool SpeedrunEnabled { get; set; }
        private bool _counting;
        private float _runTimer;
        private float _deltaTime;
        public static SpeedRunModeTracker Instance
        {
            get;
            set;
        }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            _deltaTime = Time.deltaTime;
        }
        private void FixedUpdate()
        {
            if (!SpeedrunEnabled || !_counting) return;
            _runTimer += _deltaTime; //this is a very long float to display
            tmptxt.text = _runTimer.ToString(CultureInfo.InvariantCulture);
        }
        public void startTimer()
        {
            if (!SpeedrunEnabled) return;
            coolCanvas.SetActive(true);
            _counting = true;
        }
        public void stopTimer()
        {
            _counting = false;
        }
        public void checkForToggle()
        {
            SpeedrunEnabled = toggler.isOn switch //lol this looks funny
            {
                true => true,
                false => false
            };
        }
    }
}
