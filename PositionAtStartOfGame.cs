using UnityEngine;
//[ExecuteInEditMode]
public class PositionAtStartOfGame : MonoBehaviour {
    public int posH=1;
    public int posV=0;
    BoardGrid grid;
    GameMaster gameMaster;
    Stats stats;
    private float sizeOfTiles, distance, scaleTiles;
    private Vector3 offset; 
    // Use this for initialization
    void Start () {
        grid = GameObject.FindWithTag("Grid").GetComponent<BoardGrid>();
        offset = grid.transform.position;
        posH %= grid.amountRowsAndColumns;
        posV %= grid.amountRowsAndColumns;


        while ( grid.tile[posH, posV].GetComponent<TileScript>().isOccupied() == true 
            && posV < grid.amountRowsAndColumns - 1)
        {
            while (grid.tile[posH, posV].GetComponent<TileScript>().isOccupied() == true 
                && posH < grid.amountRowsAndColumns - 1)
            {
                posH++;
            }
            if (grid.tile[posH, posV].GetComponent<TileScript>().isOccupied() == true)
                posV++;
        }
        sizeOfTiles = grid.getSizeOfTiles();
        scaleTiles = grid.scaleTiles;
        distance = grid.gapBetweenTiles;

        //IF ALL TILES AREN'T NULL!
        if ( grid.tile[posH, posV].GetComponent<TileScript>().isOccupied())///NOT NULL!
        {
            Debug.Log("ERROR: No available tiles");
            Destroy(gameObject);
        }
        else
        {
            transform.position = new Vector3(posH * (sizeOfTiles*scaleTiles + distance) + offset.x, 0f, posV*(sizeOfTiles * scaleTiles + distance) + offset.z);
            grid.tile[posH, posV].GetComponent<TileScript>().minionOnTile = transform;
        }
        stats = GetComponent<Stats>();
        stats.InitializePosition(posH,posV);
    }
}
