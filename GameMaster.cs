using UnityEngine;
using System.Collections.Generic;
public class GameMaster : MonoBehaviour {
    Queue<ActionTaken> actionsTaken = new Queue<ActionTaken>();
    bool actionInProgress = false;
    Transform selected = null;
    public int teamCount = 2;
    Transform[,] minionsByTeam;
    BoardGrid grid;
    Transform[] deadMinions;
    Minions minionsScript;

    public int abilitySelected = 0;
    public int tilesSelected = 0;
    public int friendlyMinionsSelected = 0;
    public int enemyMinionsSelected = 0;


    int turnNumber = 1;
    int[] energyByTeam;

    public void decreaseEnergy(int energy, int team)
    {
        energyByTeam[team] -= energy;
    }

    public void activateAbility(int index)
    {
        if (selectionExists())
        {
            selected.GetComponent<AbilitiesActivation>().activateAbility(index);
        }
    }
    public void abilityDeselect()
    {
        abilitySelected = 0;
        friendlyMinionsSelected=0;
        enemyMinionsSelected=0;
        tilesSelected = 0;
        for (int i=0; i<grid.amountRowsAndColumns;i++)
        {
            for (int j = 0; j < grid.amountRowsAndColumns; j++)
            {
                grid.tile[i, j].GetComponent<AbilitySelection>().setAbilityHighlighted(false);
            }
        }
    }
    public void enqueueAction(ActionTaken toAdd)
    {
        actionsTaken.Enqueue(toAdd);
    }
    public void initializeAbility(int index)
    {
        if (selectionExists())
        {
            selected.GetComponent<AbilitiesActivation>().initializeAbility(index);
        }
    }
    public void dequeueAction()
    {
        actionsTaken.Dequeue();
    }
    public void selectUnit(Transform unit)
    {
        selected = unit;
    }
    public Transform getSelected()
    {
        return selected;
    }
    public void deselect()
    {
        selected = null;
    }
    public bool selectionExists()
    {
        if (selected == null)
            return false;
        return true;
    }
    public bool getActionInProgress()
    {
        return actionInProgress;
    }
    public List<Transform> getAdjacentMinions(Stats toMinion, int range=1)
    {
        List<Transform> adjacentMinions= new List<Transform>();
        for (int i = toMinion.horizontal - range; i < toMinion.horizontal + range; i++)
        {
            for (int j = toMinion.vertical - range; j < toMinion.vertical + range; j++)
            {
                if (legitPosition(i, j))
                {
                    TileScript tile = grid.tile[i, j].GetComponent<TileScript>();
                    if (tile.isOccupied())
                    {
                        adjacentMinions.Add(tile.minionOnTile);
                    }
                }
            }
        }
        return adjacentMinions;
    }
    public bool legitPosition(int horizontal, int vertical)
    {
        if (horizontal < 0 || vertical < 0)
            return false;
        if (grid.amountRowsAndColumns < horizontal || grid.amountRowsAndColumns < vertical)
            return false;

        return true;
    }
    public void setIfActionInProgress(bool inProgress)
    {
        actionInProgress = inProgress;
    }
    private void LateUpdate()
    {
        if (!actionInProgress &&
            actionsTaken.Count>0)
        {
            ActionTaken currentAction = actionsTaken.Peek();
            currentAction.activate();
        }
    }

    void Start()
    {

        // ALLOCATION

        grid = GameObject.FindWithTag("Grid").GetComponent<BoardGrid>();
        minionsScript = transform.Find("Minions").GetComponent<Minions>();

        deadMinions = new Transform[100];
        List<Transform> minions = minionsScript.getMinions();
        deadMinions.GetLength(0);

        minionsByTeam = new Transform[teamCount,100];
        int[] indexPerTeam= new int[teamCount];
        for (int i = 0; i<teamCount;i++)
        {
            indexPerTeam[i] = 0;
        }
        int allMinionsCount=0;
        foreach (Transform child in minions)
        {
            Stats stats = child.GetComponent<Stats>();
            indexPerTeam[stats.playerAllegiance]++;
            minionsByTeam[stats.playerAllegiance, allMinionsCount] = child;
            allMinionsCount++;
        }
        turnNumber = 1;

        energyByTeam = new int[teamCount];
        energyByTeam[0] = 1;
        for (int i = 1; i < teamCount; i++)
        {
            energyByTeam[i] = 2;
        }
        //END OF ALLOCATION

        //CleanupTiles();
    }


    void CleanupTiles() //administrative function, not to be used normally
    {
        for (int i=0; i < grid.amountRowsAndColumns; i++)
        {
            for (int j = 0; j < grid.amountRowsAndColumns; j++)
            {
                foreach (Transform tile in grid.transform)
                {
                     while (tile.childCount != 0)
                     {
                         DestroyImmediate(tile.GetChild(0).gameObject);
                     }
                }
            }
        }
    }
}
