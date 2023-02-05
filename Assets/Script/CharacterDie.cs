using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDie : MonoBehaviour
{
    private Vector3 StartPosition;

    public void Start()
    {
        StartPosition = this.gameObject.transform.position;
    }


    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = LayerMask.NameToLayer("Enemy");
        if(collision.gameObject.layer == layer && collision.gameObject.active)
        {
            this.gameObject.transform.position = StartPosition;
        }
    }
}
