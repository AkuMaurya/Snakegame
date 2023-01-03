using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerUps;
    [SerializeField]
    private Collider2D gridArea;
    private int randomPowerup;
    Snake player;

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
        while(!player.Restart.activeSelf){
            randomPowerup = Random.Range(0,3);
            Vector2 PowerUpPositions = powerUps[randomPowerup].transform.position = RandomizePosition();
            GameObject power = Instantiate(powerUps[randomPowerup],new Vector3(PowerUpPositions.x,PowerUpPositions.y,0),Quaternion.identity);
            yield return new WaitForSeconds(5.0f); 
            Destroy(power);
        }      
    }
}