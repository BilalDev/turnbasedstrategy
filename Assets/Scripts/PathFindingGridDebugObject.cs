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
        base.SetGridObject(gridObject);
        pathNode = (PathNode)gridObject;
    }

    protected override void Update()
    {
        base.Update();

        gCostText.SetText($"G: {pathNode.GetGCost().ToString()}");
        hCostText.SetText($"H: {pathNode.GetHCost().ToString()}");
        fCostText.SetText($"F: {pathNode.GetFCost().ToString()}");
    }
}