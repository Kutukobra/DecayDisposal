using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waste : MonoBehaviour
{
    public int wasteCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Waste")
        {
            wasteCount++;
            Destroy(other.gameObject);
        }
    }
}
