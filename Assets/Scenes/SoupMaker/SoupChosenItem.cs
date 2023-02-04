using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoupChosenItem : MonoBehaviour
{
    private string lastItemType;
    public InventoryLogic.Item item;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (item is null && lastItemType is not null)
        {
            GetComponent<Image>().sprite = null;
            lastItemType = null;
            return;
        }

        if (item.Type != lastItemType)
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>(item.Type);
        }

        lastItemType = item.Type;
    }
}
