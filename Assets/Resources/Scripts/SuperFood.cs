using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// Behaviour script for super food.
// </summary>
public class SuperFood : MonoBehaviour
{
    public float lifeTime;              //Detrmines duration of superfood on game board

	// <summary>
    // Initializes super food.
    // Starts destory and flicker timer.
    // </summary>
	private void Start ()
    {
        Destroy(gameObject, lifeTime);
        InvokeRepeating("Flicker", lifeTime*2/5, 0.2f);
	}
	
    // <summary>
    // Changes visibility for oposite.
    // </summary>
	private void Flicker()
    {
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
    }
}
