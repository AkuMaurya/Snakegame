using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    public string LevelName;
    public void Level_Selection(){
        SceneManager.LoadScene(LevelName);
    }


}
