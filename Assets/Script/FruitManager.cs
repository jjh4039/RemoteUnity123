using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public bool isEatApple;
    public bool isEatBanana;

    void Awake()
    {
        isEatApple = false;
        isEatBanana = false;
    }
}
