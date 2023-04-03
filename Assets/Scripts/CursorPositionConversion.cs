using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPositionConversion : MonoBehaviour
{
    private float _xPosition;
    private float _yPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //on click
        if (Input.GetMouseButtonDown(0))
        {
            _xPosition = gameObject.transform.position.x;
            _yPosition = gameObject.transform.position.y;
            Debug.Log("X: " + _xPosition + " Y: " + _yPosition);
        }

    }
}
