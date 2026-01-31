using UnityEngine;
using UnityEngine.UI;
using UInput = UnityEngine.Input;

public class ImageListUIManager : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteList;
    private Image imageShow;
    private int imageCount;
    private bool stickUsed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageShow = GetComponentInChildren<Image>();
        imageCount = 0;
        imageShow.sprite = spriteList[imageCount];
        Debug.Log("Longitud de sprites: " + spriteList.Length);
        stickUsed = false;
    }
    private void Update()
    {
        float horizontalInput = UInput.GetAxisRaw("Horizontal");
        if(UInput.GetKeyDown(KeyCode.D) || UInput.GetKeyDown(KeyCode.RightArrow) || (horizontalInput > 0.5f && !stickUsed))
        {
            stickUsed = true;
            NextButtonInstruction();
        }
        if(UInput.GetKeyDown(KeyCode.A) || UInput.GetKeyDown(KeyCode.LeftArrow) || (horizontalInput < -0.5f && !stickUsed))
        {
            stickUsed = true;
            PreviousButtonInstruction();
        }
        if(Mathf.Abs(horizontalInput) < 0.2f)
        {
            stickUsed = false;
        }
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
