using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    public Collider2D gridArea;
    public GameObject fruit;
    // public GameObject Junk;
    // private List<GameObject> _foods = new List<GameObject>();
    
    private void Start()
    {
        RandomizePosition();
        // _foods.Add(fruit);
        // _foods.Add(Junk);

    }

    private void RandomizePosition()
    {

        Bounds bounds = gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);
        
        this.transform.position = new Vector2(x, y);
        StartCoroutine(Destroy_());   
    }

    public IEnumerator Destroy_()
    {
        yield return new WaitForSeconds(10.0f); 
        RandomizePosition();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
                Instantiate(fruit);
                Destroy(this.gameObject);
        }
    }
}
