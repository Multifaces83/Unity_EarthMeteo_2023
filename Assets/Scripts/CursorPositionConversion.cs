using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CursorPositionConversion : MonoBehaviour
{


    void OnMouseDown()
    {
        GetCursorPosition();
        GetLatitude();
        GetLongitude();
        Debug.Log("local rotation : " + transform.localRotation);
    }

    public Vector3 GetCursorPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 position = hit.point;
            Vector3 localPoint = transform.InverseTransformPoint(position);
            return localPoint;
        }
        return Vector3.zero;
    }
    public float GetLatitude()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 position = hit.point;
            Vector3 localPoint = transform.InverseTransformPoint(position);
            //calculer les coordonnées de latitude
            float latitude = Mathf.Asin(localPoint.y / localPoint.magnitude) * Mathf.Rad2Deg;
            return latitude;
        }
        return 0;
    }

    public float GetLongitude()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 position = hit.point;
            Vector3 localPoint = transform.InverseTransformPoint(position);
            //calculer les coordonnées de longitude
            float longitude = Mathf.Atan2(localPoint.z, localPoint.x) * Mathf.Rad2Deg;
            return longitude;
        }
        return 0;
    }

}
