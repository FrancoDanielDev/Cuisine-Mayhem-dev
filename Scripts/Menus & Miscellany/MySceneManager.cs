using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PlayLevel(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void NewGame()
    {
        //SceneManager.LoadScene("Level1");
        SceneManager.LoadScene(1);
    }

    public void TestRoom()
    {
        SceneManager.LoadScene("GameRoom");
    }

    public void BackToMainMenu()
    {
        //SceneManager.LoadScene("MainMenu");
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("You've exited the game");
    }
}
