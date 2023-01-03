
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{
    public Text scoreText;
    public int _score;
    public Snake player;

    public void UpdateScore()
    {
        _score +=    10;
        scoreText.text = "Score: " + _score;


        if(!player.Restart.activeSelf){
        _score = 0;
        scoreText.text = "Score: ";
    }
    }   

}
