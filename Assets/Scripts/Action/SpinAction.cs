using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    // public delegate void SpinCompleteDelegate();
    private float totalSpinAmount;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        totalSpinAmount += spinAddAmount;
        if (totalSpinAmount >= 360f)
        {
            isActive = false;
            onActionComplete();
        }
    }

    // public void Spin(SpinCompleteDelegate onSpinComplete)
    public override void TakeAction(GridPosition gridPosition, Action onSpinComplete)
    {
        onActionComplete = onSpinComplete;
        isActive = true;
        totalSpinAmount = 0f;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override string GetActionName()
    {
        return "Spin";
    }
}