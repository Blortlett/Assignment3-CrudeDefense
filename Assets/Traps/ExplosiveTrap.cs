using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : MonoBehaviour, IPickupable
{
    private float mOriginalYPosition;
    private bool mIsTrapUsed = false;
    public Animator mAnimator;
    private const string TriggerTrapName = "TrapTriggered";
    private List<GameObject> mOverlappingObjects = new List<GameObject>();

    public bool CanPickup()
    {
        return true;
    }

    public float GetOriginalYPosition()
    {
        return mOriginalYPosition;
    }

    public void Pickup()
    {
        mAnimator.SetBool(TriggerTrapName, true);   //Change animation to triggered state
    }

    public void PutDown()
    {
        mAnimator.SetBool(TriggerTrapName, false);  //Change animation to normal idle state
    }

    private void Awake()
    {
        mOriginalYPosition = transform.position.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!mIsTrapUsed)
        {
            foreach (GameObject Object in mOverlappingObjects)   //For each overlapping object
            {
                IEnemy EnemyScript = Object.GetComponent<IEnemy>();    //Get enemy script

                if (EnemyScript != null)    //If is enemy
                {
                    mIsTrapUsed = true;
                    EnemyScript.Trap();
                    mAnimator.SetBool(TriggerTrapName, true);   //Change animation to triggered state

                }
            }
        }
    }

    //On overlap
    private void OnTriggerEnter2D(Collider2D _Collider)
    {
        mOverlappingObjects.Add(_Collider.gameObject);  //Add to list
    }

    //On stop overlap
    private void OnTriggerExit2D(Collider2D _Collider)
    {
        mOverlappingObjects.Remove(_Collider.gameObject);   //Remove from list
    }

}
