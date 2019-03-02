using UnityEngine;
[ExecuteInEditMode]
public class TileOn : MonoBehaviour {

    protected GameObject[] tiles;
    public GameObject tileUnderMinion;
    public float minDist;
    public int l;

    // Use this for initialization
    void Start()
    {
        //if (tiles == null)
            tiles = GameObject.FindGameObjectsWithTag("Tile");

          l = tiles.Length;
           tileUnderMinion = null;
        minDist = Mathf.Infinity;
        GetClosestTile();
    }


    /*void LateUpdate () {
        GetClosestTile(); //Added recently
    }*/
    public GameObject  GetClosestTile()
    {
        Vector3 currentMinionPos = transform.position;
        //print(tiles[0]);//test

        foreach (GameObject t in tiles)
        {
            float dist = Vector3.Distance(t.transform.position, currentMinionPos);
            if (dist < minDist)
            {
                tileUnderMinion = t;
                minDist = dist;
            }
        }
        return tileUnderMinion;
    }
 
}
