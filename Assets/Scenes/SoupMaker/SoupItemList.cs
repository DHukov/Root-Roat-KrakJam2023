using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoupItemList : MonoBehaviour
{
    private const int MAX_ITEMS = 8;
    private float lastMoveTimeout = 0;
    private SoupChosenItem itemChosenLeft;
    private SoupChosenItem itemChosenRight;
    private Transform itemResult;
    private bool chosenFirstItem = false;
    private bool chooseLock = false;
    private bool isPlayingAnimation = false;
    private bool isShowingResult = false;
    private string result = null;
    private List<string[]> mixables = new List<string[]> {
        new string[] { "knife_2", "turnip", "cut-turnip" },
        new string[] { "peeler", "carrot", "peeled-carrot" },
        new string[] { "knife", "parsley", "cut-parsley" },
        new string[] { "beetroot", "water", "borshch" },
        new string[] { "cut-turnip", "peeled-carrot", "vegetables" },
        new string[] { "cut-parsley", "borshch", "decoction" },
        new string[] { "vegetables", "decoction", "soup" },
    };

    public InventoryLogic inventoryLogic;
    public int currentSelectedItem = 0;
    public int highlightItem = -1;
    [SerializeField] private AudioSource successSound;
    [SerializeField] private AudioSource failedSound;
    [SerializeField] private AudioSource craftSound;

    public SoupItemList()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        itemChosenLeft = transform.Find("soupItemChosenLeft").GetComponent<SoupChosenItem>();
        itemChosenRight = transform.Find("soupItemChosenRight").GetComponent<SoupChosenItem>();
        itemResult = transform.Find("soupItemResult");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayingAnimation)
        {
            if (itemChosenLeft.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length <= itemChosenLeft.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                itemChosenLeft.item = null;
                itemChosenRight.item = null;
                isPlayingAnimation = false;

                if (result is not null) {
                    itemResult.GetComponent<SoupChosenItem>().item = new InventoryLogic.Item() { Type = result };
                    itemResult.GetComponent<Animator>().Play("ResultShow");
                    successSound.Play();
                    isShowingResult = true;
                }
            }

            return;
        }

        if (isShowingResult)
        {
            if (itemResult.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length <= itemResult.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                isShowingResult = false;
                this.inventoryLogic.AddItem(result);

                if (result == "soup")
                {
                    SceneManager.LoadScene("Main menu");
                }
            }

            return;
        }

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

                    if (itemChosenLeft.item is not null && inventoryLogic.GetItem(currentSelectedItem) == itemChosenLeft.item)
                    {
                        if (currentSelectedItem > 0)
                        {
                            currentSelectedItem -= 1;
                        }
                        else
                        {
                            currentSelectedItem += 1;
                        }
                    }
                }
            }
            else if (horizontal > 0.05)
            {
                lastMoveTimeout = 0.3f;

                if (currentSelectedItem < MAX_ITEMS - 1)
                {
                    currentSelectedItem += 1;

                    if (itemChosenLeft.item is not null && inventoryLogic.GetItem(currentSelectedItem) == itemChosenLeft.item)
                    {
                        if (currentSelectedItem < MAX_ITEMS - 1)
                        {
                            currentSelectedItem += 1;
                        }
                        else
                        {
                            currentSelectedItem -= 1;
                        }
                    }
                }
            }
        }
        else
        {
            lastMoveTimeout -= Time.deltaTime;
        }

        if (Input.GetAxis("Jump") > 0.1)
        {
            if (!inventoryLogic.HasItem(currentSelectedItem))
            {
                return;
            }

            if (inventoryLogic.GetItem(currentSelectedItem) == itemChosenLeft.item)
            {
                return;
            }


            if (!chooseLock)
            {
                chooseLock = true;

                if (chosenFirstItem)
                {
                    itemChosenRight.item = inventoryLogic.GetItem(currentSelectedItem);
                    chosenFirstItem = false;
                    result = FindResultOfMixing(itemChosenLeft.item.Type, itemChosenRight.item.Type);

                    if (result is null)
                    {
                        itemChosenLeft.GetComponent<Animator>().Play("Wrong");
                        itemChosenRight.GetComponent<Animator>().Play("Wrong");
                        failedSound.Play();
                    }
                    else
                    {
                        itemChosenLeft.GetComponent<Animator>().Play("OkayLeft");
                        itemChosenRight.GetComponent<Animator>().Play("OkayRight");
                        craftSound.Play();
                        inventoryLogic.RemoveItem(itemChosenLeft.item);
                        inventoryLogic.RemoveItem(itemChosenRight.item);
                    }

                    highlightItem = -1;
                    isPlayingAnimation = true;
                }
                else
                {
                    itemChosenLeft.item = inventoryLogic.GetItem(currentSelectedItem);
                    itemChosenLeft.GetComponent<Animator>().Play("Stop");
                    chosenFirstItem = true;
                    highlightItem = currentSelectedItem;
                }
            }
        }
        else
        {
            chooseLock = false;
        }
    }

    private string FindResultOfMixing(string ingredient1, string ingredient2)
    {
        for (int i = 0; i < mixables.Count; i++)
        {
            if ((mixables[i][0] == ingredient1 && mixables[i][1] == ingredient2)
                || (mixables[i][1] == ingredient1 && mixables[i][0] == ingredient2))
            {
                return mixables[i][2];
            }
        }

        return null;
    }
}
