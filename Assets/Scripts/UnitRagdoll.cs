using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone;
    private const float EXPLOSION_FORCE = 300f;
    private const float EXPLOSION_RANGE = 10f;

    public void Setup(Transform originalRootbone)
    {
        MatchAllChildTransforms(originalRootbone, ragdollRootBone);
        Vector3 randomDirection = new Vector3(Random.Range(-1f, +1f), 0, Random.Range(-1f, +1f));

        ApplyExplosionToRagdoll(ragdollRootBone, EXPLOSION_FORCE, transform.position + randomDirection, EXPLOSION_RANGE);
    }

    private void MatchAllChildTransforms(Transform root, Transform clone)
    {
        foreach (Transform child in root)
        {
            Transform cloneChild = clone.Find(child.name);

            if (cloneChild != null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;

                MatchAllChildTransforms(child, cloneChild);
            }
        }
    }

    private void ApplyExplosionToRagdoll(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);
            }

            ApplyExplosionToRagdoll(child, explosionForce, explosionPosition, explosionRange);
        }
    }
}