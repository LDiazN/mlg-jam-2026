using System;
using UnityEngine;

namespace Control
{
    /// Usen Esta clase para anotar todos los eventos importantes del juego
    /// Perder salud, ganar puntos, cosas asi. Vean el ejemplo.
    ///
    /// Para usarlo en otro objetos haces:
    ///
    /// private void OnEnable()
    /// {
    ///     var channel = EventsChannel.Get();
    ///     if (channel)
    ///        channel.OnExampleEvent += MyFunction;
    /// }
    public class EventsChannel : MonoBehaviour
    {
        #region Internal State

        private static EventsChannel _instance;

        #endregion

        #region Events

        public Action OnExampleEvent;

        #endregion

        private void Awake()
        {
            if (!_instance)
                _instance = this;

            if (_instance != this)
                Destroy(gameObject);
        }

        public static EventsChannel Get() => _instance;

        public static void ExampleEvent() => _instance?.OnExampleEvent.Invoke();
    }
}
