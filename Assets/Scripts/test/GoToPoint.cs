using UnityEngine;
using UnityEngine.AI;

public class GoToPoint : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(_point.position);
    }
}
