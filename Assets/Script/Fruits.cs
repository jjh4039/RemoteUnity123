using UnityEngine;
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
                GameManager.Instance.player.MoveStop();
                break;
            case fruitsId.Banana:
                GameManager.Instance.fruitManager.isEatBanana = true;
                GameManager.Instance.introduceTextManager.StartCoroutine("FifthStep");
                GameManager.Instance.player.MoveStop();
                break;
        }
        Invoke("EatDel", 0.50f);
        anim.SetBool("Eat", true);
    }

    public void EatDel()
    {
        Destroy(gameObject);
    }
}
