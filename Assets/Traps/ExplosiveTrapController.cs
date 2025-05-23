using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosiveTrapController : MonoBehaviour, IPickupable
{
    public Animator mAnimator;
    private const string TriggerTrapName = "TrapTriggered";
    private bool mIsTrapUsed = false;

    const bool mCanPickup = true;
    private float mOriginalYPosition;

    private List<GameObject> mOverlappingObjects = new List<GameObject>();

    public bool CanPickup()
    {
        return mCanPickup;
    }

    public GameObject Pickup()
    {
        mAnimator.SetBool(TriggerTrapName, true);   //Change animation to triggered state
        return gameObject;
    }

    public void PutDown()
    {
        mAnimator.SetBool(TriggerTrapName, false);  //Change animation to normal idle state
    }

    public float GetOriginalYPosition()
    {
        return mOriginalYPosition;
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
            foreach (GameObject Object in mOverlappingObjects.ToList())   //For each overlapping object
            {
                IEnemy EnemyScript = Object.GetComponent<IEnemy>();    //Get enemy script

                if (EnemyScript != null)    //If is enemy
                {
                    mIsTrapUsed = true;
                    EnemyScript.Trap();

                    //mAnimator.SetBool(TriggerTrapName, true);   //Change animation to triggered state
                    EnemyScript.BlowUp();
                    this.GetComponent<Explosion>().Explode();
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
