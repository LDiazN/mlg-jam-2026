using System;
using Control;
using QFSW.QC;
using UnityEngine;

namespace MPlayer
{
    public class Dragon : MonoBehaviour
    {
        #region Inspector Properties

        [Min(0)]
        [SerializeField] private int MaxCharge = 100;

        #endregion

        #region Internal State

        private enum State
        {
            Normal,
            Rage
        }


        private State _currentState = State.Normal;

        private int _currentCharge;

        #endregion

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Dragon item triggered");
        }

        private void TakeItem(DragonItem item)
        {
            _currentCharge = Mathf.Min(_currentCharge + item.energy, MaxCharge);
            Destroy(item.gameObject);
            var shouldRage = _currentCharge == MaxCharge;
            if (shouldRage)
                _currentState = State.Rage;

            EventsChannel.DragonChargeChanged((float)_currentCharge / MaxCharge, shouldRage);
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
