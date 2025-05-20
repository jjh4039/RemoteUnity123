using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KoreanTyper;

public class Dialogues : MonoBehaviour
{
    public enum DialoguePlace
    {
        FoodStail,
        Table,
        Sings
    }

    static public bool isDialogue;
    public DialoguePlace myPlace;
    public GameObject PressF;
    public KoreanTyperDemo_Auto DialogueScript;

    void Start()
    {
        DialogueScript = GameObject.Find("Dialogue").GetComponent<KoreanTyperDemo_Auto>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isDialogue == false)
        {
            PressF.SetActive(true);
            if ((Input.GetKeyDown(KeyCode.F)))
            {
                Debug.Log("Dialogue started");
                PressF.SetActive(false);
                isDialogue = true;
                DialogueScript.StartCoroutine(DialogueScript.TypingText(0));
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PressF.SetActive(false);
        }
    }
}
