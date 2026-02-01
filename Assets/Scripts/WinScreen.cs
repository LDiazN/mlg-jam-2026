using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public string MainMenu = "MainMenuDefinitive";
    public float timeToWait = 5;
    private float timePassed;

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > timeToWait)
            SceneManager.LoadScene(MainMenu);
    }
}
