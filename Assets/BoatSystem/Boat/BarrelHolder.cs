using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHolder : MonoBehaviour
{
    // Barrel sprites
    [SerializeField] private GameObject[] mOilSprites = new GameObject[6];
    // Barrels on boat counter
    int mBarrelCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject sprite in mOilSprites)
        {
            sprite.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
