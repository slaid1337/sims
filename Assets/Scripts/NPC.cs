using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float SpeedNeedsLosing;
    public float SpeedNeedsAdding;

    public Queue<ActionType> ToDoList;

    public ActivityZonesContainer activityZonesContainer;
    public ActivityZone CurrentActivityZone;

    public AreaBase CurrentActiveArea;

    public bool IsWaitOfPlace;

    public Needs NeedForBathing { get; set; }
    public Needs NeedForRest { get; set; }
    public Needs NeedForFood { get; set; }

    private Path _walkingArea;

    public Path WalkingArea
    {
        get
        {
            return _walkingArea;
        }
    }

    private NPCStateBase _currentState;
    public NPCWalkingState npcWalkingState = new NPCWalkingState();
    public NPCStoppingAtPointState npcStoppingState = new NPCStoppingAtPointState();
    public NPCGoingToPointState npcGoingToPointState = new NPCGoingToPointState();
    public NPCIdleState npcIdleState = new NPCIdleState();

    private NPCNeedsStateBase _currentNeedsState;
    public NPCIdleNeedsState npcIdleNeedsState = new NPCIdleNeedsState();
    public NPCSwimmingNeedsState npcSwimmingNeedsState = new NPCSwimmingNeedsState();
    public NPCEatingNeedsState npcEatingNeedsState = new NPCEatingNeedsState();
    public NPCRelaxingNeedsState npcRelaxingNeedsState = new NPCRelaxingNeedsState();

    private void Awake()
    {
        activityZonesContainer = ActivityZonesContainer.Instance;

        ToDoList = new Queue<ActionType>();

        CurrentActivityZone = GetClosestActivityZone();

        _walkingArea = GetClosestPath();

        _currentState = npcWalkingState;
        _currentNeedsState = npcIdleNeedsState;

        NeedForFood = new Needs(ActionType.Eating, Random.Range(20.0f, 80.0f));
        NeedForRest = new Needs(ActionType.Relaxing, Random.Range(20.0f, 80.0f));
        NeedForBathing = new Needs(ActionType.PoolSwiming, Random.Range(40.0f, 80.0f));

        _currentState.EnterState(this);
        _currentNeedsState.EnterState(this);
    }

    private void OnTriggerEnter(Collider collider)
    {
        _currentState.OnTriggerEnter(this, collider);
        _currentNeedsState.OnTriggerEnter(this, collider);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
        _currentNeedsState.UpdateState(this);
        
    }

    public void SwitchState(NPCStateBase state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void SwitchState(NPCIdleNeedsState state)
    {
        _currentNeedsState = state;
        _currentNeedsState.EnterState(this);
    }

    public void SwitchState(ActionType action, AreaBase area)
    {
        CurrentActiveArea = area;
        switch (action)
        {
            case ActionType.PoolSwiming:
                _currentNeedsState = npcSwimmingNeedsState;
                break;

            case ActionType.Eating:
                _currentNeedsState = npcEatingNeedsState;
                break;

            case ActionType.Relaxing:
                _currentNeedsState = npcRelaxingNeedsState;
                break;
        }
        _currentNeedsState.EnterState(this);
    }

    private ActivityZone GetClosestActivityZone()
    {
        ActivityZone[] zones = activityZonesContainer.ActivityZones;

        ActivityZone closestZone = zones[0];
        float closestZoneDistance = Vector3.Distance(closestZone.transform.position, transform.position);

        for (int i = 0; i < zones.Length; i++)
        {
            float currentDistance = Vector3.Distance(zones[i].transform.position, transform.position);

            if (currentDistance < closestZoneDistance)
            {
                closestZone = zones[i];
                closestZoneDistance = currentDistance;
            }
        }

        return closestZone;
    }

    private Path GetClosestPath()
    {
        Path[] paths = PathsContainer.Instance.Paths;

        Path closestPath = paths[0];
        float closestPathDistance = Vector3.Distance(closestPath.transform.position, transform.position);

        for (int i = 0; i < paths.Length; i++)
        {
            float currentDistance = Vector3.Distance(paths[i].transform.position, transform.position);

            if (currentDistance < closestPathDistance)
            {
                closestPath = paths[i];
                closestPathDistance = currentDistance;
            }
        }

        return closestPath;
    }
}