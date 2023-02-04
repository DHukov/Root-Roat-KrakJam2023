using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLogic : MonoBehaviour
{

    public List<int> Inventory = new List<int>();
    public int maxItemsCount = 8;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Trigger");
        if(collision.gameObject.layer == 6)
        {
            // Debug.Log("Layer detected");
            if(Inventory.Count + 1 < maxItemsCount)
            {
                Inventory.Add(Inventory.Count);
                // Debug.Log("Inventory Count: " + Inventory.Count);
                Destroy(collision.gameObject);
            }
        }
    }
}
