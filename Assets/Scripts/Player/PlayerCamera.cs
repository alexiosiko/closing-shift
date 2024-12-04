using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float sensX = 2f;
    [SerializeField] private float sensY = 2f;
    [SerializeField] private Transform playerTransform;
    private float xRotation;
    private float yRotation;

    private void Update()
    {   
        if (StatusManager.Singleton.GetFreeze() == true)
            return;

        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerTransform.Rotate(Vector3.up * mouseX);
    }
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
}