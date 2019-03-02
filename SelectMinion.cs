using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectMinion : MonoBehaviour
{
    private BoardGrid grid;
    private GameMaster gameMaster;
    private Stats stats;
    private Transform portraitCamera;
    private AbilityButtons abilityButtonsScript;
    private AbilitiesActivation abilitiesActivation;

    private void Start()
    {
        grid = GameObject.FindWithTag("Grid").GetComponent<BoardGrid>();
        gameMaster = grid.GetComponentInParent<GameMaster>();
        stats = GetComponent<Stats>();
        abilitiesActivation = GetComponent<AbilitiesActivation>();
        portraitCamera = GameObject.FindWithTag("PortraitCamera").GetComponent<Transform>();
        portraitCamera.localPosition.Set(0f, 2.15f, 0.4f);
        abilityButtonsScript = GameObject.FindWithTag("AbilityButtons").GetComponent<AbilityButtons>();
    }
    public void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!gameMaster.selectionExists())
        {
            grid.deselectMinion();
            gameMaster.selectUnit(transform);

            portraitCamera.parent = transform;
            portraitCamera.position = transform.position + stats.portraitCameraDisplacement;

            abilityButtonsScript.changeButtons(abilitiesActivation);

            for (int i = stats.horizontal - 5; i < stats.horizontal + 5 && i <= grid.amountRowsAndColumns; i++)// !!! opraven error, out of scope [i>10, j>10]
            {
                if (i < 0)
                    i = 0;
                for (int j = stats.vertical - 5; j < stats.vertical + 5 && j <= grid.amountRowsAndColumns; j++) // !!! opraven error, out of scope [i>10, j>10]
                {

                    if (j < 0)
                        j = 0;

                    if (stats.pattern[i - stats.horizontal + 5, j - stats.vertical + 5])
                    {
                        TileScript tileScript = grid.tile[i, j].GetComponent<TileScript>();
                        if (tileScript.minionOnTile == null)
                        {
                            grid.movementHighlight[i, j].gameObject.SetActive(true);
                            tileScript.movementHighlighted = true;
                        }
                        else
                        {
                            if (transform == tileScript.minionOnTile)
                            {
                                grid.attackHighlight[i, j].gameObject.SetActive(true);
                                //WhiteLight for self selection
                                Light light = grid.attackHighlight[i, j].gameObject.GetComponent<Light>();
                                light.color = grid.selectedMinionLightColor;
                            }
                            else
                            {
                                Transform selectedMinion = transform;
                                Stats selectedMinionStats = selectedMinion.GetComponent<Stats>();
                                Transform otherMinion = gameMaster.getSelected();
                                Stats otherMinionStats = grid.tile[i, j].GetComponent<TileScript>().minionOnTile.GetComponent<Stats>();
                                if (otherMinionStats.playerAllegiance != selectedMinionStats.playerAllegiance)
                                {
                                    grid.attackHighlight[i, j].gameObject.SetActive(true);
                                    tileScript.attackHighlighted = true;
                                }
                            }
                        }
                    } // TODO: Fix attack highlight, should be triggered when in ATR, not when enemy minion is on the movement pattern
                }
            }
        }
        else
        {
            Transform clickedMinion = transform;
            Stats clickedMinionStats = clickedMinion.GetComponent<Stats>();
            Transform minionTakingAction = gameMaster.getSelected();
            Stats minionTakingActionStats = minionTakingAction.GetComponent<Stats>();
            //AttackScript attackScript = minionTakingAction.GetComponent<AttackScript>();

            if (clickedMinion != minionTakingAction)
            {
                if (minionTakingActionStats.playerAllegiance != clickedMinionStats.playerAllegiance)
                {
                    //Select Enemy Minions for ability
                    if (gameMaster.abilitySelected > 0)
                    {
                        Transform abilityHighlight = grid.abilityHighlight[stats.horizontal, stats.vertical];
                        if (gameMaster.enemyMinionsSelected >=
                           abilitiesActivation.ability[gameMaster.abilitySelected].enemyMinionSelectionRequirement)
                        {
                            AbilityTaken abilityTaken = new AbilityTaken(GetComponent<Transform>(), gameMaster.abilitySelected);//, gameMaster.enemyMinionsSelected);
                            gameMaster.enqueueAction(abilityTaken);
                            gameMaster.abilityDeselect();
                            gameMaster.deselect();
                        }
                        else if (gameMaster.enemyMinionsSelected <
                           abilitiesActivation.ability[gameMaster.abilitySelected].friendlyMinionSelectionRequirement)
                        {
                            TileScript tileBelow = grid.tile[stats.horizontal, stats.vertical].GetComponent<TileScript>();
                            if (tileBelow.abilityHighlighted == true)
                            {
                                grid.abilityHighlight[stats.horizontal, stats.vertical].gameObject.SetActive(false);
                                tileBelow.abilityHighlighted = false;
                                gameMaster.enemyMinionsSelected--;
                            }
                            else if (tileBelow.abilityHighlighted == false)
                            {
                                grid.abilityHighlight[stats.horizontal, stats.vertical].gameObject.SetActive(true);
                                tileBelow.abilityHighlighted = true;
                                gameMaster.enemyMinionsSelected++;
                            }
                        }
                    }
                    // No ability selected - ATTACK ENEMY!
                    else
                    {
                        AttackTaken attack = new AttackTaken(minionTakingAction, clickedMinion);
                        gameMaster.enqueueAction(attack);
                    }
                }
                
                grid.deselectMinion();
            }
            if (minionTakingActionStats.playerAllegiance == clickedMinionStats.playerAllegiance)
            {
                    //TODO: friendly minion selection
            }
            //tileHoverScript.minionPatternDisplay = true;
        }
    }
}
