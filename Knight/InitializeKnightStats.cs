using UnityEngine;

public class InitializeKnightStats : MonoBehaviour {
    private Stats knightStats;
   
    void Start () {
        knightStats = GetComponent<Stats>();

        knightStats.id = 1;
        knightStats.tier = 2;
        knightStats.hp = 15;
        knightStats.hp_regen = 2;
        knightStats.armor = 5;
        knightStats.att_range = 1;
        knightStats.ad = 10;
        for (int i = 4; i <= 6; i++)
            for (int j = 4; j <= 6; j++)
            {
                //Debug.Log(knightStats.pattern[i,j]);
                knightStats.pattern[i, j] = true;
            }
	}
	
	
}
