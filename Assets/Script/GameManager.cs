using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Start()
    {

        instance = this; //fucking  singleton


   }

    void Update()
    {
        
    }
}
