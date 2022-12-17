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
    //private ChessBoard _board;




    // Start is called before the first frame update
    void Start()
    {
        originRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {
        //_board = new ChessBoard(true);
        //AuthHttpClient authHttpClient = new AuthHttpClient() { BaseAddress = new Uri("https://localhost:7256/") };
        //HttpClient httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:44327/") };
        // authWebApi = new AuthWebApi(authHttpClient, httpClient);

        //var answ = await authWebApi.Autorization(new AccountRequestModel() { Login= "admin", Password= "admin" });
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogWarning("test");
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
