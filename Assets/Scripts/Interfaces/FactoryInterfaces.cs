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

public interface IAcceptCoal
{
    public void OnRecieveCoal();
}

public interface IFurnaceable
{
    public void OnToggleFurnace(bool _isOn);
}

public interface IAcceptBarrels
{
    public void OnRecieveBarrel();
}