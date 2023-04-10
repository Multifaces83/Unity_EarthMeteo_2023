using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour
{
    private bool _autoRotate = true;
    private float _speedRotation = 0.1f;
    private float _sensitivity = 1f;
    private float _autoSpeedRotation = 0.05f;
    private float _resetRotationTime = 2f;
    private float _resetRotationTimer = 0f;

    void Start()
    {

    }

    void Update()
    {
        //AutoRotation();

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _autoRotate = false;
            gameObject.transform.Rotate(0, -_speedRotation, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _autoRotate = false;
            gameObject.transform.Rotate(0, _speedRotation, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _autoRotate = false;
            gameObject.transform.Rotate(_speedRotation, 0, 0);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _autoRotate = false;
            gameObject.transform.Rotate(-_speedRotation, 0, 0);
        }

        // if (!Input.anyKey)
        // {
        //     _resetRotationTimer += Time.deltaTime;
        //     if (_resetRotationTimer >= _resetRotationTime)
        //     {
        //         _resetRotationTimer = 0f;
        //         gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //     }
        // }

        if (Input.GetMouseButton(0))
        {
            _autoRotate = false;
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            gameObject.transform.Rotate(-mouseY * _sensitivity, mouseX * _sensitivity, 0);
        }
        _autoRotate = true;

    }

    private void AutoRotation()
    {
        if (_autoRotate)
        {
            gameObject.transform.Rotate(0, -_autoSpeedRotation, 0);
        }
        else
        {
            gameObject.transform.Rotate(0, 0, 0);
        }

    }
}
