using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMoraleText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        //text.text = "Morale: " + PlayerManager.Instance.CurrentHitpoints.ToString();
        text.text = PlayerManager.Instance.CurrentHitpoints.ToString();
    }
}
