using UnityEngine;

public class IntroducePosition : MonoBehaviour
{
    public int introduceID;

    public void introduceStart()
    {
        switch (introduceID)
        {
            case 1:
                GameManager.Instance.introduceTextManager.isQuestClear = true;
                gameObject.SetActive(false);
                break;
            case 2:
                GameManager.Instance.introduceTextManager.StartCoroutine("SecondStep");
                GameManager.Instance.player.isMove = false;
                GameManager.Instance.player.rigid.linearVelocityX = 0;
                GameManager.Instance.player.leftRight = 0;
                gameObject.SetActive(false);
                break;
        }
    }
}
