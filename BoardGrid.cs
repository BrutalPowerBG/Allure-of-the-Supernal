using UnityEngine;
[ExecuteInEditMode]
public class BoardGrid : MonoBehaviour {
    GameMaster gameMaster;

    public Transform tilePrefab;
    public Transform tilePrefab2;
    public Transform attackHighlightPrefab;
    public Transform movementHighlightPrefab; //nz dali tr TRANSFORM, LIGHT ili GameObject EDIT mai tr transform :D zasega bachka 04.06.2018
    public Transform abilityHighlightPrefab;
    public Transform[,] attackHighlight;
    public Transform[,] movementHighlight;
    public Transform[,] abilityHighlight;
    public Transform[,] tile;


    public int amountRowsAndColumns = 10;
    private float sizeOfTiles = 4f;
    public float scaleTiles = 1f;
    public float gapBetweenTiles = 1f;
    public float movementHighlightHeight = 0.75f;
    public float attackHighlightHeight = 1.0f;
    public float abilityHighlightHeight = 1.0f;

    public Vector3 offset;
    public Vector3 tilePosition = new Vector3(0f, 1f, 0f);
    public Quaternion tileRotation = new Quaternion(0f, 0f, 0f, 0f);
    public Quaternion attackHighlightRotation = new Quaternion(90f, 0f, 0f, 0f);
    public Quaternion abilityHighlightRotation = new Quaternion(90f, 0f, 0f, 0f);

    public Color attackHighlightColor;
    public Color selectedMinionLightColor;
    public Color abilityHighlightColor;
    public Color abilitySelectColor;

