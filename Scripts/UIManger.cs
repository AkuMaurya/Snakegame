using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{
    public Text scoreText;
    public int _score;

    public void UpdateScore()
    {
        _score +=    10;
        scoreText.text = "Score: " + _score;
    }

    // public void ShowTitleScreen()
    // {
    //     titleScreen.SetActive(true);
    // }

    // public void HideTitleScreen()
    // {
    //     titleScreen.SetActive(false);
    //     score = 0;
    //     scoreText.text = "Score: ";
    // }

}
