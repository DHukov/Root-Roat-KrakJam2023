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

    public string[] inventory = new string[8];
    public int currentSelectedItem = 0;

    public SoupItemList()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = "carrot";
        }
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
            if (!chooseLock) {
                chooseLock = true;
                if (chosenFirstItem)
                {
                    itemChosenRight.GetComponent<Image>().sprite = Resources.Load<Sprite>(inventory[currentSelectedItem]);
                    chosenFirstItem = !chosenFirstItem;
                }
                else
                {
                    itemChosenLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>(inventory[currentSelectedItem]);
                    chosenFirstItem = !chosenFirstItem;
                }
            }
        }
        else
        {
            chooseLock = false;
        }
    }
}
