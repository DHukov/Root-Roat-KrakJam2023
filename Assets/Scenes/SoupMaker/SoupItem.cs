using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SoupItem : MonoBehaviour
{
    private int lastSelectedItem = 0;
    private Transform inner;
    private SoupItemList itemList;

    public InventoryLogic inventoryLogic;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        itemList = transform.parent.GetComponent<SoupItemList>();
        inner = transform.Find("inner");

        var test = Resources.Load<Sprite>("beet");
        inner.GetComponent<Image>().sprite = test;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryLogic)
        {
            return;
        }

        var item = inventoryLogic.GetItem(index);
        var sprite = item is null ? null : Resources.Load<Sprite>(item.Type);

        inner.GetComponent<Image>().sprite = sprite;

        if (this.lastSelectedItem != itemList.currentSelectedItem)
        {
            var material = this.GetComponent<Image>();

            if (this.index == itemList.currentSelectedItem)
            {
                material.color = new Color(255, 255, 255, 100 / 255.0f);
            }
            else
            {
                material.color = new Color(255, 255, 255, 50 / 255.0f);
            }
        }

        lastSelectedItem = itemList.currentSelectedItem;
    }
}
