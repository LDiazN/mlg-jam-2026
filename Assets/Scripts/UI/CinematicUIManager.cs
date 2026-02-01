using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
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

        // Bring the first image to the top
        if (imageList.Length > 0)
        {
            imageList[imageCount].rectTransform.SetSiblingIndex(imageList.Length - 1);
        }
    }


    // Update is called once per frame
    void Update()
    {
        timerImage += Time.deltaTime;
        if(timerImage >= imagePerSecond && !_canTween)
        {
            timerImage = 0f;
            PlayNextImage();
        }
    }
    private void PlayNextImage()
    {
        _canTween = true;
        var rect = imageList[imageCount].rectTransform;

        // Bring the current image to the top

        rect.DOMoveX(rect.position.x - 1930, 1)
            .OnComplete(() =>
            {
                _canTween = false;
                imageCount++;
                if (imageCount >= imageList.Length)
                {
                    SceneManager.LoadScene("MainMenuDefinitive");
                }
            }).Play();
    }

}