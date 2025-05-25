using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLadder : MonoBehaviour, ILaderable
{
    [SerializeField] private GameObject mPlatformCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanLader()
    {
        return true;
    }
}
