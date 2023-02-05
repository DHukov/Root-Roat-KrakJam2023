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
    private InventoryLogic.Item lastItem;

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

        if (item is null)
        {
            inner.GetComponent<Image>().enabled = false;
            inner.GetComponent<Image>().sprite = null;
        }
        else
        {
            var sprite = Resources.Load<Sprite>(item.Type);

            inner.GetComponent<Image>().enabled = true;
            inner.GetComponent<Image>().sprite = sprite;

            if (lastItem != item)
            {
                inner.GetComponent<Animator>().Play("SoupItemIn");
            }
        }

        lastItem = item;

        var material = this.GetComponent<Image>();
        material.color = new Color(255, 255, 255, 50 / 255.0f);

        if (this.index == itemList.currentSelectedItem)
        {
            material.color = new Color(255, 255, 255, 100 / 255.0f);
        }

        if (itemList.highlightItem == index)
        {
            material.color = new Color(100, 255, 100, 150 / 255.0f);
        }

        lastSelectedItem = itemList.currentSelectedItem;
    }
}
