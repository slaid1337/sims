using UnityEngine;

public class ActivityZone : MonoBehaviour
{
    public AreaBase[] ActivityAreas;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}