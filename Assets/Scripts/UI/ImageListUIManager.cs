using UnityEngine;
using UnityEngine.UI;

public class ImageListUIManager : MonoBehaviour
{
    [SerializeField] private Image[] imageList;
    private int imageCount;
    private Image imageShow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageCount = 0;
        imageShow = imageList[imageCount];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
