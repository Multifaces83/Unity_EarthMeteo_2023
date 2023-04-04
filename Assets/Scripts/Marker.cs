using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private Transform _earthTransform;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_earthTransform, Vector3.up);
    }
}
