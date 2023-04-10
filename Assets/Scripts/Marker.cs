using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    private CursorPositionConversion _cursorPositionConversion;
    private Vector3 markerPosition;
    [SerializeField] private Transform _earthTransform;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_earthTransform, Vector3.up);
    }
}
