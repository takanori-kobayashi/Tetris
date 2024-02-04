using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alphatest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color color = GetComponent<Renderer>().material.color;
        color.a = 0.6f;
        GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
