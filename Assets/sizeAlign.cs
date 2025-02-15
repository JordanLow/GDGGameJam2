using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeAlign : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float height;
    [SerializeField] float width;
    [SerializeField] float x_pos;
    [SerializeField] float y_pos;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(width, height);
        GetComponent<SpriteRenderer>().size = new Vector2(width, height);
        GetComponent<Transform>().position = new Vector2(x_pos,y_pos);
    }
}
