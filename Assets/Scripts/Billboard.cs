using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Récupérer la caméra principale (celle qui rend la vue)
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        // Faire en sorte que l'objet soit toujours orienté vers la caméra
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);
    }
}