using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        //yOffset = -150;
    }

    RectTransform rectTransform;
    public RectTransform TargetContainer;

    public float yOffset;

    // Update is called once per frame
    void Update()
    {
        // Find the topmost quest in the container
        if( TargetContainer.transform.childCount == 0 )
        {
            return; // New quests should be incoming?
        }

        Transform c = TargetContainer.transform.GetChild(0);

        float targetY = c.position.y;

        //Debug.Log(c.gameObject.name + " " + targetY);

        this.transform.position = new Vector3(
            this.transform.position.x,
            targetY + yOffset,
            this.transform.position.z
        );
    }
}
