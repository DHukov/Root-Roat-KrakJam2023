using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerToAlchemy : MonoBehaviour
{
    public Camera MainCamera;
    public Camera AlchemyCamera;
    public GameObject SoupMaker;

    public void Start()
    {
        MainCamera.gameObject.SetActive(true);
        AlchemyCamera.gameObject.SetActive(false);
        SoupMaker.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        int layer = LayerMask.NameToLayer("Player");
        if(collision.gameObject.layer == layer && collision.gameObject.active)
        {
            Debug.Log("Player detected");
            MainCamera.gameObject.SetActive(false);
            AlchemyCamera.gameObject.SetActive(true);
            SoupMaker.SetActive(true);
        }
    }
}
