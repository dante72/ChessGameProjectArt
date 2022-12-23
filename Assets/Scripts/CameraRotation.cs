using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;
//using ChessGame;

public class CameraRotation : MonoBehaviour
{
    public float MouseSens = 5;
    public float Speed = 16;

    private Quaternion originRotation;   
    private float angleVertical;
    private float angleHorizontal;

    private bool freeMode = true;




    // Start is called before the first frame update
    void Start()
    {
        originRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            freeMode = !freeMode;
            if (freeMode)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.Confined;
        }

        if (!freeMode)
            return;

        angleHorizontal += Input.GetAxis("Mouse X") * MouseSens;
        angleVertical += Input.GetAxis("Mouse Y") * MouseSens;

        angleVertical = Mathf.Clamp(angleVertical, -60, 60);

        Quaternion rotationY = Quaternion.AngleAxis(angleHorizontal, Vector3.up);
        Quaternion rotationX = Quaternion.AngleAxis(-angleVertical, Vector3.right);

        transform.rotation = originRotation * rotationY * rotationX;

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward / Speed;
        }

        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward / Speed;

        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right / Speed;

        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right / Speed;
    }
}
