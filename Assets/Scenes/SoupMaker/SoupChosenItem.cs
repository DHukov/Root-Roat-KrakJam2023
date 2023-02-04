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
        if ((item is null || item.Type == string.Empty) && lastItemType is not null)
        {
            GetComponent<Image>().enabled = false;
            GetComponent<Image>().sprite = null;
            lastItemType = null;
            return;
        }

        if ((item is null || item.Type == string.Empty))
        {
            return;
        }

        if (item.Type != lastItemType)
        {
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = Resources.Load<Sprite>(item.Type);
        }

        lastItemType = item.Type;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(2, 2, 1));
    }
}
