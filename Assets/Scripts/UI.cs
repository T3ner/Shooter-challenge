using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void Leave()
    {
        Application.Quit();
    }
}
