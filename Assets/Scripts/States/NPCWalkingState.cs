using UnityEngine;
using UnityEngine.AI;

public class NPCWalkingState : NPCStateBase
{
    private NavMeshAgent _npcNavMeshAgent;
    private PathNode _currentNode;

    public override void EnterState(NPC npc)
    {
        if (_currentNode == null)
        {
            PathNode[] nodes = npc.WalkingArea.PathNodes;

            PathNode closestNode = nodes[0];
            float closestNodeDistance = Vector3.Distance(closestNode.transform.position, npc.transform.position);

            for (int i = 0; i < nodes.Length; i++)
            {
                float currentDistance = Vector3.Distance(nodes[i].transform.position, npc.transform.position);

                if (currentDistance < closestNodeDistance)
                {
                    closestNode = nodes[i];
                    closestNodeDistance = currentDistance;
                }
            }

            _currentNode = closestNode;

            _npcNavMeshAgent = npc.GetComponent<NavMeshAgent>();
            _npcNavMeshAgent.SetDestination(closestNode.transform.position);
        }
        else
        {
            ChangeNode(_currentNode, Random.Range(0.2f, 1.0f));
        }
    }

    public override void OnTriggerEnter(NPC npc, Collider collider)
    {
        if (collider.tag == TagsTypes.StayPoint)
        {
            npc.SwitchState(npc.npcStoppingState);
        }
    }

    public override void UpdateState(NPC npc)
    {
        
    }

    private void ChangeNode(PathNode node, float waitTime)
    {
        int number = Random.Range(0, 100);
        PathNode newNode = null;

        if (number < 34)
        {
            newNode = node.PreviousNode;
        }
        else
        {
            newNode = node.NextNode;
        }

        _currentNode = newNode;

        _npcNavMeshAgent.SetDestination(newNode.transform.position);
    }
}