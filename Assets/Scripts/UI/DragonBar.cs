using System;
using Control;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DragonBar : MonoBehaviour
    {
        #region Inspector Properties

        [SerializeField] private Slider slider;
        [SerializeField] private Color rageColor = Color.magenta;
        [SerializeField] private Image bar;

        #endregion

        #region Internal State

        [SerializeField] private Color _originalColor;

        #endregion

        private void Start()
        {
            _originalColor = bar.color;
        }

        private void OnEnable()
        {
            var channel = EventsChannel.Get();
            if (!channel)
                return;

            channel.OnDragonChargeChanged += OnDragonChargeChanged;
            channel.OnDragonRageStarted += OnDragonRageStarted;
            channel.OnDragonRageFinished += OnDragonRageFinished;
        }

        private void OnDisable()
        {
            var channel = EventsChannel.Get();
            if (!channel)
                return;

            channel.OnDragonChargeChanged -= OnDragonChargeChanged;
            channel.OnDragonRageStarted -= OnDragonRageStarted;
            channel.OnDragonRageFinished -= OnDragonRageFinished;
        }

        private void OnDragonChargeChanged(float percent)
        {
            slider.value = percent;
        }

        private void OnDragonRageFinished()
        {
            bar.color = _originalColor;
        }

        private void OnDragonRageStarted()
        {
            bar.color = rageColor;
        }
    }
}
