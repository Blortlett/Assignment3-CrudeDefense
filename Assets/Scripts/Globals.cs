using UnityEngine;
using TMPro;

public class Globals : MonoBehaviour
{
    public static Globals instance { private set; get; } // Singleton variable

    // UI to update
    [SerializeField] private TMP_Text mMoneyUIDisplay;
    [SerializeField] private BarrelSoldAlert mMoneyUIAlert;

    // Player Money Variable
    private int mPlayerMoney = 0;
    private int mMoneyIncrement = 1000;

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


    public void BarrelSold()
    {
        mPlayerMoney += mMoneyIncrement;
        mMoneyUIAlert.TriggerAlertUI();
        mMoneyUIDisplay.text = "$" + mPlayerMoney;
    }
}