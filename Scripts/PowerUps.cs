using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    // public Snake player;
    [SerializeField]
    private int PowerUpId;
    [SerializeField]
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Snake player = other.GetComponent<Snake>();
        Debug.Log("yo bro");
        if(other.tag == "Player")
        {
            Debug.Log(" bro yo");
                if(PowerUpId==0)
                {
                    player.SetSpeed(true);
                    player.Velocity(player.transform);
                }
                else if(PowerUpId==1)
                {
                    player.SetShield(true);
                    player.Damage();
                }
                else if(PowerUpId==2)
                {
                    player.SetScore2x(true);
                    player.ScoreGain();
                }
            Destroy(this.gameObject);
        }
    }
}
