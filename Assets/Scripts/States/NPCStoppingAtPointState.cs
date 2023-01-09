using UnityEngine;

public class NPCStoppingAtPointState : NPCStateBase
{
    private float _counting;
    private float _waitToGo;

    public override void EnterState(NPC npc)
    {
        _counting = 0f;
        _waitToGo = Random.Range(1.0f, 2.5f);
    }

    public override void OnTriggerEnter(NPC npc, Collider collider)
    {
        
    }

    public override void UpdateState(NPC npc)
    {
        _counting += Time.deltaTime;
        if (_counting >= _waitToGo)
        {
            if (npc.ToDoList.Count == 0)
            {
                npc.SwitchState(npc.npcWalkingState);
            }
            else
            {
                npc.SwitchState(npc.npcGoingToPointState);
            }
        }
    }
}