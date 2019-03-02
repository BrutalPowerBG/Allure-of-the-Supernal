
using UnityEngine;

[ExecuteInEditMode]

public class Grid : MonoBehaviour {
    public Transform tilePrefab;
    public Transform[,] tile;
    public GameObject[] minions;
    public int numberOfRowsAndColumns=1;
    public float sizeOfTiles;
    public float distance = 1f;
    public Vector3 tilePosition = new Vector3 ( 0f, 1f, 0f);
    public Quaternion tileRotation = new Quaternion(0f, 0f, 0f, 0f);
    //public GameObject[] tiles;
	// Use this for initialization
	void Awake () {
        sizeOfTiles = tilePrefab.transform.localScale.x;
           
       
        tile = new Transform[numberOfRowsAndColumns, numberOfRowsAndColumns];
       
        // tilePosition.Scale(new Vector3(4 * sizeOfTiles, 1f, 4 * sizeOfTiles));
        for (int i = 0; i < numberOfRowsAndColumns; i++)
        {
             for (int j = 0; j < numberOfRowsAndColumns; j++)
             {
                    if (transform.Find("tile[" + i + ", " + j + "]"))
                    {
                        tile[i, j] = transform.Find("tile[" + i + ", " + j + "]").transform;
                    }  
                    if (tile[i,j] == null)
                    {
                        tilePosition.x = i * ( sizeOfTiles + distance);
                        tilePosition.z = j * ( sizeOfTiles + distance);
                        tile[i, j] = Instantiate(tilePrefab, tilePosition, tileRotation);
                        tile[i, j].transform.parent = gameObject.transform; //FIXXXX
                        tile[i, j].name = ("tile[" + i + ", " + j + "]");
                        tile[i, j].tag = "Tile";
                        tile[i, j].GetComponent<SelectMovement>().horizontal=i;
                        tile[i, j].GetComponent<SelectMovement>().vertical = j;
                    } 
             }
        }

        
    }
    
    void Start()
    {
        refreshMinions();
        minionsOnAllTiles();
    }

    public void minionsOnAllTiles()
    {
       refreshMinions();
       foreach (Transform currentTile in tile)
       {
           currentTile.GetComponent<TileHover>().minionOnTile();
       }
    }
    public void refreshMinions()
    {
        minions = GameObject.FindGameObjectsWithTag("Minion");
        foreach(GameObject minion in minions)
        {
            print(minion.name);
        }
    }
}
