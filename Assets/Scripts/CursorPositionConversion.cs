using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPositionConversion : MonoBehaviour
{
    // private float _earthPositionX;
    // private float _earthPositionY;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // récupérez la position de l'impact sur la sphère
                Vector3 spherePos = hit.point;
                Debug.Log(spherePos);
                // convertir la position de la sphère en coordonnées de latitude et de longitude
                // float lat = Mathf.Atan2(spherePos.y, spherePos.x) * Mathf.Rad2Deg;
                // float lon = Mathf.Asin(spherePos.z) * Mathf.Rad2Deg;
                // transformez la position de la sphère en coordonnées locales
                Vector3 localPos = transform.InverseTransformPoint(spherePos);
                float lat = Mathf.Asin(spherePos.y / spherePos.magnitude) * Mathf.Rad2Deg;
                float lon = Mathf.Atan2(spherePos.z, spherePos.x) * Mathf.Rad2Deg;
                Debug.Log("latitude" + lat);
                Debug.Log("longitude" + lon);
            }
        }
    }



}
