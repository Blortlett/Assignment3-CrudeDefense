using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButtonable
{
    public void OnButtonPress();
}
public interface ILaderable
{
    public bool CanLader();
}

public interface IValveWheelable
{
    public void FillTank(float WheelOpenPercent);
}

public interface IPressurePlateable
{
    public void PressurePlatePushed();
}