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
                GameManager.Instance.player.MoveStop();
                gameObject.SetActive(false);
                break;
            case 3:
                GameManager.Instance.introduceTextManager.StartCoroutine("FourthStep");
                GameManager.Instance.player.MoveStop();
                gameObject.SetActive(false);
                break;
        }
    }
}
