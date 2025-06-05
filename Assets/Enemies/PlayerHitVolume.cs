using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitVolume : MonoBehaviour
{
    private List<IEnemies> mOverlappingEnemys = new List<IEnemies>();

    public List<IEnemies> GetEnemies()
    {
        return mOverlappingEnemys;
    }

    private void Update()
    {

    }

    //On overlap
    private void OnTriggerEnter2D(Collider2D _Collider)
    {
        if (_Collider.GetComponent<IEnemies>() != null)
        {
            mOverlappingEnemys.Add(_Collider.GetComponent<IEnemies>());  //Add to list
        }

        // Check for lader component
        IEnemies EnemyInterface = _Collider.GetComponent<IEnemies>();
    }

    //On stop overlap
    private void OnTriggerExit2D(Collider2D _Collider)
    {
        if (_Collider.GetComponent<IEnemies>() != null)
        {
            mOverlappingEnemys.Remove(_Collider.GetComponent<IEnemies>());   //Remove from list
        }
    }

}
