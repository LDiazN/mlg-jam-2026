using System.Collections.Generic;
using MPlayer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        [SerializeField] private List<Texture2D> characterSprites;
        [SerializeField] private Image iconObj;
        [SerializeField] private Masks masks;

        public int playerId = -1;
        private int maskIndex = -1;

        public int MaskIndex
        {
            get => maskIndex;
            set => SetMaskIndex(value);
        }

        private void Start()
        {
            if (playerId != -1)
                label.text = $"P{playerId + 1}";
        }

        private void SetMaskIndex(int index)
        {
            iconObj.sprite = masks.masks[index];
            maskIndex = index;
        }

    }
}
