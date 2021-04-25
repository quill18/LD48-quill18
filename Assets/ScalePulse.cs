using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float t;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * 5f;

        transform.localScale = Vector3.one + Vector3.one * ( Mathf.Sin(t) / 10f);
    }
}
