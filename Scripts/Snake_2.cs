using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_2 : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    private Vector2Int gridPosition;
    private Vector2Int gridMoveDirection;
    // private float gridMoveTimer;
    // private float gridMoveTimerMax;

    private void Awake(){
        gridPosition = new Vector2Int(-5, -5    );
        // gridMoveTimerMax = 0.5f;
        // gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1,0);
        segments.Clear();
        segments.Add(this.transform);

    }

    private void Update(){
        HandleInput();
        // HandleGridMovement();    

        
    }

    private void FixedUpdate(){
        for(int i = segments.Count - 1; i>0 ; i--){
            segments[i].position = segments[i-1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + gridMoveDirection.x,
            Mathf.Round(this.transform.position.y) + gridMoveDirection.y,
            0.0f
        );
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));
    }
    

    private void HandleInput(){
        if(Input.GetKeyDown(KeyCode.W)){
            if(gridMoveDirection.y != -1){
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.S)){
            if(gridMoveDirection.y != 1){
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        }
        if(Input.GetKeyDown(KeyCode.A)){
            if(gridMoveDirection.x != 1){
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.D)){
            if(gridMoveDirection.x != -1){
                gridMoveDirection.x = 1;
                gridMoveDirection.y = 0;
            }
        }
    }

    private float GetAngleFromVector(Vector2Int dir){
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(n<0) n += 360;
        return n;
    }

    private void Grow(){
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Food"){
            Grow();
        }
    }
}
