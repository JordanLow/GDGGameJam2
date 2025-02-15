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
            other.gameObject.GetComponent<PlayerMovement>().portalNo = 2;
            Debug.Log("portal 2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
