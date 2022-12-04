using System;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    private Vector3 targetPosition;
    private float moveSpeed = 15f;
    private float damageRadius = 4f;
    private float reachedTargetDistance = .2f;
    private Action onGrenadeBehaviourComplete;

    private void Update()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < reachedTargetDistance)
        {
            Collider[] colliders = Physics.OverlapSphere(targetPosition, damageRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<Unit>(out Unit targetUnit))
                {
                    targetUnit.Damage(30);
                }
            }
            Destroy(gameObject);

            onGrenadeBehaviourComplete();
        }
    }

    public void Setup(GridPosition targetGridPosition, Action onGrenadeBehaviourComplete)
    {
        this.onGrenadeBehaviourComplete = onGrenadeBehaviourComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
    }
}