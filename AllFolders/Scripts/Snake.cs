using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    private Vector2Int gridPosition;
    private Vector2Int gridMoveDirection;
    public GameObject Restart;
    private UIManger uiManager;
    int snakeLength ; 
    
    private bool Shield =false, Score2x =false, Speed = false;

    public void SetShield(bool _shield){
        Shield = _shield;
    }

    public void SetScore2x(bool _score){
        Score2x = _score; 
    }

    public void SetSpeed(bool _speed){
        _speed = Speed;
    }

    private void Awake(){
        gridPosition = new Vector2Int(-5, -5    );
        gridMoveDirection = new Vector2Int(1,0);
        segments.Clear();
        segments.Add(this.transform);
        uiManager = GameObject.Find("UI").GetComponent<UIManger>();
    }

    private void Update(){
        snakeLength = segments.Count;
        HandleInput();  
        ScreenWrapper(segments);
    }

    private void FixedUpdate(){
        
        for(int i = snakeLength - 1; i>0 ; i--){
            segments[i].position = segments[i-1].position;
        }
        Transform trans = this.transform;
        Velocity(trans);
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));
    }

    public void ScreenWrapper(List<Transform> list){
        if(list[0].position.y > 7){
                list[0].position = new Vector3(list[0].position.x,-7,list[0].position.z);                
            }
            else if(list[0].position.y < -7){
                list[0].position = new Vector3(list[0].position.x,7,list[0].position.z);   
            }

        if(list[0].position.x > 16){
                list[0].position = new Vector3(-16,list[0].position.y,list[0].position.z);                
            }
            else if(list[0].position.x < -16){
                list[0].position = new Vector3(16,list[0].position.y,list[0].position.z);   
            }
    }

    private void HandleInput(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(gridMoveDirection.y != -1){
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(gridMoveDirection.y != 1){
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(gridMoveDirection.x != 1){
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(gridMoveDirection.x != -1){
                gridMoveDirection.x = 1;
                gridMoveDirection.y = 0;
            }
        }
    }

    public float GetAngleFromVector(Vector2Int dir){
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(n<0) n += 360;
        return n;
    }

    public void Grow(){
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[snakeLength - 1].position;
        segments.Add(segment);
    }

    public void Reduce(){
        Transform tail = segments[snakeLength-1];
        GameObject tailObject = tail.gameObject;
        segments.RemoveAt(snakeLength-1);
        Destroy(tailObject);
    }

    public void Velocity(Transform objct){
        if(Speed){
            float speed = 1.0f;
            objct.position = new Vector3(
            Mathf.Round(objct.position.x) + gridMoveDirection.x + speed,
            Mathf.Round(objct.position.y) + gridMoveDirection.y + speed,
            0.0f);
            StartCoroutine(SpeedPowerUp());
        }
        else{
            objct.position = new Vector3(
            Mathf.Round(objct.position.x) + gridMoveDirection.x,
            Mathf.Round(objct.position.y) + gridMoveDirection.y,
            0.0f
            );
        }
    }

    public IEnumerator SpeedPowerUp()
    {
        yield return new WaitForSeconds(5.0f); 
        Speed = false;
    }

    public void ScoreGain(){
        
        // UIManger uiManager = GetComponent<UIManger>();
        if(Score2x){
            uiManager._score += 2; 
            Debug.Log(uiManager._score);
            StartCoroutine(ScorePowerUp());
            Score2x = false;
        }
        else{
            uiManager._score += 1; 
        }
    }
    
    public IEnumerator ScorePowerUp()
    {
        yield return new WaitForSeconds(5.0f); 
        Score2x = false;
    }
    
    public void Damage(){
        if(Shield){
            Debug.Log("Shield is active");
        }
        else{
            Restart.SetActive(true);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Food"){
            Grow();
        }
        if(other.tag == "Junk"){
            if(snakeLength > 3){
                Reduce();
            }
        }
        if(other.tag == "Obstacle"){
            if(Shield != true){
                Damage();
                this.enabled = false;
            }
            Shield = false;
            
            
        }
        
    }   
}
