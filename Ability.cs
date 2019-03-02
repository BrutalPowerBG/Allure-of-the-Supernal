using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TimeOfActivation
{
        Always,
        AtStartOfGame,
        StartOfYourTurn,
        EndOfYourTurn,
        StartOfEnemyTurn,
        EndOfEnemyTurn,
        FriendlyMinionMoves,
        EnemyMinionMoves,
        FriendlyMinionDies,
        EnemyMinionDies
};
[CreateAssetMenu(fileName ="Ability 1", menuName ="Ability")]
public class Ability : ScriptableObject{
    
    public new string name;
    public string description;
    public Sprite artwork;

    //one of these needs to be true;
    public bool isPassive=false;
    public bool hasPassiveEffect = false;
    public TimeOfActivation[] timesOfActivation;
    public bool hasInstantEffect=true;
    public int hasProlongedEffect=0; // 0 means false, 5 means the effect lasts for 5 turns

    public int cd; // 0 means no cooldown, 5 means 5 turns of cooldown
    public int currentCD=0;
    public int channel=0; // 0 means quickcast, 5 means this ability requires 5 turns of channeling
    public int range = 0;

    public int enemyMinionSelectionRequirement=0;
    public int friendlyMinionSelectionRequirement=0;
    public int tileSelectionRequirement = 0;


    //already implemented in AbilityTaken class
    /*
    public List<Transform> enemyMinionsSelected; // to be MADE
    public List<Transform> friendlyMinionsSelected; //To be made
    */
    public int unorthodoxCost = 0;

    public bool heals=false;
    public int healAmount=10;
    public bool healsOnlySelf = true;
    public int power = 0;

    public bool grabEnemy=false;
    public int distance = 4;

    public bool dealDamage = false;
    public int damageAmount = 5;

    public int requiresSacrifice = 0;
    public int requiresKills = 0;
    public int fromOtherMinion = 0; //id
    public List<int> tiersOfRequiredMinionDeaths;

}
