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
        bool _counting;
        float _runTimer;
        private void FixedUpdate()
        {
            if (!SpeedrunEnabled || !_counting) return;
            _runTimer += Time.deltaTime; //this is a very long float to display
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
        public static SpeedRunModeTracker Instance
        {
            get;
            set;
        }
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
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
