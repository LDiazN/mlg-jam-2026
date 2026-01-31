using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionForcer : MonoBehaviour
{
    #region Private Variables
    private GameObject _previousSelection;
    #endregion
    #region Unity Messages
    private void Update()
    {
        var currentSelection = EventSystem.current.currentSelectedGameObject;
        if (currentSelection != null)
        {
            _previousSelection = currentSelection;
        }
        if (currentSelection == null)
        {
            EventSystem.current.SetSelectedGameObject(_previousSelection);
        }
    }
    #endregion
}
