using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAccets : MonoBehaviour
{
    public static GameAccets i;

    private void Awake(){
        i = this;
    }

    public Sprite snakeHeadSprite;
}
