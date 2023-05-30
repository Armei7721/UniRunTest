using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Axe")
        {
            Debug.Log("¹®Á¦°¡ ¹¹Áö??");
        }
    }
}