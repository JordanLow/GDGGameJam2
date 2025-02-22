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
    public GameObject onLadder;
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

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas) {
                Debug.Log("game over");
                canvas.GetComponent<levelUIManager>().showPanel();

            }
        }
    }

    public void enableClimb(GameObject other, Bounds bounds) {
        onLadder = other;
        this.ladderTopY = bounds.max.y;
        this.ladderBottomY = bounds.min.y;
    }

    public void disableClimb() {
        onLadder = null;
        isClimbing = false;
        GetComponent<Animator>().speed = 1;
    }

    void OnMove(InputValue val) {
        isClimbing = false;
        moveDirection = val.Get<Vector2>();
        GetComponent<Animator>().SetBool("isMoving", moveDirection.magnitude > 0 ? true : false);
    }

    void OnScaleLadder(InputValue val) {
        if (onLadder != null) {
            isClimbing = true;
            ladderScaling = val.Get<float>();
        }
    }

    void OnPortalActivate(InputValue val) {
        Debug.Log(onPortal);

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

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (isClimbing) {
            GetComponent<Animator>().SetBool("isClimbing", true);
            rb.gravityScale = 0f;
            rb.mass = 0;
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
            if (onLadder == null) {
                rb.gravityScale = 100f;
            } else {
                rb.gravityScale = 50f;
            }
            rb.mass = 20;
            if (moveDirection.x > 0) transform.localScale = new Vector3(1, 1, 1);
            if (moveDirection.x < 0) transform.localScale = new Vector3(-1, 1, 1);
            
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            GetComponent<Animator>().SetBool("isClimbing", false);
            
        }
        rb.velocity.Normalize();
        rb.velocity = rb.velocity * speed;
        if (rb.velocity.y < 0) {
            rb.velocity = new Vector2(rb.velocity.x * 0.2f, rb.velocity.y);
        }
    }

    
}
