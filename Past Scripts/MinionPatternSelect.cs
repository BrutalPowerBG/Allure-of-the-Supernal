using UnityEngine;

public class MinionPatternSelect : MonoBehaviour
{

    private int horizontal;
    private int vertical;

    public Color availableTileColor;
    public Color startingTileColor;

    private Move moveScript;
    private Grid gridScript;
    private Stats statsOfMinion;
    //    private TileHover tileHoverScript;

    public bool isUnitSelected = false;

    void Start()
    {
        statsOfMinion = GetComponent<Stats>();
        moveScript = GetComponent<Move>();
        gridScript = GameObject.FindWithTag("Grid").GetComponent<Grid>();
        startingTileColor = GetComponent<TileOn>().tileUnderMinion.GetComponent<TileHover>().startColor;
    }

    public void OnMouseDown()
    {
        horizontal = moveScript.startHorizontal;
        vertical = moveScript.startVertical;

        if (isUnitSelected == false)
        {
            isUnitSelected = true;
            for (int i = 0; i < gridScript.numberOfRowsAndColumns; i++)
            {
                for (int j = 0; j < gridScript.numberOfRowsAndColumns; j++)
                {
                    gridScript.tile[i, j].GetComponent<TileHover>().rend.material.color = startingTileColor;
                }
            }
            for (int i = horizontal - 5; i < horizontal + 5 && i <= gridScript.numberOfRowsAndColumns; i++)// !!! opraven error, out of scope [i>10, j>10]
            {
                if (i < 0)
                    i = 0;
                for (int j = vertical - 5; j < vertical + 5 && j <= gridScript.numberOfRowsAndColumns; j++) // !!! opraven error, out of scope [i>10, j>10]
                {

                    if (j < 0)
                        j = 0;

                    if (statsOfMinion.pattern[i - horizontal + 5, j - vertical + 5])
                    {
                        gridScript.tile[i, j].GetComponent<TileHover>().minionPatternDisplayed = true;
                        gridScript.tile[i, j].GetComponent<TileHover>().rend.material.color = availableTileColor;
                    }
                }
            }
        }
        else
        {
            deselectMinion();
        }
        //tileHoverScript.minionPatternDisplay = true;
    }


    public void deselectMinion()
    {
            isUnitSelected = false;
            for (int i = 0; i < gridScript.numberOfRowsAndColumns; i++)
            {
                for (int j = 0; j < gridScript.numberOfRowsAndColumns; j++)
                {
                    gridScript.tile[i, j].GetComponent<TileHover>().minionPatternDisplayed = false;
                    gridScript.tile[i, j].GetComponent<TileHover>().rend.material.color = startingTileColor;
                }
            }
    }
}
