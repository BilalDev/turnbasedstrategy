using System;
using UnityEngine;

public class PathfindingUpdater : MonoBehaviour
{
    private void Start()
    {
        DesctructibleCrate.OnAnyDestroyed += DesctructibleCrate_OnAnyDestroyed;
    }

    private void DesctructibleCrate_OnAnyDestroyed(object sender, EventArgs e)
    {
        DesctructibleCrate desctructibleCrate = sender as DesctructibleCrate;
        PathFinding.Instance.SetIsWalkableGridPosition(desctructibleCrate.GetGridPosition(), true);
    }
}