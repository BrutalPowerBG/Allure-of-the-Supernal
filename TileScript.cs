using UnityEngine;

public class TileScript : MonoBehaviour {
    GameMaster gameMaster;
    BoardGrid grid;

    public int horizontal;
    public int vertical;
    public Transform minionOnTile = null;
    public bool movementHighlighted = false;
    public bool attackHighlighted = false;
    public bool abilityHighlighted = false;
    public bool isOccupied()
    {
        if (minionOnTile == null)
            return false;
        return true;
    }
  
    
}
