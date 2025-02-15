using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float speed = 1f;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMove(InputValue val) {
        moveDirection = val.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);
        GetComponent<Rigidbody2D>().velocity.Normalize();
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * speed;
    }
}
