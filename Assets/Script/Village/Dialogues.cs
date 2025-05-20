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
        Signs,
        board
    }

    static public bool isDialogue;
    public bool isStayPlayer;
    public DialoguePlace myPlace;
    public GameObject PressF;
    public KoreanTyperDemo_Auto DialogueScript;

    void Start()
    {
        isStayPlayer = false;
        isDialogue = false;
        DialogueScript = GameObject.Find("Dialogue").GetComponent<KoreanTyperDemo_Auto>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F) && isStayPlayer == true))
        {
            isStayPlayer = false;
            isDialogue = true;
            PressF.SetActive(false);
            if (GameManager.Instance.player.isFocus == true) { GameManager.Instance.player.Focus(false); GameManager.Instance.player.ResetFruit(); }

            switch (myPlace)
            {
                case DialoguePlace.FoodStail:
                    DialogueScript.StartCoroutine(DialogueScript.TypingText(0));
                    break;
                case DialoguePlace.Table:
                    DialogueScript.StartCoroutine(DialogueScript.TypingText(1));
                    break;
                case DialoguePlace.Signs:
                    DialogueScript.StartCoroutine(DialogueScript.TypingText(2));
                    break;
                case DialoguePlace.board:
                    DialogueScript.StartCoroutine(DialogueScript.TypingText(3));
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isDialogue == false)
        {
            PressF.SetActive(true);
            isStayPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PressF.SetActive(false);
            isStayPlayer = false;
        }
    }
}
