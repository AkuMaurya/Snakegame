using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        UIManger uiManager = GetComponent<UIManger>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Button Clicked");
        uiManager._score = 0;
        uiManager.scoreText.text = "Score: ";
    }

}
