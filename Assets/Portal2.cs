using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.layer == 7) {
                other.gameObject.layer = 8;
            } else if (other.gameObject.layer == 8) {
                other.gameObject.layer = 7;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
