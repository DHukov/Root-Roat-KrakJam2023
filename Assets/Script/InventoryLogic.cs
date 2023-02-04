using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLogic : MonoBehaviour
{

    public class Item
    {
        public int Id;
        public string Name;
    }

    public List<Item> Inventory = new List<Item>();
    public int maxItemsCount = 8;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = LayerMask.NameToLayer("InventoryItem");         
        // Debug.Log("Trigger");
        if (collision.gameObject.layer == layer && collision.gameObject.active)
        {
            // Debug.Log("Layer detected");
            if (Inventory.Count + 1 <= maxItemsCount)
            {
                Item item = new Item();
                item.Id = Inventory.Count;
                Debug.Log("ID: " + item.Id);
                item.Name = collision.gameObject.name;
                Inventory.Add(item);
                Debug.Log("Inventory Count: " + Inventory.Count);
                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject);
            }
        }
    }
}
