using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float mouseSensitivity;

    [SerializeField] float maxRotation;
    [SerializeField] float minRotation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        xRotation = 0f;
        yRotation = 0f;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        xRotation = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        yRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation = Mathf.Clamp(yRotation, minRotation, maxRotation);

        player.transform.Rotate(new Vector3(0f, xRotation, 0f));
        transform.localRotation = Quaternion.Euler(-yRotation, 0f, 0f);
        
    }
}
