using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMenuUIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject playerMenu;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMenu.SetActive(true);
        SelectFirstSelectable(playerMenu);
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
    public void BackMainMenu()
    {
        SceneManager.LoadScene("MainMenuDefinitive");
    }
    public void StartGame()
    {

    }
}
