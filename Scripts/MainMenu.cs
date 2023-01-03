using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Button buttonPlay;
    public GameObject LevelSelection;
    
    // GameOver _over;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(PlayGame);
    }

    public void PlayGame()
    {
        LevelSelection.SetActive(true);
    }
}
