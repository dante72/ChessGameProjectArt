using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationV2 : MonoBehaviour
{
    private Quaternion originRotation;
    private float angleVertical;
    private float angleHorizontal;

    public float MouseSens = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angleHorizontal += Input.GetAxis("Mouse X") * MouseSens;
        angleVertical += Input.GetAxis("Mouse Y") * MouseSens;

        angleVertical = Mathf.Clamp(angleVertical, -60, 60);

        Quaternion rotationY = Quaternion.AngleAxis(angleHorizontal, Vector3.up);
        Quaternion rotationX = Quaternion.AngleAxis(-angleVertical, Vector3.right);

        transform.rotation = originRotation * rotationY * rotationX;
    }
}
