using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camControl : MonoBehaviour
{
    float sensitivity = 100f;
    public Transform playerBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y= Input.GetAxisRaw("Mouse Y");

        float mouseX = x * sensitivity * Time.deltaTime;
        float mouseY = y * sensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);
        transform.Rotate(Vector3.left * mouseY);

        
    }
}
