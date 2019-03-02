
using UnityEngine;

public class MoveMinion : MonoBehaviour {
    GameMaster gameMaster;
    BoardGrid grid;

    Transform minion;
    Stats minionStats;
    int currH;
    int currV;

    float moveSpeed = 3f;
    float distance = 0f;
    float distanceMultiplier;
    Vector2 direction;



	void Start () {
        gameMaster = GetComponentInParent<GameMaster>();
        grid = GameObject.FindWithTag("Grid").GetComponent<BoardGrid>();
        distanceMultiplier = grid.scaleTiles * grid.getSizeOfTiles() + grid.gapBetweenTiles;
	}
	
    //recently added:
    public void enqueueMovement(int targetTileH, int targetTileV)
    {
        MovementTaken movement = new MovementTaken(gameMaster.getSelected(), targetTileH, targetTileV); 
        gameMaster.enqueueAction(movement);
        grid.deselectMinion();
    }
    //recently added:
    public void activateMovement(MovementTaken toActivate)
    {
        if (toActivate.getType() != 'M')
        {
            Debug.Log("ERROR: Action isn't a movement:" + toActivate);
        }
        minion = toActivate.getMinionTakingAction();
        minionStats = minion.GetComponent<Stats>();

        currH = minionStats.horizontal;
        currV = minionStats.vertical;

        int horizontalTarget = toActivate.targetTileH;
        int verticalTarget = toActivate.targetTileV;
        minionStats.horizontal = horizontalTarget;
        minionStats.vertical = verticalTarget;

        distance = Mathf.Sqrt(Mathf.Pow((horizontalTarget - currH) * distanceMultiplier, 2) + Mathf.Pow((verticalTarget - currV) * distanceMultiplier, 2));
        direction = new Vector2(horizontalTarget - currH, verticalTarget - currV);
        grid.tile[currH, currV].GetComponent<TileScript>().minionOnTile = null;
        grid.tile[horizontalTarget, verticalTarget].GetComponent<TileScript>().minionOnTile = minion;
        grid.deselectMinion();
        gameMaster.setIfActionInProgress(true);
    }

    void FixedUpdate () {
        if (gameMaster.getActionInProgress())
        {
            if (distance > 2f)
            {
                if (minion == null)
                {
                    Debug.Log("No minion selected for movement invoked.");
                }
                else
                {
                    Vector3 toMove = new Vector3(direction.x * distanceMultiplier, 0, direction.y * distanceMultiplier) * Time.deltaTime * moveSpeed;
                    minion.position += toMove;
                    distance -= Mathf.Sqrt(Mathf.Pow((toMove.x), 2) + Mathf.Pow((toMove.z), 2));
                }
            }
            else
            {
                minion.gameObject.transform.position = new Vector3(distanceMultiplier * minionStats.horizontal + grid.offset.x, minion.position.y, (distanceMultiplier * minionStats.vertical + grid.offset.z));
                //Debug.Log(minion.position);
                distance = 0f;
                gameMaster.dequeueAction();
                gameMaster.setIfActionInProgress(false);
            }
        }
	}
    public float getDistance()
    {
        return distance;
    }
}
