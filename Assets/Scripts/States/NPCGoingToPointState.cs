using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NPCGoingToPointState : NPCStateBase
{
    private AreaBase activeArea;

    public override void EnterState(NPC npc)
    {
        if (npc.ToDoList.Count > 0 && !npc.IsWaitOfPlace)
        {
            npc.IsWaitOfPlace = true;
            switch (npc.ToDoList.Peek())
            {
                case ActionType.PoolSwiming:
                    GameObject pool = GetClosestObject(GetAreasByTag(npc.CurrentActivityZone.ActivityAreas, TagsTypes.PoolZone), npc);
                    activeArea = pool.GetComponent<AreaBase>();
                    break;

                case ActionType.Eating:
                    GameObject food = GetClosestObject(GetAreasByTag(npc.CurrentActivityZone.ActivityAreas, TagsTypes.FoodZone), npc);
                    activeArea = food.GetComponent<AreaBase>();
                    break;

                case ActionType.Relaxing:
                    GameObject relax = GetClosestObject(GetAreasByTag(npc.CurrentActivityZone.ActivityAreas, TagsTypes.RestZone), npc);
                    activeArea = relax.GetComponent<AreaBase>();
                    break;
            }
            foreach (var point in activeArea.ActivityPoints)
            {
                if (!point.IsBusyForRegistration)
                {
                    npc.IsWaitOfPlace = false;
                    point.IsBusyForRegistration = true;
                    break;
                }
            }
            if (npc.IsWaitOfPlace)
            {
                activeArea.NPCTurn.Enqueue(npc);
                npc.SwitchState(npc.npcWalkingState);
            }
            else
            {
                npc.GetComponent<NavMeshAgent>().SetDestination(activeArea.EnterPoint.transform.position);
            }
        }
        else
        {
            npc.SwitchState(npc.npcWalkingState);
        }
    }

    public override void OnTriggerEnter(NPC npc, Collider collider)
    {
        if (collider.tag == TagsTypes.EnterPoint)
        {
            if (npc.ToDoList.Count != 0)
            {
                ActionType action = collider.GetComponentInParent<AreaBase>().actionType;
                if (npc.ToDoList.Peek() == action)
                {
                    npc.ToDoList.Dequeue();
                    npc.SwitchState(action, activeArea);
                    npc.SwitchState(npc.npcIdleState);
                }
            }
        }
    }

    public override void UpdateState(NPC npc)
    {
        
    }

    private GameObject[] GetAreasByTag(AreaBase[] areas, string tag)
    {
        List<GameObject> pools = new List<GameObject>();

        foreach (var Zone in areas)
        {
            if (Zone.tag == tag)
            {
                pools.Add(Zone.gameObject);
            }
        }

        GameObject[] poolsArr = new GameObject[pools.Count];

        for (int i = 0; i < poolsArr.Length; i++)
        {
            poolsArr[i] = pools[i];
        }

        return poolsArr;
    }

    private GameObject GetClosestObject(GameObject[] arr, NPC npc)
    {
        GameObject closest = arr[0];
        float closestDistance = Vector3.Distance(closest.transform.position, npc.transform.position);

        for (int i = 0; i < arr.Length; i++)
        {
            float currentDistance = Vector3.Distance(arr[i].transform.position, npc.transform.position);

            if (currentDistance < closestDistance)
            {
                closest = arr[i];
                closestDistance = currentDistance;
            }
        }

        return closest;
    }
}
