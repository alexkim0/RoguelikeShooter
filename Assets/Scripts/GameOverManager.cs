using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;
    [Header("References")]
    public GameObject player;
    public GameObject gameOverUIRoot;
    public bool isDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        isDead = true;
        player.SetActive(false);
        gameOverUIRoot.SetActive(true);

        Camera cam = Camera.main;

        cam.GetComponentInChildren<Animator>().gameObject.SetActive(false);
        cam.GetComponent<PlayerCamera>().enabled = false;

        // get current rotation
        Vector3 euler = cam.transform.eulerAngles;

        // Minecraft-style tilt: roll 90 degrees on Z
        Vector3 target = new Vector3(euler.x, euler.y, 90f);

        cam.transform.DORotate(target, 0.6f, RotateMode.Fast);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Desert Map");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Home");
    }

}
