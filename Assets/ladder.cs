using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    private BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.enableClimb(this.gameObject, box.bounds);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.disableClimb();
        }
    }
}
