using UnityEngine;

public class PathNode : MonoBehaviour
{
    [SerializeField] private PathNode _previousNode;
    [SerializeField] private PathNode _nextNode;

    public PathNode PreviousNode
    {
        get
        {
            return _previousNode;
        }
    }

    public PathNode NextNode
    {
        get
        {
            return _nextNode;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, _nextNode.transform.position);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}