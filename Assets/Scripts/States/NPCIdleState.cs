using UnityEngine;
using UnityEngine.AI;

public class NPCIdleState : NPCStateBase
{
    public override void EnterState(NPC npc)
    {
        npc.GetComponent<NavMeshAgent>().ResetPath();
        npc.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
    }

    public override void OnTriggerEnter(NPC npc, Collider collider)
    {
        
    }

    public override void UpdateState(NPC npc)
    {
        
    }
}
