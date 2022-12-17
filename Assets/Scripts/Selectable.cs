using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    private Material deselected;
    public Material selected;

    private void Start()
    {
        deselected = GetComponent<Renderer>().material;
    }
    public void Select()
    {
        GetComponent<Renderer>().material = selected;
    }

    public void Deselect()
    {
        GetComponent<Renderer>().material = deselected;
    }
}
