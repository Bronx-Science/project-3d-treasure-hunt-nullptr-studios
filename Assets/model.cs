using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class model : MonoBehaviour
{
    [SerializeField] private GameObject playerCollider;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerCollider.transform.position;
        transform.rotation = playerCollider.transform.rotation;
        
    }
}
