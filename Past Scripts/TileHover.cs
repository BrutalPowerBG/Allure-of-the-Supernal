using UnityEngine;

public class TileHover : MonoBehaviour
{
    public Color hoverColor;
    public Color startColor;

    public bool thereIsMinion;
    public bool minionPatternDisplayed = false;
    public Grid gridScript;
    public GameObject onTile;

    public Renderer rend;


    void Awake()
    {
        rend = GetComponent<Renderer>();
       // print(transform.parent);
        gridScript = transform.parent.gameObject.GetComponent<Grid>();
        startColor = rend.material.color;
       // gridScript.minionsOnAllTiles();
       // minionOnTile();
    }


    public GameObject minionOnTile() //thru da force i sense that something's not right 
    {
        onTile = null;
        thereIsMinion = false;
        //print(minions.Length);
        for (int i=0; i<gridScript.minions.Length; i++)
        {
            GameObject tileUnderMinion = gridScript.minions[i].GetComponent<TileOn>().GetClosestTile(); 

            if (tileUnderMinion.transform==transform)
            {
                thereIsMinion = true;
                onTile = gridScript.minions[i];//!!!
                print(gridScript.minions[i]);
                break;
            }
        }
        return onTile;
    }
       
    void OnMouseEnter()
    {
        if ((!minionPatternDisplayed && onTile == null) || (onTile!=null && onTile.GetComponent<MinionPatternSelect>().isUnitSelected == false))
        rend.material.color = hoverColor;
    }
    void OnMouseExit()
    {
        if ((!minionPatternDisplayed && onTile == null) || (onTile!=null && onTile.GetComponent<MinionPatternSelect>().isUnitSelected == false))
            rend.material.color = startColor;
    }

}
