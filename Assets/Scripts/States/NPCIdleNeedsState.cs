using UnityEngine;

public class NPCIdleNeedsState : NPCNeedsStateBase
{
    public override void EnterState(NPC npc)
    {
        
    }

    public override void OnTriggerEnter(NPC npc, Collider collider)
    {
        
    }

    public override void UpdateState(NPC npc)
    {
        if (npc.NeedForBathing.NeedsValue > 1)
            npc.NeedForBathing.NeedsValue -= Time.deltaTime * npc.SpeedNeedsLosing;
        
        if (npc.NeedForFood.NeedsValue > 1)
            npc.NeedForFood.NeedsValue -= Time.deltaTime * npc.SpeedNeedsLosing;

        if (npc.NeedForRest.NeedsValue > 1)
            npc.NeedForRest.NeedsValue -= Time.deltaTime * npc.SpeedNeedsLosing;

        SetActionWithMinNeeds(new Needs[] { npc.NeedForBathing , npc.NeedForFood, npc.NeedForRest}, npc);
    }

    private void SetActionWithMinNeeds(Needs[] needs, NPC npc)
    {
        Needs NeedsMin = needs[0];

        for (int i = 0; i < needs.Length; i++)
        {
            if (needs[i].NeedsValue < NeedsMin.NeedsValue)
            {
                NeedsMin = needs[i];
            }
        }

        if (NeedsMin.NeedsValue < 50f && !npc.ToDoList.Contains(NeedsMin.actionType))
        {
            AreaBase[] activityAreas = npc.CurrentActivityZone.ActivityAreas;

            foreach (var area in activityAreas)
            {
                if (area.actionType == NeedsMin.actionType)
                {
                    npc.ToDoList.Enqueue(NeedsMin.actionType);
                    break;
                }
            }
        }
    }
}
