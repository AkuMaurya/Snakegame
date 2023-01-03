using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayer : MonoBehaviour
{
    public List<Transform> segments = new List<Transform>();
    public List<Transform> segmentsForP2 = new List<Transform>();
    public Transform segmentPrefab;
    public GameObject Player2;
    private Vector2Int gridPosition;
    private Vector2Int gridMoveDirection;
    private Vector2Int gridMoveDirection2;
    public GameObject Restart;
    private UIManger uiManager;
    int snakeLength ;
    public int snake2Len ; 
    
    
    private bool Shield =false, Score2x =false, Speed = false;

    public bool GetShield(){
        return Shield;
    }
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
        gridPosition = new Vector2Int(-5, -5);

        gridMoveDirection = new Vector2Int(1,0);
        gridMoveDirection2 = new Vector2Int(1,0);

        segments.Clear();
        segments.Add(this.transform);

        segmentsForP2.Clear();
        segmentsForP2.Add(Player2.transform);

        uiManager = GameObject.Find("UI").GetComponent<UIManger>();
    }

    private void Update(){
        snakeLength = segments.Count;
        snake2Len = segmentsForP2.Count;

        HandleInput();  
        HandleInputFor2();

        ScreenWrapper(segments);
        ScreenWrapper(segmentsForP2);

        CheckForP2();
    }

    private void FixedUpdate(){
        
        for(int i = snakeLength - 1; i>0 ; i--)
        {
            segments[i].position = segments[i-1].position;
        }
        Transform snake1 = this.transform;
        Velocity(snake1,0);
        this.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));

        for(int j = snake2Len - 1; j>0 ; j--)
        {
            segmentsForP2[j].position = segmentsForP2[j-1].position;
        }
        Transform snake2 = Player2.transform;
        Velocity(snake2,1);
        Player2.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection2));
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

    private void HandleInputFor2(){
        if(Input.GetKeyDown(KeyCode.W)){
            if(gridMoveDirection2.y != -1){
                gridMoveDirection2.x = 0;
                gridMoveDirection2.y = 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.S)){
            if(gridMoveDirection2.y != 1){
                gridMoveDirection2.x = 0;
                gridMoveDirection2.y = -1;
            }
        }
        if(Input.GetKeyDown(KeyCode.A)){
            if(gridMoveDirection2.x != 1){
                gridMoveDirection2.x = -1;
                gridMoveDirection2.y = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.D)){
            if(gridMoveDirection2.x != -1){
                gridMoveDirection2.x = 1;
                gridMoveDirection2.y = 0;
            }
        }
    }

    public float GetAngleFromVector(Vector2Int dir){
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(n<0) n += 360;
        return n;
    }

    public void Grow(int check){
        Transform segment = Instantiate(this.segmentPrefab);
        if(check == 0){
            segment.position = segments[snakeLength - 1].position;
            segments.Add(segment);
        }
        
    }

    public void Reduce(int check){
        if(check == 0){
            Transform tail = segments[snakeLength-1];
            GameObject tailObject = tail.gameObject;
            segments.RemoveAt(snakeLength-1);
            Destroy(tailObject);
        }else{
            Transform tail = segmentsForP2[snake2Len-1];
            GameObject tailObject = tail.gameObject;
            segmentsForP2.RemoveAt(snake2Len-1);
            Destroy(tailObject);
        }
    }

    public void Velocity(Transform objct, int i){
        if(i == 0){
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
        else{
           if(Speed){
            float speed = 1.0f;
            objct.position = new Vector3(
            Mathf.Round(objct.position.x) + gridMoveDirection2.x + speed,
            Mathf.Round(objct.position.y) + gridMoveDirection2.y + speed,
            0.0f);
            StartCoroutine(SpeedPowerUp());
            }
            else{
                objct.position = new Vector3(
                Mathf.Round(objct.position.x) + gridMoveDirection2.x,
                Mathf.Round(objct.position.y) + gridMoveDirection2.y,
                0.0f
                );
            } 
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
            Grow(0);
        }
        if(other.tag == "Junk"){
            if(snakeLength > 3){
                Reduce(0);
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

    private void CheckForP2(){
        Snake2 _player2 = GetComponent<Snake2>();
        int t = _player2.GetDetect();
        if(t == 0){
            Grow(1);
        }
        if(t == 1){
            if(snake2Len > 3){
                Reduce(1);
            }
        }
        if(t == 0){
            if(!GetShield()){
                Damage();
                // Player2.enabled = false;
            }
            SetShield(false);
        }
    }
}
