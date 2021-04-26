using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManpowerNext : MonoBehaviour
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
        text.text = "(Next Shift: " + PlayerManager.Instance.MaxMana.ToString() + ")";
    }
}
