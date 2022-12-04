using UnityEngine;

public class MouseWorld : SingletonMonoBehaviour<MouseWorld>
{
    [SerializeField] private LayerMask mousePlaneLayerMask;

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.mousePlaneLayerMask);

        return raycastHit.point;
    }
}