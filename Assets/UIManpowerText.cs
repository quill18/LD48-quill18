using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManpowerText : MonoBehaviour
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
        //text.text = "Workforce: " + PlayerManager.Instance.CurrentMana.ToString() + "\n(Next Shift: " + PlayerManager.Instance.MaxMana.ToString() + ")";
        text.text = PlayerManager.Instance.CurrentMana.ToString();
    }
}
