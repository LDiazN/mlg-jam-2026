using System.Collections.Generic;
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


        public int playerId = -1;

        private void Start()
        {
            if (playerId != -1)
                label.text = $"P{playerId + 1}";

            // TODO merge player portraits first
            // if (playerId != -1)
            //     iconObj.image = characterSprites[playerId % (characterSprites.Count - 1) + 1];
        }


    }
}
