using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    public InventoryLogic inventoryLogic;
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var item = inventoryLogic.GetItem(index);

        if (item is null)
        {
            GetComponent<Image>().sprite = null;
            GetComponent<Image>().enabled = false;
        }
        else
        {
            var sprite = Resources.Load<Sprite>(item.Type);

            GetComponent<Image>().sprite = sprite;
            GetComponent<Image>().enabled = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(1.5f, 1.5f, 1));
    }
}
