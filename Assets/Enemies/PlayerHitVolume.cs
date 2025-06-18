using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitVolume : MonoBehaviour
{
    private List<IEnemies> mOverlappingEnemys = new List<IEnemies>();
    private List<Enemy> mOverlappingEnemyControllers = new List<Enemy>();

    public List<IEnemies> GetEnemies()
    {
        return mOverlappingEnemys;
    }
    public List<Enemy> GetEnemyControllers()
    {
        return mOverlappingEnemyControllers;
    }

    //On overlap
    private void OnTriggerEnter2D(Collider2D _Collider)
    {
        // Add IEnemy to list
        if (_Collider.GetComponent<IEnemies>() != null)
        {
            mOverlappingEnemys.Add(_Collider.GetComponent<IEnemies>());  //Add to list
        }
        // Add EnemyController to list
        if (_Collider.GetComponent<Enemy>() != null)
        {
            mOverlappingEnemyControllers.Add(_Collider.GetComponent<Enemy>());  //Add to list
        }


        // Check for lader component
        IEnemies EnemyInterface = _Collider.GetComponent<IEnemies>();
    }

    //On stop overlap
    private void OnTriggerExit2D(Collider2D _Collider)
    {
        // Remove IEnemy from list
        if (_Collider.GetComponent<IEnemies>() != null)
        {
            mOverlappingEnemys.Remove(_Collider.GetComponent<IEnemies>());   //Remove from list
        }
        // Remove EnemyController from list
        if (_Collider.GetComponent<Enemy>() != null)
        {
            mOverlappingEnemyControllers.Remove(_Collider.GetComponent<Enemy>());  // Remove from list
        }
    }

}
