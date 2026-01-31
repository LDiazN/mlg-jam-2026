using UnityEngine;
using UnityEngine.UI;

public class CinematicUIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Image[] imageList;
    [SerializeField] private float imagePerSecond = 3f;
    private int imageCount;
    private float timerImage;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageCount = 0;
        timerImage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timerImage += Time.deltaTime;
        if(timerImage >= imagePerSecond)
        {

        }
    }
}
