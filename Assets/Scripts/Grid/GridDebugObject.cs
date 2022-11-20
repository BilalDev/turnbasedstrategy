using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] TextMeshPro textMeshPro;
    private GridObject gridObject;

    private void Update()
    {
        if (textMeshPro != null)
        {
            textMeshPro.SetText(gridObject.ToString());
        }
    }

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
}
