using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlappedVerticalLayout : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        PlayerManager.Instance.onPlayerHandCountChanged += UpdateCardPositions;
    }

    RectTransform rectTransform;

    float padding = 8;


    // Update is called once per frame
    void Update() {
    }

    public void UpdateCardPositions()
    {
        int numChildren = transform.childCount;
        //Debug.Log("UpdateCardPositions: " + numChildren);

        if(numChildren == 0)
            return;

        float childHeight = transform.GetChild(0).GetComponent<RectTransform>().rect.height;

        float noOverlapHeight = childHeight*numChildren + padding * (numChildren-1);

        if(noOverlapHeight <= rectTransform.rect.height)
        {
            NoOverlap(numChildren, childHeight);
        }
        else
        {
            WithOverlap(numChildren, childHeight);
        }

    }

    void NoOverlap(int numChildren, float childHeight)
    {
        //Debug.Log("NoOverlap");
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform c = transform.GetChild(i);
            c.localPosition = new Vector3(
                0,
                (childHeight+padding) * ( -numChildren/2.0f + i ) + childHeight/2.0f,
                0
            );
        }
    }

    void WithOverlap(int numChildren, float childHeight)
    {
        //Debug.Log("WithOverlap");

        float overlappedChildHeight = (rectTransform.rect.height - childHeight) / (numChildren-1);

        for(int i = 0; i < numChildren; i++)
        {
            Transform c = transform.GetChild(i);
            c.GetComponent<CardGO>()?.StopHover();
            c.localPosition = new Vector3(
                0,
                overlappedChildHeight * ( -numChildren/2.0f + (numChildren - i - 1) ) + overlappedChildHeight/2.0f,
                0
            );
            //Debug.Log(c.localPosition.y);
        }

    }
}
