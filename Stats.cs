
using UnityEngine;

public class Stats : MonoBehaviour {
    public int id=-1;
    public int playerAllegiance = 0;

    public int tier = 0;
    public int hp = 1;
    public int max_hp = 1;
    public int ad = 0;
    public int armor = 0;
    public int att_range = 1;
    public int hp_regen = 0;
    public int crt = 0;
    public bool invulnerability = false;
    public int level = 0; // 0 if it doesnt level up or some shit idk
    public bool[,] pattern;
    public int patternDisplacement = 5;
    public int horizontal, vertical;

    private int enemiesKilled = 0;


    public Ability[] ability;
    //string ability_name[5];
    public short last_used_ability = 0;
    // void call_ability(int, int, int, int/*, int,  bool*/);

    // NON-Gameplay Stats
    public Vector3 portraitCameraDisplacement;
    public void incrementEnemiesKilled(int enemiesCount=1)
    {
        enemiesKilled += enemiesCount;
    }
    public void resetEnemiesKilled()
    {
        enemiesKilled = 0;
    }
    
    public void InitializePosition(int posH, int posV)
    {
        vertical = posV;
        horizontal = posH;
    }

    void Start()
    {
        portraitCameraDisplacement = new Vector3(0f, 7f, 2.5f);
       pattern = new bool[10, 10]; 
    }
}
