using UnityEngine;

public class Path : MonoBehaviour
{
    public PathNode[] PathNodes;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}