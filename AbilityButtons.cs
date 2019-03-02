using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButtons : MonoBehaviour {
    AbilitiesActivation abilitiesActivation;
    public void changeButtons(AbilitiesActivation _abilitiesActivation)
    {
        abilitiesActivation = _abilitiesActivation;
        foreach (Transform child in transform)
        {
            child.GetComponent<Image>().enabled = false;
        }
        int i = 0;
        foreach (Transform child in transform)
        {
            if (abilitiesActivation.ability.Length > i)
            {
                child.GetComponent<Image>().enabled = true;
                child.GetComponent<Image>().sprite = abilitiesActivation.ability[i].artwork;
                child.GetComponentInChildren<TextMeshProUGUI>().enabled=true;
                child.GetComponentInChildren<TextMeshProUGUI>().SetText(abilitiesActivation.ability[i].name);
            }
            else
            {
                child.GetComponent<Image>().enabled = false;
                child.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
            i++;
        }
    }

}
