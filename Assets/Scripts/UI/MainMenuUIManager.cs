using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject glossaryMenu;
    [SerializeField] private GameObject instructionsMenu;
    [SerializeField] private GameObject creditsMenu;
    #endregion
    private void Start()
    {
        optionMenu.SetActive(false);
        creditsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        glossaryMenu.SetActive(false);
        mainMenu.SetActive(true);
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
        creditsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        glossaryMenu.SetActive(false);
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
        SelectFirstSelectable(optionMenu);

    }
    public void CloseOptionMenu()
    {
        creditsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        glossaryMenu.SetActive(false);
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
        SelectFirstSelectable(mainMenu);
    }
    public void OpenCreditsMenu()
    {
        instructionsMenu.SetActive(false);
        optionMenu.SetActive(false);
        mainMenu.SetActive(false);
        glossaryMenu.SetActive(false);
        creditsMenu.SetActive(true);
        SelectFirstSelectable(creditsMenu);
    }
    public void CloseCreditsMenu()
    {
        instructionsMenu.SetActive(false);
        glossaryMenu.SetActive(false);
        optionMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
        SelectFirstSelectable(mainMenu);
    }
    public void OpenGlossaryMenu()
    {
        instructionsMenu.SetActive(false);
        optionMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(false);
        glossaryMenu.SetActive(true);
        SelectFirstSelectable(glossaryMenu);
    }

    public void CloseGlossaryMenu()
    {
        instructionsMenu.SetActive(false);
        optionMenu.SetActive(false);
        creditsMenu.SetActive(false);
        glossaryMenu.SetActive(false);
        mainMenu.SetActive(true);
        SelectFirstSelectable(mainMenu);
    }

    public void OpenInstructionsMenu()
    {
        optionMenu.SetActive(false);
        creditsMenu.SetActive(false);
        glossaryMenu.SetActive(false);
        mainMenu.SetActive(false);
        instructionsMenu.SetActive(true);
        SelectFirstSelectable(instructionsMenu);
    }

    public void CloseInstructionsMenu()
    {
        optionMenu.SetActive(false);
        creditsMenu.SetActive(false);
        glossaryMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        SelectFirstSelectable(mainMenu);
    }
    public void OpenPlayerMenu()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}
