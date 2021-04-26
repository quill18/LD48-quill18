using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        Velocity.x *= Random.Range(.9f, 1.1f);
        Velocity.y *= Random.Range(.9f, 1.1f);
    }

    public Vector2 Velocity;
    public float lifespan = 2f;
    TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;

        if(lifespan <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if(lifespan < 1f)
        {
            Color c= text.color;
            c.a = lifespan;
            text.color = c;
        }

        this.transform.Translate( Velocity * Time.deltaTime );
    }
}
