using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerIndicator : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (player.layer)
        {
            case 6:
                GetComponent<Image>().color = Color.red;
                break;
            case 7:
                GetComponent<Image>().color = Color.yellow;
                break;
            case 8:
                GetComponent<Image>().color = Color.green;
                break;
            case 9:
                GetComponent<Image>().color = Color.blue;
                break;
        }

    }
}
