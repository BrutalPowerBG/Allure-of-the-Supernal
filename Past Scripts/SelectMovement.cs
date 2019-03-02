using UnityEngine;

public class SelectMovement : MonoBehaviour {

    private MinionPatternSelect minionPatternSelectScript;
    private TileHover tileHoverScript;
    private Stats statsScript;
    private Move moveScript;
    private Grid gridScript;

    public int horizontal, vertical;


    private GameObject[] minions;
    private GameObject selectedMinion=null;
    void Start()
    {
        moveScript = GetComponent<Move>();
        tileHoverScript = GetComponent<TileHover>();
        gridScript = GameObject.FindWithTag("Grid").GetComponent<Grid>();
    }

    void OnMouseDown()
    {
        if (tileHoverScript.minionOnTile() != null)
        {
            tileHoverScript.onTile.GetComponent<MinionPatternSelect>().OnMouseDown();
            return;
        }
        minions = GameObject.FindGameObjectsWithTag("Minion"); // can microoptimize it with 2d array from gridScript
        for (int i = 0; i < minions.Length; i++)
        {
           minionPatternSelectScript = minions[i].GetComponent<MinionPatternSelect>();
            if (minionPatternSelectScript.isUnitSelected == true)
            {
                selectedMinion = minions[i];
               // print(minions[i].name);
            }
        }
        if(selectedMinion!=null)
        {
            statsScript = selectedMinion.GetComponent<Stats>();
            moveScript = selectedMinion.GetComponent<Move>();
            int horDistance = horizontal - selectedMinion.GetComponent<TileOn>().tileUnderMinion.GetComponent<SelectMovement>().horizontal;
            int vertDistance = vertical - selectedMinion.GetComponent<TileOn>().tileUnderMinion.GetComponent<SelectMovement>().vertical;

            if (horDistance<=5 && vertDistance<=5 
            && horDistance >= -5 && vertDistance >= -5
            && gridScript.tile[horizontal, vertical].GetComponent<TileHover>().minionPatternDisplayed == true && statsScript.pattern[horDistance+5, vertDistance+5])/// promenlivi na TILE horiz i vertical
            {
                moveScript.movementControl(horDistance,vertDistance);
            }
            else
            {
                GameObject[] minions;
                minions= GameObject.FindGameObjectsWithTag("Minion");
                for (int i=0; i<minions.Length; i++)
                    minions[i].GetComponent<MinionPatternSelect>().deselectMinion();
            }
            selectedMinion = null; //recently added, pay attention
        }
    }
}
