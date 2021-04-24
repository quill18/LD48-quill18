using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrillMaskMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
    }

    RawImage image;

    // Update is called once per frame
    void Update()
    {
        Rect r = image.uvRect;
        r.y -= Time.deltaTime/4f;
        image.uvRect = r;
    }
}
