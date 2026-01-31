using System;
using DG.Tweening;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    [RequireComponent(typeof(PlayerCard))]
    public class JoinCardUIBindings : BaseInputBindings
    {
        private PlayerCard _playerCard;
        private float _timeAlive;

        private void Awake()
        {
            _playerCard = GetComponent<PlayerCard>();

            var submit = controls.FindActionMap("UI").FindAction("Submit");

            SetHandlers(new ()
            {
                (submit, Submit)
            });
        }

        private void Submit(InputAction.CallbackContext context)
        {
            if (!IsMine(context) || _timeAlive < 1 || !context.performed)
                return;

            transform.DOShakePosition(0.25f, new Vector3(0, 10, 0)).Play();
        }

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            _timeAlive += Time.deltaTime;
        }

        private bool IsMine(InputAction.CallbackContext context) => InputManager.IsMine(context, _playerCard.playerId);
    }
}
