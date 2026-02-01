using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using NUnit.Framework.Internal.Execution;

public class ButtonTween : MonoBehaviour
{
    [SerializeField] private AudioClip switchSound;
    [SerializeField] private AudioClip clickSound;
    private GameObject lastSelected;
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    private void Start()
    {
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }
    private void Update()
    {
        GameObject current = EventSystem.current.currentSelectedGameObject;
        if (current != lastSelected)
        {
            lastSelected = current;
            if (lastSelected == gameObject && switchSound != null)
            {
                AudioManager.Play(switchSound);
            }
            /*if (switchSound != null)
            {
                AudioManager.Play(switchSound);
            }*/
        }
    }
    private void OnClick()
    {
        if (clickSound != null)
        {
            AudioManager.Play(clickSound);
        }
    }
}
