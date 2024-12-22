using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WasteCollection : MonoBehaviour
{
    public int wasteCount = 0;

    public TextMeshProUGUI wasteText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Waste")
        {
            wasteCount++;
            wasteText.text = wasteCount.ToString();
            Destroy(other.gameObject);
        }
    }
}
