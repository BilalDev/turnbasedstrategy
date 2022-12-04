using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    private List<Vector3> positionList;
    private int currentPositionIndex;
    private float stoppingDistance = .1f;
    private float moveSpeed = 4f;
    private float rotateSpeed = 10f;
    [SerializeField] private int maxMoveDistance = 4;
    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        Vector3 targetPosition = positionList[currentPositionIndex];
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // rotate
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);

        // move/animate this unit
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            currentPositionIndex++;
            if (currentPositionIndex >= positionList.Count)
            {
                OnStopMoving?.Invoke(this, EventArgs.Empty);
                ActionComplete();
            }
        }
    }


    public override void TakeAction(GridPosition gridPosition, Action onMoveComplete)
    {
        List<GridPosition> pathGridPositionList = PathFinding.Instance.FindPath(unit.GetGridPosition(), gridPosition, out int pathLength);
        currentPositionIndex = 0;
        positionList = new List<Vector3>();

        foreach (GridPosition pathGridPosition in pathGridPositionList)
        {
            positionList.Add(LevelGrid.Instance.GetWorldPosition(pathGridPosition));
        }

        OnStartMoving?.Invoke(this, EventArgs.Empty);

        ActionStart(onMoveComplete);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                // Same Grid Position where the unit is currently at
                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }

                // Grid position already occupied with another unit
                if (LevelGrid.Instance.HasUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                if (!PathFinding.Instance.IsWalkableGridPosition(testGridPosition))
                {
                    continue;
                }

                // Checks if there is a path to the destination grid position
                if (!PathFinding.Instance.HasPath(unitGridPosition, testGridPosition))
                {
                    continue;
                }

                // Before that we were able to go from a position + 1 even if there was a wall in between
                if (PathFinding.Instance.GetPathLength(unitGridPosition, testGridPosition) > maxMoveDistance)
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtGridPosition = unit.GetAction<ShootAction>().GetTargetCountAtPosition(gridPosition);

        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = targetCountAtGridPosition * 10,
        };
    }
}
