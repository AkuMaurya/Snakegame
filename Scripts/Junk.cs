using System.Collections.Generic;
using UnityEngine;

public class Snke : MonoBehaviour
{
    public GameObject junk;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Snake player = other.GetComponent<Snake>();
            if(player.Score>4){
                Instantiate(junk);
                Destroy(this.gameObject);
            }
        }
    }
}