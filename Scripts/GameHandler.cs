using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    void Start()
    {
        Debug.Log("GameHandler.Start");

        GameObject sankeHeadGameObject = new GameObject();
        SpriteRenderer snakeSpriteRenderer = sankeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAccets.i.snakeHeadSprite;
    }

    void Update()
    {
        
    }
}
