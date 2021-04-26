using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    Vector3 initPos;
    float duration = 1f;
    float timeFactor = 30f;
    float distFactor = 5f;

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;

        if(duration < 0)
        {
            Destroy(this);
        }
        transform.position = initPos + Vector3.right*(Mathf.Sin(duration*timeFactor))*distFactor;
    }
}
