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
            ActionComplete();
        }
    }

    // public void Spin(SpinCompleteDelegate onSpinComplete)
    public override void TakeAction(GridPosition gridPosition, Action onSpinComplete)
    {
        totalSpinAmount = 0f;

        ActionStart(onSpinComplete);
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

    public override int GetActionPoitnsCost()
    {
        return 1;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }
}