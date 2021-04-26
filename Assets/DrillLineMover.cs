using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrillLineMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        minX = this.transform.localPosition.x;
        maxX = minX + 141f;
    }

    float maxX;// = 77;
    float minX;// = -64;

    public float t;
    float lastS;

    Image image;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        float s = Mathf.Sin(t);

        if(s < lastS || s < 0)
        {
            // Hide
            lastS = s;
            image.enabled = false;
            return;
        }
        else
        {
            image.enabled = true;
        }

        lastS = s;

        //Debug.Log(Mathf.Sin(t));

        this.transform.localPosition = new Vector3(
            Mathf.Lerp(maxX, minX, s),
            this.transform.localPosition.y,
            this.transform.localPosition.z
        );
    }
}
