using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoupItemList : MonoBehaviour
{
    private const int MAX_ITEMS = 8;
    private float lastMoveTimeout = 0;
    private SoupChosenItem itemChosenLeft;
    private SoupChosenItem itemChosenRight;
    private bool chosenFirstItem = false;
    private bool chooseLock = false;

    public InventoryLogic inventoryLogic;
    public int currentSelectedItem = 0;

    public SoupItemList()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        itemChosenLeft = transform.Find("soupItemChosenLeft").GetComponent<SoupChosenItem>();
        itemChosenRight = transform.Find("soupItemChosenRight").GetComponent<SoupChosenItem>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > -0.05 && horizontal < 0.05)
        {
            lastMoveTimeout = 0;
        }

        if (lastMoveTimeout <= 0)
        {
            if (horizontal < -0.05)
            {
                lastMoveTimeout = 0.3f;

                if (currentSelectedItem > 0)
                {
                    currentSelectedItem -= 1;
                }
            }
            else if (horizontal > 0.05)
            {
                lastMoveTimeout = 0.3f;

                if (currentSelectedItem < MAX_ITEMS - 1)
                {
                    currentSelectedItem += 1;
                }
            }
        }
        else
        {
            lastMoveTimeout -= Time.deltaTime;
        }

        if (Input.GetAxis("Jump") > 0.1)
        {
            if (!chooseLock)
            {
                chooseLock = true;

                if (chosenFirstItem)
                {
                    itemChosenRight.item = inventoryLogic.GetItem(currentSelectedItem);
                    chosenFirstItem = false;

                    if (itemChosenLeft.item.Type == itemChosenRight.item.Type)
                    {
                        itemChosenLeft.GetComponent<Animator>().Play("Wrong");
                        itemChosenRight.GetComponent<Animator>().Play("Wrong");
                    }
                    else
                    {
                        itemChosenLeft.GetComponent<Animator>().Play("OkayLeft");
                        itemChosenRight.GetComponent<Animator>().Play("OkayRight");
                    }
                }
                else
                {
                    itemChosenLeft.item = inventoryLogic.GetItem(currentSelectedItem);
                    itemChosenLeft.GetComponent<Animator>().Play("Stop");
                    chosenFirstItem = true;
                }
            }
        }
        else
        {
            chooseLock = false;
        }
    }
}
