using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class TitleAnimation : MonoBehaviour
    {
        private void Start()
        {
            transform.DOScale(1.5f * transform.localScale, 2).SetLoops(-1, LoopType.Yoyo).Play();
        }
    }
}
