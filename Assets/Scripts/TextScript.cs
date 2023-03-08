using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Text : MonoBehaviour
{
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_text.text != string.Empty)
        {
            _text.alpha *= 0.991f;
        }

        if (_text.alpha <= 0.333f)
        {
            _text.text = string.Empty;
            _text.alpha = 1f;
        }
    }
}
