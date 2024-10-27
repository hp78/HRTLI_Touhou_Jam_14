using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public string nextLevelName;
    int winButtonCount = 0;

    private void Awake()
    {

    }
    void Start()
    {
        if (instance)
            Destroy(instance);
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCurrentLevel();
        }
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddWinButtonCount()
    {
        winButtonCount += 1;
    }
    public void TickDownWinButton()
    {
        winButtonCount -= 1;
        if (winButtonCount <= 0)
            LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
