using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake2 : MonoBehaviour
{
    private int detect;
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Food"){
            detect = 0;
        }
        if(other.tag == "Junk"){
            detect = 1;
        }
        if(other.tag == "Obstacle"){
            detect = 2;
        }
    }

    public int GetDetect(){
        return detect;
    }
}

// private void OnTriggerEnter2D(Collider2D other){
//         MultiPlayer Features = GetComponent<MultiPlayer>();
//         if(other.tag == "Food"){
//             Features.Grow(1);
//         }
//         if(other.tag == "Junk"){
//             if(Features.snake2Len > 3){
//                 Features.Reduce(1);
//             }
//         }
//         if(other.tag == "Obstacle"){
//             if(!Features.GetShield()){
//                 Features.Damage();
//                 this.enabled = false;
//             }
//             Features.SetShield(false);
//         }
//     }
