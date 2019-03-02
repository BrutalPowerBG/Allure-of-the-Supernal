using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesActivation : MonoBehaviour {
    GameMaster gameMaster;
    BoardGrid grid;

    public Ability[] ability;
    Stats stats;


    // Use this for initialization
    void Start () {
        gameMaster = GameObject.FindWithTag("GameController").GetComponent<GameMaster>();
        grid = GameObject.FindWithTag("Grid").GetComponent<BoardGrid>();
        stats = GetComponent<Stats>();
    }
    public void initializeAbility(int abilityIndex)
    {
        if (abilityIndex >= ability.Length || abilityIndex < 0)
        {
            Debug.Log("ERROR: abilityIndex out of range");
            return;
        }

        if (ability[abilityIndex].enemyMinionSelectionRequirement > 0 ||
            ability[abilityIndex].friendlyMinionSelectionRequirement > 0 ||
            ability[abilityIndex].tileSelectionRequirement > 0)
        {
            gameMaster.abilitySelected = abilityIndex;
            //add highlights and start selecting
        }
        else
        {
            ActionTaken abilityTaken = new AbilityTaken(transform, abilityIndex);
            gameMaster.enqueueAction(abilityTaken);
        }
    }

    public void highlightEnemyMinions(int ability, bool highlighted=true)
    {       
        // TODO two nested for cycles checking if all minions on board are within range, etc.

    }

    private void highlightEnemyMinion()
    {
        
    }
    public void highlightFriendlyMinions(int ability, bool highlighted = true)
    {
        // TODO two nested for cycles checking if all minions on board are within range, etc. 
        grid.abilityHighlight[stats.horizontal, stats.vertical].gameObject.SetActive(highlighted);
    }

    public void highlightFriendlyMinion()
    {
        //TODO
    }


    public void activateAbility(int abilityIndex)
    {
        grid.deselectMinion();
        abilityIndex--;
        if (abilityIndex>=ability.Length || abilityIndex<0)
        {
            Debug.Log("ERROR: abilityIndex out of range");
            return;
        }

        //NEW
        highlightEnemyMinions(abilityIndex, false);
        highlightFriendlyMinions(abilityIndex, false);

        if (ability[abilityIndex].isPassive)
        {
            return;
        }
        if (ability[abilityIndex].hasInstantEffect)
        {
            updateCD(abilityIndex);
            updateEnergy(abilityIndex);

            //performs healing if it is a part of the effect
            performHealing(abilityIndex);

            if (ability[abilityIndex].grabEnemy)
            {

            }
        }
    }

    public void activateAbility(int abilityIndex, Queue<Transform> minions, Queue<Transform> selectedTiles)
    {
        grid.deselectMinion();
        abilityIndex--;
        if (abilityIndex >= ability.Length || abilityIndex < 0)
        {
            Debug.Log("ERROR: abilityIndex out of range");
            return;
        }

        //NEW
        highlightEnemyMinions(abilityIndex, false);
        highlightFriendlyMinions(abilityIndex, false);

        if (ability[abilityIndex].isPassive)
        {
            return;
        }
        updateCD(abilityIndex);
        updateEnergy(abilityIndex);
    }

    private void updateCD(int abilityIndex)
    {
        ability[abilityIndex].currentCD += ability[abilityIndex].cd;
    }
    private void updateEnergy(int abilityIndex)
    {
        if (ability[abilityIndex].unorthodoxCost == 0)
        {
            gameMaster.decreaseEnergy(stats.tier, stats.playerAllegiance);
        }
        else if (ability[abilityIndex].unorthodoxCost > 0)
        {
            gameMaster.decreaseEnergy(ability[abilityIndex].unorthodoxCost, stats.playerAllegiance);
        }
    }
    private void performHealing(int abilityIndex)
    {
        if (ability[abilityIndex].heals)
        {
            if (ability[abilityIndex].healsOnlySelf)
            {
                int sum = stats.hp + ability[abilityIndex].healAmount;
                if (sum > stats.max_hp)
                {
                    stats.hp = stats.max_hp;
                }
                else
                {
                    stats.hp = sum;
                }
            }
        }
    }
}
