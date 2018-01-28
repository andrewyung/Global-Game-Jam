using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour {

    Vector2 floatY;
    float originalY;

    public float floatStrength;

    public float speed = 2;

    void Start()
    {
        this.originalY = transform.localPosition.y;
    }

    void Update()
    {
        floatY = transform.localPosition;
        floatY.y = (-Mathf.Sin(Time.time * speed) * floatStrength + originalY);
        transform.localPosition = floatY;
    }
}
