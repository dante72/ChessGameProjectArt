using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Selectable current;

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, transform.forward, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("Mouse on");

            var selectable = hit.collider.gameObject.GetComponent<Selectable>();
            if (selectable)
            {
                if (current && current != selectable)
                    current.Deselect();

                current = selectable;
                selectable.Select();

                if (Input.GetMouseButtonDown(0))
                    selectable.Click();
            }          
        }
    }
}
