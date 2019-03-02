using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeOfObjectSelected
{
    Tile=0,
    Minion=1,
    Nexus=2,
    Building=3
}
public class LocalAbilitySelection : MonoBehaviour {

    bool abilityHighlightedMinion = false;
    bool abilitySelectedMinion = false;
    TypeOfObjectSelected typeOfObjectSelected; //currently only tiles

    private void Start()
    {
        string tag = transform.tag;
        switch(tag)
        {
            case "Tile": typeOfObjectSelected = TypeOfObjectSelected.Tile;
                break;
            case "Minion": typeOfObjectSelected = TypeOfObjectSelected.Minion;
                break;
            case "Nexus":
                typeOfObjectSelected = TypeOfObjectSelected.Nexus;
                break;
            case "Building":
                typeOfObjectSelected = TypeOfObjectSelected.Building;
                break;
        }
    }


    public void setAbilityHighlighted(bool highlight = true)
    {
        //TODO
    }
    public void setAbilitySelected(bool select = true)
    {
        //TODO
    }
    public bool getAbilityHighlighted()
    {
        return abilityHighlightedMinion;
    }

    public bool getAbilitySelected()
    {
        return abilitySelectedMinion;
    }

}
