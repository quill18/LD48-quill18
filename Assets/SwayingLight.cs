using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayingLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    RectTransform rectTransform;

    float t;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(t)  );
    }
}
