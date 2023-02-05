using UnityEngine;
using TMPro;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField] GameObject TimeLine;

    private bool isInRange = false;
    [SerializeField] private TMP_Text textComponent;
    public KeyCode interactKey;
    [SerializeField] private Canvas canvasActive;
    public string[] dialogText;
    private int index;
    void Start()
    {
        canvasActive.gameObject.active = false;
    }
    private void Update()
    {
        InteractAction();
    }
    private void InteractAction()
    {
        if (isInRange) 
        {
            if (Input.GetKeyDown(interactKey))
            {
                TimeLine.gameObject.active = true;

                /*
                if (index <= dialogText.Length - 1)
                {
                    textComponent.text = dialogText[index];
                    index++;
                    //throw new System.Exception("Invalid index");
                }
                */
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Check interaction Enter");
            isInRange = true;
            canvasActive.gameObject.active = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Check interaction Exit");
            canvasActive.gameObject.active = false;

            isInRange = false;
        }
    }
}
