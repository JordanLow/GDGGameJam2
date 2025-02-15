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
    GameObject onPortal;
    public int portalNo;
    private float ladderTopY;
    private float ladderBottomY;


    // Start is called before the first frame update
    void Start()
    {
        ladderScaling = 0f;
        onLadder = null;
        isClimbing = false;
        portalNo = -1;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ladder") {
            onLadder = other.gameObject;
            Bounds ladderBounds = other.bounds;
            ladderTopY = ladderBounds.max.y;
            ladderBottomY = ladderBounds.min.y;
        }
        if (other.tag == "Portal") {
            onPortal = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Ladder") {
            onLadder = null;
            isClimbing = false;
        }
        if (other.tag == "Portal") {
            onPortal = null;
            portalNo = -1;
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

    void OnPortalActivate(InputValue val) {
        Debug.Log(onPortal);
        if (onPortal != null) {
            if (portalNo == 1) {
                if (gameObject.layer == 6) {
                    gameObject.layer = 7;
                } else if (gameObject.layer == 7) {
                    gameObject.layer = 6;
                }
            } else if (portalNo == 2) {
                if (gameObject.layer == 7) {
                    gameObject.layer = 8;
                } else if (gameObject.layer == 8) {
                    gameObject.layer = 7;
                }
            } else if (portalNo == 3) {
                if (gameObject.layer == 9) {
                    gameObject.layer = 8;
                } else if (gameObject.layer == 8) {
                    gameObject.layer = 9;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (isClimbing) {
            GetComponent<Animator>().SetBool("isClimbing", true);
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
            if (rb.velocity.y == 0) {
                GetComponent<Animator>().speed = 0;
            } else {
                GetComponent<Animator>().speed = 1;
            }
        } else { 
            rb.gravityScale = 50f;
            if (moveDirection.x > 0) transform.localScale = new Vector3(1, 1, 1);
            if (moveDirection.x < 0) transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            GetComponent<Animator>().SetBool("isClimbing", false);
            GetComponent<Animator>().SetBool("isMoving", rb.velocity.magnitude > 0 ? true : false);
        }
        rb.velocity.Normalize();
        rb.velocity = rb.velocity * speed;
    }
}
