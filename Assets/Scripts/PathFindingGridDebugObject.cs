using TMPro;
using UnityEngine;

public class PathFindingGridDebugObject : GridDebugObject
{
    [SerializeField] private TextMeshPro gCostText;
    [SerializeField] private TextMeshPro hCostText;
    [SerializeField] private TextMeshPro fCostText;

    private PathNode pathNode;

    public override void SetGridObject(object gridObject)
    {
        pathNode = (PathNode)gridObject;
        base.SetGridObject(gridObject);
    }

    protected override void Update()
    {
        base.Update();

        gCostText.SetText(pathNode.GetGCost().ToString());
        hCostText.SetText(pathNode.GetHCost().ToString());
        fCostText.SetText(pathNode.GetFCost().ToString());
    }
}