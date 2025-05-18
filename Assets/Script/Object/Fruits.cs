using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fruits : MonoBehaviour
{
    public enum fruitsId
    {
        Apple,
        Banana
    }
    public fruitsId myid;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void EatFruit()
    {
        switch (myid)
        {
            case fruitsId.Apple:
                GameManager.Instance.fruitManager.isEatApple = true;
                GameManager.Instance.introduceTextManager.StartCoroutine("ThirdStep");
                GameManager.Instance.fruitManager.EatIndex = 1;
                GameManager.Instance.fruitManager.StartCoroutine("StartGuide");
                GameManager.Instance.player.MoveStop();
                break;
            case fruitsId.Banana:
                GameManager.Instance.fruitManager.isEatBanana = true;
                GameManager.Instance.fruitManager.EatIndex = 2;
                GameManager.Instance.introduceTextManager.StartCoroutine("FifthStep");
                GameManager.Instance.fruitManager.StartCoroutine("StartGuide");
                GameManager.Instance.player.MoveStop();
                break;
        }
        anim.SetBool("Eat", true);
        Invoke("EatDel", 0.50f);
    }

    public void EatDel()
    {
        Destroy(gameObject);
    }
}