    void Start()
    {
        gameMaster = GameObject.FindWithTag("GameController").GetComponent<GameMaster>();
        //selectedMinionLightColor = new Color(126f, 244f, 255f, 255f);
        //attackHighlightColor = new Color(255f, 0f, 0f, 255f);

        offset = transform.position;
        tilePrefab.transform.localScale = new Vector3(scaleTiles * sizeOfTiles, 1, scaleTiles * sizeOfTiles);
        tilePrefab2.transform.localScale = new Vector3(scaleTiles * sizeOfTiles, 1, scaleTiles * sizeOfTiles);

        tile = new Transform[amountRowsAndColumns, amountRowsAndColumns];
        movementHighlight = new Transform[amountRowsAndColumns, amountRowsAndColumns];
        attackHighlight  = new Transform[amountRowsAndColumns, amountRowsAndColumns];
        abilityHighlight = new Transform[amountRowsAndColumns, amountRowsAndColumns];
        for (int i = 0; i < amountRowsAndColumns; i++)
        {
            for (int j = 0; j < amountRowsAndColumns; j++)
            {
                if (transform.Find("tile[" + i + ", " + j + "]"))
                {
                    tile[i, j] = transform.Find("tile[" + i + ", " + j + "]").transform;
                }
                
                tilePosition.x = i * (sizeOfTiles * scaleTiles + gapBetweenTiles) + offset.x;
                tilePosition.z = j * (sizeOfTiles * scaleTiles + gapBetweenTiles) + offset.z;
                TileScript tileScript;
                if (tile[i, j] == null)
                {
                    if ((i + j) % 2 == 0)
                    {
                        tile[i, j] = Instantiate(tilePrefab, tilePosition, tileRotation);
                    }
                    else
                    {
                        tile[i, j] = Instantiate(tilePrefab2, tilePosition, tileRotation);
                    }
                    tileScript = tile[i, j].GetComponent<TileScript>();
                    tile[i, j].transform.parent = gameObject.transform;
                    tile[i, j].name = ("tile[" + i + ", " + j + "]");
                    tile[i, j].tag = "Tile";
                    tileScript.horizontal = i;
                    tileScript.vertical = j;
                }
                else
                {
                    tileScript = tile[i, j].GetComponent<TileScript>();
                    tileScript.minionOnTile = null;
                }
                Transform movementHighlightSearch = tile[i, j].Find("movementHighlight[" + i + ", " + j + "]");
                Transform attackHighlightSearch = tile[i, j].Find("attackHighlight[" + i + ", " + j + "]");
                Transform abilityHighlightSearch = tile[i, j].Find("abilityLight[" + i + ", " + j + "]");
                if (movementHighlightSearch)
                {
                    movementHighlight[i, j] = movementHighlightSearch.transform;
                }
                if (attackHighlightSearch)
                {
                    attackHighlight[i, j] = attackHighlightSearch.transform;
                }
                if (abilityHighlightSearch)
                {
                    abilityHighlight[i, j] = abilityHighlightSearch.transform;
                }
                if (movementHighlight[i, j] == null)
                {
                    Vector3 movementHighlightVector = new Vector3(tilePosition.x, movementHighlightHeight, tilePosition.z);
                    movementHighlight[i, j] =  Instantiate(movementHighlightPrefab, movementHighlightVector , tileRotation);
                    movementHighlight[i, j].transform.parent = tile[i,j];
                    movementHighlight[i, j].name = ("movementHighlight[" + i + ", " + j + "]");
                    movementHighlight[i, j].tag = "TileMovementHighlight";
                    movementHighlight[i, j].gameObject.SetActive(false);
                }
                if (attackHighlight[i, j] == null)
                {
                    Vector3 attackLightVector = new Vector3(tilePosition.x, attackHighlightHeight, tilePosition.z);
                    attackHighlight[i, j] = Instantiate(attackHighlightPrefab, attackLightVector, attackHighlightRotation);
                    attackHighlight[i, j].transform.parent = tile[i, j];
                   // attackHighlight[i, j].transform.SetPositionAndRotation(new Vector3(tilePosition.x, movementHighlightHeight, tilePosition.z), tileRotation);
                    attackHighlight[i, j].name = ("attackHighlight[" + i + ", " + j + "]");
                    attackHighlight[i, j].tag = "AttackLight";
                    attackHighlight[i, j].gameObject.SetActive(false);
                }
                if (abilityHighlight[i, j] == null)
                {
                    Vector3 abilityHighlightVector = new Vector3(tilePosition.x, abilityHighlightHeight, tilePosition.z);
                    abilityHighlight[i, j] = Instantiate(abilityHighlightPrefab, abilityHighlightVector, abilityHighlightRotation);
                    abilityHighlight[i, j].transform.parent = tile[i, j];
                    // attackHighlight[i, j].transform.SetPositionAndRotation(new Vector3(tilePosition.x, movementHighlightHeight, tilePosition.z), tileRotation);
                    abilityHighlight[i, j].name = ("abilityLight[" + i + ", " + j + "]");
                    abilityHighlight[i, j].tag = "AbilityHighlight";
                    abilityHighlight[i, j].gameObject.SetActive(false);
                }
            }
        }
    }

    public void deselectMinion()
    {
        gameMaster.deselect();
        for (int i = 0; i < amountRowsAndColumns; i++)
        {
            for (int j = 0; j < amountRowsAndColumns; j++)
            {
                deselectMovementHighlight(i, j);
                deselectAttackHighlight(i, j);
                deselectAbilityHighlight(i, j);
            }
        }
    }

    private void deselectMovementHighlight(int i, int j)
    {
        movementHighlight[i, j].gameObject.SetActive(false);
        tile[i, j].GetComponent<TileScript>().movementHighlighted = false;
    }
    private void deselectAttackHighlight(int i, int j)
    {
        attackHighlight[i, j].gameObject.SetActive(false);
        tile[i, j].GetComponent<TileScript>().attackHighlighted = false;
    }
    private void deselectAbilityHighlight(int i, int j)
    {
        abilityHighlight[i, j].gameObject.SetActive(false);
        tile[i, j].GetComponent<TileScript>().abilityHighlighted = false;
    }

    public float getSizeOfTiles()
    {
        return sizeOfTiles;
    }
}
