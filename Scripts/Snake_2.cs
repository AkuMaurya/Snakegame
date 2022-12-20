using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_2 : MonoBehaviour
{
    public List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    private Vector2Int gridPosition;
    private Vector2Int gridMoveDirection;
    public GameObject Restart;
    public int Score = 0;
    public bool Shield =false, Score2x =false, Speed = false;

    private void Awake(){
        gridPosition = new Vector2Int(-5, -5    );

        gridMoveDirection = new Vector2Int(1,0);
        segments.Clear();
        segments.Add(this.transform);
    }

    private void Update(){
        HandleInput();  
        ScreenWrapper();
    }

    private void FixedUpdate(){
        
        for(int i = segments.Count - 1; i>0 ; i--){
            segments[i].position = segments[i-1].position;
        }

        Velocity();
        
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));
    }

    private void ScreenWrapper(){
        if(segments[0].position.y > 7){
                segments[0].position = new Vector3(segments[0].position.x,-7,segments[0].position.z);                
            }
            else if(segments[0].position.y < -7){
                segments[0].position = new Vector3(segments[0].position.x,7,segments[0].position.z);   
            }

        if(segments[0].position.x > 16){
                segments[0].position = new Vector3(-16,segments[0].position.y,segments[0].position.z);                
            }
            else if(segments[0].position.x < -16){
                segments[0].position = new Vector3(16,segments[0].position.y,segments[0].position.z);   
            }
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

    private void Reduce(){
        // Transform seg = segments.RemoveAt(segments.Count-1);
        Debug.Log("len" + segments.Count);
        Destroy(segments[segments.Count-1].gameObject);
    }

    public void Velocity(){
        if(Speed){
            float speed = 1.0f;
            this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + gridMoveDirection.x + speed,
            Mathf.Round(this.transform.position.y) + gridMoveDirection.y + speed,
            0.0f);
            Speed = false;
        }
        else{
            this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + gridMoveDirection.x,
            Mathf.Round(this.transform.position.y) + gridMoveDirection.y,
            0.0f
            );
        }
    }

    public void ScoreGain(){
        UIManger uiManager = GetComponent<UIManger>();
        if(Score2x){
            uiManager._score += 2; 
            Score2x = false;
        }
        else{
            uiManager._score += 1; 
        }
    }

    public void Damage(){
        if(Shield){
            Debug.Log("Shield is active");
            Shield = false;
        }
        else{
            Restart.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Food"){
            Grow();
            Score += 1; 
            Debug.Log("Food" + Score);
        }
        if(other.tag == "Junk"){
            if(Score > 2)
            Reduce();
            Score -= 1;
            Debug.Log("Junk" + Score);
        }
        if(other.tag == "Obstacle"){
            Restart.SetActive(true);
            this.enabled = false;
        }
        
    }   
}
