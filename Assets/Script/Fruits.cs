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
                GameManager.Instance.player.isMove = false;
                GameManager.Instance.player.rigid.linearVelocityX = 0;
                GameManager.Instance.player.leftRight = 0;
                break;
            case fruitsId.Banana:
                GameManager.Instance.fruitManager.isEatBanana = true;
                Debug.Log("Banana");
                break;
        }
        Invoke("EatDel", 0.55f);
        anim.SetBool("Eat", true);
    }

    public void EatDel()
    {
        Destroy(gameObject);
    }
}
