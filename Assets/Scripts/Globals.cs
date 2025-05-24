using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals instance { private set; get; } // Singleton variable

    public static GameObject PlayerGameObject;
    public static bool IsPlayerInteracting = false;

    public const float mGroundYPos = -3.55f;

    void Awake()
    {
        //      -= Singleton stuff here =-
        if ((instance != null) && (instance != this))
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
}

}