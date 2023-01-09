using UnityEngine;

public abstract class NPCNeedsStateBase
{
    public abstract void EnterState(NPC npc);

    public abstract void UpdateState(NPC npc);

    public abstract void OnTriggerEnter(NPC npc, Collider collider);
}
