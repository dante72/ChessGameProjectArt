using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject lookAt;
    public GameObject boardObject;
    private Vector3 whitePosition = new Vector3(7, 12, -4);
    private Vector3 blackPosition = new Vector3(7, 12, 18);
    // Start is called before the first frame update

    private CameraMode currentMode = CameraMode.White;
    public static CameraMode Mode = CameraMode.White;
    void Start()
    {
        transform.position = whitePosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAt.transform);

        if (currentMode != Mode)
        {
            currentMode = Mode;
            switchCamera(currentMode);
        }
    }

    private void switchCamera(CameraMode mode)
    {
        switch(mode)
        {
            case CameraMode.White:
                transform.position = whitePosition;
                break;
            case CameraMode.Black:
                transform.position = blackPosition;
                break;
        }
    }
}
