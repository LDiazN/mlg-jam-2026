using UnityEngine;
using UnityEngine.UI;

public class ImageListUIManager : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteList;
    private Image imageShow;
    private int imageCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageShow = GetComponentInChildren<Image>();
        imageCount = 0;
        imageShow.sprite = spriteList[imageCount];
        Debug.Log("Longitud de sprites: " + spriteList.Length);
    }

    public void PreviousButtonInstruction()
    {
        Debug.Log("Imagen cambiada");
        imageCount = (imageCount - 1) % spriteList.Length;
        if(imageCount < 0)
        {
            imageCount = spriteList.Length - 1;
        }
        imageShow.sprite = spriteList[imageCount];
    }
    public void NextButtonInstruction()
    {
        Debug.Log("Imagen cambiada");
        imageCount = (imageCount + 1) % spriteList.Length;
        imageShow.sprite = spriteList[imageCount];
    }
}
