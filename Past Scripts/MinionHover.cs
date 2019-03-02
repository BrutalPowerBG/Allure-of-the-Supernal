/*using UnityEngine;

public class MinionHover : MonoBehaviour {

    private Renderer rendMinion;
    private Renderer rendTile;

    private Color startTileColor;
    private Color startMinionColor;
    private Color hoverColor;

    public GameObject minion;
    public GameObject tile;
    public Grid grid;

    public MinionPatternSelect minionPatternSelectScript;
    private TileHover tileHoverScript;
    private TileOn tileOnScript;
    
    void Start ()
    {
        grid = FindObjectOfType<Grid>();
        tileOnScript = GetComponent<TileOn>();
        tileHoverScript = tileOnScript.tileUnderMinion.GetComponent<TileHover>();
        rendTile = tileOnScript.tileUnderMinion.GetComponent<Renderer>();
        //
        rendMinion = GetComponent<Renderer>();

        startTileColor =rendTile.material.color;
        minionPatternSelectScript = GetComponent<MinionPatternSelect>();

        startMinionColor = rendMinion.material.color;
        hoverColor = tileHoverScript.hoverColor;
    }
    void Update ()
    {
        tileOnScript = GetComponent<TileOn>();
        tileHoverScript = tileOnScript.tileUnderMinion.GetComponent<TileHover>(); // change this, its only a temporary solution
        rendTile = tileOnScript.tileUnderMinion.GetComponent<Renderer>();
    }
    void OnMouseEnter()
    {
        if (!tileHoverScript.minionPatternDisplayed && minionPatternSelectScript.isUnitSelected == false)
        {
            rendTile = tileOnScript.tileUnderMinion.GetComponent<Renderer>();
            rendTile.material.color = hoverColor;
            rendMinion.material.color = hoverColor;
        }
    }
    void OnMouseExit()
    {
        if (!tileHoverScript.minionPatternDisplayed && minionPatternSelectScript.isUnitSelected == false)
        {
            rendTile.material.color = startTileColor;
            rendMinion.material.color = startMinionColor;
        }
    }

}
*/