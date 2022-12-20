using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerUps;
    [SerializeField]
    private Collider2D gridArea;
    [SerializeField]
    private int PowerUpId;

    private int randomPowerup;

    private void Start(){
        StartCoroutine(PowerupSpawnRoutin()); 
    }

    public Vector2 RandomizePosition()
    {
        Vector2 obj;
        Bounds bounds = gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);
        
        obj = new Vector2(x, y); 
        return obj;
    }

    IEnumerator PowerupSpawnRoutin()
    {
       
            randomPowerup = Random.Range(0,3);
            Vector2 gmobj = powerUps[randomPowerup].transform.position = RandomizePosition();
            GameObject power = Instantiate(powerUps[randomPowerup],new Vector3(gmobj.x,gmobj.y,0),Quaternion.identity);
            yield return new WaitForSeconds(5.0f); 
            Destroy(power);
            StartCoroutine(PowerupSpawnRoutin());       
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            Snake player = other.GetComponent<Snake>();
            if(randomPowerup==0)
                {
                    player.Speed = true;
                    player.Velocity();
                }
                else if(randomPowerup==1)
                {
                    player.Shield = true;
                    player.Damage();
                }
                else if(randomPowerup==2)
                {
                    player.Score2x = true;
                    player.ScoreGain();
                }
        }
    }

}