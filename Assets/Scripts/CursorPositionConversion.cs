using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPositionConversion : MonoBehaviour
{
    private float _xPosition;
    private float _yPosition;
    void Start()
    {

    }


    void Update()
    {
        // Récupérer la position de la souris sur l'écran
        Vector3 mousePos = Input.mousePosition;

        // Convertir la position de la souris en coordonnées du monde
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Convertir la position en coordonnées locales de l'objet
        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        // Récupérer le rayon de la sphère à partir de son échelle
        float sphereRadius = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z) / 2f;

        // Vérifier si la position locale est à l'intérieur de la sphère
        if (localPos.magnitude <= sphereRadius)
        {
            // Faire quelque chose avec la position récupérée
            // ...
            Debug.Log(localPos.x);
        }
    }

}
