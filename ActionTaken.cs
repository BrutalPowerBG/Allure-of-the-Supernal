using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionTaken : MonoBehaviour {
    protected char type;
    protected Transform minionTakingAction;
    public abstract void activate();
    public char getType()
    {
        return type;
    }
    public Transform getMinionTakingAction()
    {
        return minionTakingAction;
    }
}

public class MovementTaken : ActionTaken
{
    public int targetTileH;
    public int targetTileV;

    public override void activate()
    {
        minionTakingAction.GetComponentInParent<MoveMinion>().activateMovement(this);
    }

    public MovementTaken(Transform _minionTakingAction, int _targetTileH, int _targetTileV)
    {
        type = 'M';
        minionTakingAction = _minionTakingAction;
        targetTileH = _targetTileH;
        targetTileV = _targetTileV;
    }
}

public class AttackTaken : ActionTaken
{
    public Transform targetMinion;

    public override void activate()
    {
        minionTakingAction.GetComponentInParent<AttackScript>().activateAttack(targetMinion);
    }

    public AttackTaken(Transform _minionTakingAction, Transform _targetMinion)
    {
        type = 'A';
        minionTakingAction = _minionTakingAction;
        targetMinion = _targetMinion;
    }
}

public class AbilityTaken : ActionTaken
{
    public int index;
    Queue<Transform> selectedMinions;
    Queue<Transform> selectedTiles;
    public override void activate()
    {
        if (selectedMinions.Count == 0 && selectedTiles.Count == 0)
        {
            minionTakingAction.GetComponentInParent<AbilitiesActivation>().activateAbility(index);
        }
        else
        {
            minionTakingAction.GetComponentInParent<AbilitiesActivation>().activateAbility(index, selectedMinions, selectedTiles);
        }
    }

    public void addSelectedTile(Transform tile) //be carefull this should only be tile not minion
    {
        selectedTiles.Enqueue(tile);
    }
    public void addSelectedMinion(Transform minion) //be carefull this should only be minion not tile
    {
        selectedMinions.Enqueue(minion);
    }

    public AbilityTaken(Transform _minionTakingAction, int abilityIndex, Queue<Transform> _selectedMinions, Queue<Transform> _selectedTiles)
    {
        type = 'S'; //for spell
        minionTakingAction = _minionTakingAction;
        index = abilityIndex;
        selectedMinions = _selectedMinions;
        selectedTiles = _selectedTiles;
    }
    public AbilityTaken(Transform _minionTakingAction, int abilityIndex)
    {
        type = 'S'; //for spell
        minionTakingAction = _minionTakingAction;
        index = abilityIndex;
        selectedMinions = new Queue<Transform>();
        selectedTiles = new Queue<Transform>();
    }
}


