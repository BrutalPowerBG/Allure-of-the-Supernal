using UnityEngine;
//using System.Collections;


public class Move : MonoBehaviour {
    public Grid gridScript;
   // private TileHover tileHoverScript;
    private TileOn tileOnScript;

    private float moveSpeed = 2F;
    private float distanceBetweenTiles;
    private float approximation = 0.05F;
    //private bool move = false;
    // private int verticalMove;
    // private int horizontalMove;

    public int startVertical; // these are the real coordinates, find where the imposter is and replace him
    public int startHorizontal;
    Vector3 targetPosition;


    void Start ()
    {
        gridScript = GameObject.FindWithTag("Grid").GetComponent<Grid>();
        tileOnScript = GetComponent<TileOn>();
        distanceBetweenTiles = gridScript.distance + gridScript.sizeOfTiles;
        startHorizontal = (int)(tileOnScript.tileUnderMinion.transform.position.x/ (gridScript.sizeOfTiles + gridScript.distance));
        startVertical = (int)(tileOnScript.tileUnderMinion.transform.position.z / (gridScript.sizeOfTiles + gridScript.distance));
        targetPosition = transform.position;
        //movementControl(3, 5);
    }
    /*
    void Update()
    {
        if (move)
            physicalMovement(horizontalMove, verticalMove);
    }
    */
    /*void physicalMovement(int h, int v)
    {
        // horizontalMove = (int)(tileScript.tileUnderMinion.transform.position.x / 4.5f);
        //verticalMove = (int)(tileScript.tileUnderMinion.transform.position.z / 4.5f);
        transform.position += new Vector3(h, 0, v) * Time.deltaTime*moveSpeed; //** (gridScript.sizeOfTiles + gridScript.distance)
        if (transformIsWithin(new Vector3 (h+startHorizontal,0, v+startVertical)))//tileScript.tileUnderMinion.name==("tile["+(startHorizontal+h)+", "+ (startVertical + v) +"]")
        {
            move = false;
            transform.position = new Vector3(startHorizontal+h , 0, startVertical+v) * (gridScript.sizeOfTiles + gridScript.distance);
            startHorizontal = startHorizontal + h;
            startVertical = startVertical + v;
            gridScript.minionsOnAllTiles();
           
            tileOnScript.GetClosestTile();
            //tileHoverScript = GetComponent<TileOn>().tileUnderMinion.GetComponent<TileHover>();
        }
      // Debug.DrawRay(transform.position,);
    }
    bool transformIsWithin( Vector3 positionCheck,Transform unit=null, float approximation=0.2f)
    {
        if (unit == null)
            unit = transform;
        if (unit.position.x >= (positionCheck.x - approximation) * (gridScript.sizeOfTiles + gridScript.distance) &&
            unit.position.x <= (positionCheck.x+ approximation) * (gridScript.sizeOfTiles + gridScript.distance) &&
            unit.position.z <= (positionCheck.z + approximation) * (gridScript.sizeOfTiles + gridScript.distance) &&
            unit.position.z >= (positionCheck.z - approximation) * (gridScript.sizeOfTiles + gridScript.distance))
        {
            //tileHoverScript.minionOnTile();
            return true;
        }
        return false;
    }*/
    public void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > approximation)//approximation is optional, 0 is also viable
        {
            transform.position += (targetPosition - transform.position) * (0.01F * moveSpeed) ;
        }
    }
    public void movementControl(int h, int v) {
        targetPosition = transform.position + ((new Vector3(h, 0, v)) * distanceBetweenTiles);
        if (Vector3.Distance(transform.position, targetPosition) > 0) // if we are more than 0 units away
        {
            startHorizontal = startHorizontal + h;
            startVertical = startVertical + v;
            gridScript.minionsOnAllTiles();
            tileOnScript.GetClosestTile();
        }
         
       /*move = true;
        if (startHorizontal + h <= 10 && startVertical + v <= 10 && startHorizontal + h >= 0 && startVertical + v >= 0)
        {
            verticalMove = v;
            horizontalMove = h;
        }
        else
        {
            verticalMove = 0;
            horizontalMove = 0;
        }*/
        // horizontalMove = (int)(tileScript.tileUnderMinion.transform.position.x / 4.5f);
        //verticalMove = (int)(tileScript.tileUnderMinion.transform.position.z / 4.5f);
    }
    

}
