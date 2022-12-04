using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : SingletonMonoBehaviour<UnitActionSystem>
{
    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction selectedAction;
    private bool isBusy;

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    private void Update()
    {
        if (isBusy)
        {
            return;
        }

        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // select unit when right mouse click
        if (TryHandleUnitSelection())
        {
            return;
        }

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (InputManager.Instance.IsLeftMouseButtonDown())
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (!selectedAction.IsvalidActionGridPosition(mouseGridPosition))
            {
                return;
            }
            if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction))
            {
                return;
            }

            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);

            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SetBusy()
    {
        OnBusyChanged?.Invoke(this, true);
        isBusy = true;
    }

    private void ClearBusy()
    {
        OnBusyChanged?.Invoke(this, false);
        isBusy = false;
    }

    private bool TryHandleUnitSelection()
    {
        if (InputManager.Instance.IsRightMouseButtonDown())
        {
            Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());

            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if (unit == selectedUnit)
                    {
                        // Unit is already selected
                        return false;
                    }

                    if (unit.IsEnemy())
                    {
                        return false;
                    }

                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetAction<MoveAction>());

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;

        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
}