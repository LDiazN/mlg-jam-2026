using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CinematicUIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Image[] imageList;
    [SerializeField] private float imagePerSecond = 3f;
    private int imageCount;
    private float timerImage;
    private bool _canTween;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageCount = 0;
        timerImage = 0f;
        _canTween = false;
    }

    // Update is called once per frame
    void Update()
    {
        timerImage += Time.deltaTime;
        if(timerImage >= imagePerSecond)
        {
            while (imageCount < imageList.Length && !_canTween)
            {
                _canTween = true;
                var rect = imageList[imageCount].rectTransform;
                rect.DOMoveX(rect.position.x - 1930, 1).Play();
                timerImage = 0f;
                imageCount++;
            }
            _canTween = false;
        }
    }
}
