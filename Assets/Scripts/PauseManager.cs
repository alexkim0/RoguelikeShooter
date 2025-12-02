using UnityEngine;
using UnityEngine.SceneManagement; // only if you want main menu / quit

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public GameObject pauseMenuRoot;

    public bool isPaused = false;
    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOverManager.instance.isDead) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuRoot.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuRoot.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Optional: hook this to a Quit button
    public void ExitGame()
    {
        SceneManager.LoadScene("Home");
    }
}
