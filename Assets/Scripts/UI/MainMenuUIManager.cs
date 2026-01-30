using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionMenu;
    #endregion
    private void Start()
    {
        SelectFirstSelectable(mainMenu);
    }
    private void SelectFirstSelectable(GameObject newMenu)
    {
        EventSystem.current.SetSelectedGameObject(null);
        Selectable first = newMenu.GetComponentInChildren<Selectable>();
        if (first != null && first.gameObject.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(first.gameObject);
        }
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
    public void OpenOptionMenu()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
        SelectFirstSelectable(optionMenu);
    }
    public void CloseOptionMenu()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
        SelectFirstSelectable(mainMenu);
    }
}
