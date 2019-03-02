using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileClick : MonoBehaviour {
    GameMaster gameMaster;
    BoardGrid grid;
    TileScript tileScript;
    Transform minionOnTile;
    int horizontal, vertical;
    void Start()
    {
        gameMaster = GameObject.FindWithTag("GameController").GetComponent<GameMaster>();
        grid = GameObject.FindWithTag("Grid").GetComponent<BoardGrid>();
        tileScript = GetComponentInParent<TileScript>();
        horizontal = tileScript.horizontal;
        vertical = tileScript.vertical;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (gameMaster.abilitySelected == 0) // No ability selected
        {
            if (tileScript.minionOnTile == null && gameMaster.selectionExists())
            {
            
                Stats minionStats = gameMaster.getSelected().GetComponent<Stats>();
                int hPattern = horizontal - minionStats.horizontal + minionStats.patternDisplacement;
                int vPattern = vertical - minionStats.vertical + minionStats.patternDisplacement;

                if (hPattern < grid.amountRowsAndColumns && vPattern < grid.amountRowsAndColumns
                    && hPattern >= 0 && vPattern >= 0 && minionStats.pattern[hPattern, vPattern])
                {
                    grid.tile[minionStats.horizontal, minionStats.vertical].GetComponent<TileScript>().minionOnTile = null; // ? before movement completed?
                    MovementTaken movement = new MovementTaken(gameMaster.getSelected(), horizontal, vertical);
                    gameMaster.enqueueAction(movement);
                }
                else
                {
                    grid.deselectMinion();
                }
            }
        }
        else
        {
            // TODO: add tile/minion to selected for ability
        }
    }

}
