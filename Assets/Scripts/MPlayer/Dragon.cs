using System;
using Control;
using QFSW.QC;
using TMPro;
using UnityEngine;

namespace MPlayer
{
    public class Dragon : MonoBehaviour
    {
        #region Inspector Properties

        [Min(0)]
        [SerializeField] private int MaxCharge = 100;

        [Tooltip("How many units of energy to lose per second once in rage mode")]
        [Min(0)]
        [SerializeField] private int unitsPerSecond = 5;

        [SerializeField] private GameObject rageTrail;
        [SerializeField] private GameObject normalTrail;

        #endregion

        #region Internal State

        private enum State
        {
            Normal,
            Rage
        }


        private State _currentState = State.Normal;

        private int _currentCharge;

        private SpriteRenderer _spriteRenderer;

        private float timeSinceLastDip;

        #endregion

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        private void Update()
        {
            if (_currentState == State.Normal)
                return;

            // In rage
            timeSinceLastDip += Time.deltaTime;
            if (timeSinceLastDip >= 1)
            {
                timeSinceLastDip = 0;
                _currentCharge = Mathf.Max(0, _currentCharge - unitsPerSecond);
                EventsChannel.DragonChargeChanged((float)_currentCharge / MaxCharge);
                if (_currentCharge == 0)
                    FinishRage();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var dragonItem = other.gameObject.GetComponent<DragonItem>();
            if (!dragonItem)
                return;
            TakeItem(dragonItem);
        }

        private void TakeItem(DragonItem item)
        {
            _currentCharge = Mathf.Min(_currentCharge + item.energy, MaxCharge);
            Destroy(item.gameObject);
            var shouldRage = _currentCharge == MaxCharge;
            if (shouldRage)
            {
                StartRage();
            }

            EventsChannel.DragonChargeChanged((float)_currentCharge / MaxCharge);
        }

        private void StartRage()
        {
            _currentState = State.Rage;
            EventsChannel.DragonRageStarted();
            rageTrail.gameObject.SetActive(true);
            normalTrail.gameObject.SetActive(false);
        }

        private void FinishRage()
        {
            _currentState = State.Normal;
            EventsChannel.DragonRageFinished();
            rageTrail.gameObject.SetActive(false);
            normalTrail.gameObject.SetActive(true);
        }

        #region Commands

        [Command("dg.take-item", "take an item from the map and consume it")]
        private void CTakeItem()
        {
            var item = FindAnyObjectByType<DragonItem>();
            if (!item)
                return;

            TakeItem(item);
        }

        #endregion
    }
}
