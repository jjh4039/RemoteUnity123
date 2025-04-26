using UnityEngine;
using System.Collections;

public class IntroducePosition : MonoBehaviour
{
    public int introduceID;

    public void introduceStart()
    {
        switch (introduceID)
        {
            case 0:
                GameManager.Instance.player.Die();
                break;
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
            case 4:
                GameManager.Instance.introduceTextManager.StartCoroutine("OneMore");
                GameManager.Instance.player.MoveStop();
                gameObject.SetActive(false);
                break;
            case 5:
                GameManager.Instance.introduceTextManager.StartCoroutine("HighJump");
                GameManager.Instance.player.MoveStop();
                GameManager.Instance.mainCamera.cameraLevel = 1;
                GameManager.Instance.mainCamera.StartCoroutine("SizeFiveZoom");
                gameObject.SetActive(false);
                break;
            case 6:
                GameManager.Instance.mainCamera.cameraLevel = 2;
                GameManager.Instance.mainCamera.StartCoroutine("SizeFourZoom");
                gameObject.SetActive(false);
                break;
        }
    }
}
