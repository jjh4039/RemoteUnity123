using UnityEngine;
using System.Collections;

public class IntroducePosition : MonoBehaviour
{
    [Header("Field")]
    public int introduceID;

    public void introduceStart()
    {

        switch (introduceID)
        {
            case 0:
                if (GameManager.Instance.player.isFocus == true) { GameManager.Instance.player.Focus(false); }
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
                GameManager.Instance.cutScene.StartCoroutine("CutSceneStart");
                GameManager.Instance.mainCamera.StartCoroutine("SizeFourZoom");
                GameManager.Instance.player.MoveStop();
                GameManager.Instance.introduceTextManager.StartCoroutine("FinalCut");
                gameObject.SetActive(false);
                break;
        }
    }
}
