using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class TitleAnimation : MonoBehaviour
    {
        private Tween animation;
        private void Start()
        {
            animation = transform.DOScale(1.5f * transform.localScale, 2).SetLoops(-1, LoopType.Yoyo).Play();
        }

        private void OnDestroy()
        {
            animation?.Kill();
        }
    }
}
