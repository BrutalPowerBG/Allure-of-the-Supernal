using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {
    BoardGrid grid;
    GameMaster gameMaster;
    Stats stats;
    Stats otherMinionStats;
    private int attackPassiveTriggerAbility = 0;
    private int collectedCrit;
    private bool trueAttack = false;
	// Use this for initialization
	void Start () {
        grid = GameObject.FindWithTag("Grid").GetComponent<BoardGrid>();
        gameMaster = grid.GetComponentInParent<GameMaster>();
        stats = GetComponent<Stats>();
        collectedCrit = stats.crt;
    }
	
    public void activateAttack(Transform target)
    {
        otherMinionStats = target.GetComponent<Stats>();

        gameMaster.setIfActionInProgress(true);

        if (otherMinionStats.invulnerability==true)
        {
            Debug.Log(transform + "Cannot attack invulnerable minions.");
            return;
        }

        int verticalDifference = stats.vertical - otherMinionStats.vertical;
        int horizontalDifference = stats.horizontal - otherMinionStats.horizontal;

        if (verticalDifference < 0)
        {
            verticalDifference = -verticalDifference;
        }
        if (horizontalDifference < 0)
        {
            horizontalDifference = -horizontalDifference;
        }

        if (stats.att_range >= verticalDifference
         && stats.att_range >= horizontalDifference)
        {
            Attack(target);
        }
        else
        {
            Debug.Log("Target Out of range");
        }
    }

    //newly private
	private void Attack(Transform target)
    {
        if (attackPassiveTriggerAbility > 0)
        {
            //ActivateAbility(int numberOfAbility)
        }
        int verticalDifference = stats.vertical - otherMinionStats.vertical;
        int horizontalDifference = stats.horizontal - otherMinionStats.horizontal;
        if (horizontalDifference < 0)
        {
            horizontalDifference = -horizontalDifference;
        }
        if (verticalDifference < 0)
        {
            verticalDifference = -verticalDifference;
        }

        if (stats.att_range < verticalDifference ||
            stats.att_range < horizontalDifference)
        {
            Debug.Log("Not in range");
        }

        int attackValue=0;
        int critGamble = Random.Range(1, 100);
        if (critGamble <= collectedCrit)
        {
            if (trueAttack)
            {
                attackValue = stats.ad * 2;
            }
            else
            {
                attackValue = stats.ad * 2 - otherMinionStats.armor;
            }
            collectedCrit = stats.crt;
        }
        else
        {
            if (trueAttack)
            {
                attackValue = stats.ad;
            }
            else
            {
                attackValue = stats.ad - otherMinionStats.armor;
            }
            collectedCrit += stats.crt;
        }
        if (attackValue > 0)
        {
            otherMinionStats.hp -= attackValue;
        }
        else
        {
            Debug.Log("attackValue below zero!");
        }

        if (otherMinionStats.hp<1)
        {
            stats.incrementEnemiesKilled();
            Destroy(target.gameObject);
        }
        gameMaster.dequeueAction(); 
        gameMaster.setIfActionInProgress(false);
    }
}


