using System.Collections.Generic;
using UnityEngine;

public class InventoryLogic : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public int Id;
        public string Name;
        public string Type;
    }

    public List<Item> Inventory = new List<Item>();
    public int maxItemsCount = 8;

    public bool HasItem(int index)
    {
        return index < Inventory.Count;
    }

    public Item GetItem(int index)
    {
        return index < Inventory.Count ? Inventory[index] : null;
    }

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
                item.Type = collision.gameObject.GetComponent<ItemData>().Type;
                Inventory.Add(item);
                Debug.Log("Inventory Count: " + Inventory.Count);
                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject);
            }
        }
    }
}
