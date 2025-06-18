using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_BloodExplosion : MonoBehaviour
{
    public void AnimationFinished()
    {
        Destroy(gameObject);
    }
}
