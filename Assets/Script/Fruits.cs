using UnityEngine;

public class Fruits : MonoBehaviour
{
    public enum fruitsId
    {
        Apple,
        Banana
    }
    public fruitsId myid;

    public void EatFruit()
    {
        switch (myid)
        {
            case fruitsId.Apple:
                GameManager.Instance.fruitManager.isEatApple = true;
                Debug.Log("Apple");
                break;
            case fruitsId.Banana:
                Debug.Log("Banana");
                break;
        }
    }
}
