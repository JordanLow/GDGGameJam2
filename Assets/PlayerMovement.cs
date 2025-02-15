using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float speed = 1f;
    bool isClimbing;
    Vector2 moveDirection;
    float ladderScaling;
    GameObject onLadder;
    private float ladderTopY;
    private float ladderBottomY;


    // Start is called before the first frame update
    void Start()
    {
        ladderScaling = 0f;
        onLadder = null;
        isClimbing = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ladder") {
            onLadder = other.gameObject;
            Bounds ladderBounds = other.bounds;
            ladderTopY = ladderBounds.max.y;
            ladderBottomY = ladderBounds.min.y;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Ladder") {
            onLadder = null;
            isClimbing = false;
        }
    }

    void OnMove(InputValue val) {
        isClimbing = false;
        moveDirection = val.Get<Vector2>();
    }

    void OnScaleLadder(InputValue val) {
        if (onLadder != null) {
            isClimbing = true;
            ladderScaling = val.Get<float>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (isClimbing) {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0, ladderScaling);
            if (rb.position.y >= ladderTopY - 0.1f && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0); // Stop upward movement
            }
            if (rb.position.y <= ladderBottomY && rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0); // Stop upward movement
            }
        } else { 
            rb.gravityScale = 30f;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            GetComponent<Animator>().SetBool("isMoving", rb.velocity.magnitude > 0 ? true : false);
        }
        rb.velocity.Normalize();
        rb.velocity = rb.velocity * speed;
    }
}
