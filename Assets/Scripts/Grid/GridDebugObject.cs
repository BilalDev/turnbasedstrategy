using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] TextMeshPro textMeshPro;

    private object gridObject;

    protected virtual void Update()
    {
        if (textMeshPro != null && gridObject != null)
        {
            textMeshPro.SetText(gridObject.ToString());
        }
    }

    public virtual void SetGridObject(object gridObject)
    {
        this.gridObject = gridObject;
    }
}
