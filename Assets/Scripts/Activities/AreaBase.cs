using System.Collections.Generic;
using UnityEngine;

public abstract class AreaBase : MonoBehaviour
{
    public Queue<NPC> NPCTurn;
    public ActionType actionType;
    public GameObject EnterPoint;
    public GameObject OutPoint;
    public ActivityPoint[] ActivityPoints;

    private void Start()
    {
        NPCTurn = new Queue<NPC>();
        foreach (var point in ActivityPoints)
        {
            point.OnFreePoint.AddListener(OnPointFree);
        }
    }

    private void OnPointFree(bool free)
    {
        if (NPCTurn.Count > 0)
        {
            NPCTurn.Dequeue().IsWaitOfPlace = false;
        }
    }
}