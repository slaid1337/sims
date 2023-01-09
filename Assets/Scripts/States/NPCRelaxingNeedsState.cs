using UnityEngine;

public class NPCRelaxingNeedsState : NPCNeedsStateBase
{
    private ActivityPoint _currentPoint;

    public override void EnterState(NPC npc)
    {
        foreach (var point in npc.CurrentActiveArea.ActivityPoints)
        {
            if (!point.IsBusy())
            {
                npc.transform.position = point.transform.position;
                point.TakePoint();
                _currentPoint = point;
                break;
            }
        }
    }

    public override void OnTriggerEnter(NPC npc, Collider collider)
    {

    }

    public override void UpdateState(NPC npc)
    {
        npc.NeedForRest.NeedsValue += Time.deltaTime * npc.SpeedNeedsAdding;

        if (npc.NeedForRest.NeedsValue > 80f)
        {
            _currentPoint.FreePoint();
            npc.SwitchState(npc.npcIdleNeedsState);
            npc.SwitchState(npc.npcGoingToPointState);
            npc.transform.position = npc.CurrentActiveArea.OutPoint.transform.position;
            npc.CurrentActiveArea = null;
        }
    }
}
