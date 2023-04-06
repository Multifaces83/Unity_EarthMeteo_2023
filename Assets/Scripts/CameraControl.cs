using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _earth;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //camera turns around the earth with mouse vertical and horizontal

        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(_earth.position, Vector3.up, Input.GetAxis("Mouse X") * 100 * Time.deltaTime);
            transform.RotateAround(_earth.position, transform.right, Input.GetAxis("Mouse Y") * 100 * Time.deltaTime);
            transform.LookAt(_earth);
        }

        // transform.RotateAround(_earth.position, Vector3.up, 20 * Time.deltaTime);
        // transform.LookAt(_earth);

    }
}
