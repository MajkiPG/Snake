using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// <summary>
// Behaviour scrtipt for individual segment.
// </summary>
public class Segment : MonoBehaviour
{
    static public Action<String> collision;         //Stores collision action.

    // <summary>
    // Handles collision with other object.
    // <summary
    private void OnTriggerEnter(Collider other)
    {
        if(collision != null)
        {
            collision(other.tag);
        }

        if (other.tag == "Food" || other.tag == "SuperFood")
        {
            Destroy(other.gameObject);
        }
    }

    // <summary>
    // Changes segment sprite for new from input.
    // </summary>
    public void ChangeSprite(Sprite newSprite)
    {
        this.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
