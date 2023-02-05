using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerToAlchemy : MonoBehaviour
{
    public Camera MainCamera;
    public Camera AlchemyCamera;
    public GameObject SoupMaker;
    public InventoryLogic InventoryLogic;

    private SpriteRenderer sr;

    public void Start()
    {
        MainCamera.gameObject.SetActive(true);
        AlchemyCamera.gameObject.SetActive(false);
        SoupMaker.SetActive(false);

        sr = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (InventoryLogic.Inventory.Count == 8)
        {
            sr.color = new Color(255, 255, 255);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(InventoryLogic.Inventory.Count == 8)
        {
            // Debug.Log("Triggered");
            int layer = LayerMask.NameToLayer("Player");
            if (collision.gameObject.layer == layer && collision.gameObject.active)
            {
                // Debug.Log("Player detected");
                MainCamera.gameObject.SetActive(false);
                AlchemyCamera.gameObject.SetActive(true);
                SoupMaker.SetActive(true);
            }
        }

        
    }
}
