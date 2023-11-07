using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camControl : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 100f;
    public Transform camHelper;
    public Transform playerBody;

    public Vector3 camOffset;

    bool isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isLocked = !isLocked;
        }
        if (isLocked)
        {
            camHelper.position = transform.position;
            camHelper.LookAt(playerBody.position - playerBody.right * camOffset.x);
            // set the y rotation of transform to match that of camHelper
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                camHelper.rotation.eulerAngles.y,
                0f
            );

            float x = Input.GetAxisRaw("Mouse X");
            float y = Input.GetAxisRaw("Mouse Y");

            float mouseX = x * sensitivity * Time.deltaTime;
            float mouseY = y * sensitivity * Time.deltaTime;

            playerBody.Rotate(Vector3.up * mouseX);

            transform.Rotate(Vector3.left * mouseY);

            transform.position =
                playerBody.position
                - playerBody.forward * camOffset.z
                + playerBody.up * camOffset.y;
        }

        // stop cam
    }
}
