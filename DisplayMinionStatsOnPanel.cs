using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayMinionStatsOnPanel : MonoBehaviour {

    GameMaster gameMaster;
    TextMeshProUGUI minionStats;
    public bool first;

    private void Start()
    {
        gameMaster = GameObject.FindWithTag("GameController").GetComponent<GameMaster>();
        minionStats = GetComponentInParent<TextMeshProUGUI>();
    }
    void Update () {
        Transform selected = gameMaster.getSelected();

        if (selected == null)
        {
            minionStats.SetText("");
        }
        else
        {
            if (first)
            {
                minionStats.SetText("HP: " + selected.GetComponent<Stats>().hp.ToString() + "\n"
               + "AD: " + selected.GetComponent<Stats>().ad.ToString() + "\n"
               + "AR: " + selected.GetComponent<Stats>().armor.ToString() + "\n");
            }
            else
            {
                minionStats.SetText("HPR: " + selected.GetComponent<Stats>().hp_regen.ToString() + "\n"
               + "CRT: " + selected.GetComponent<Stats>().crt.ToString() + "\n"
               + "ATR: " + selected.GetComponent<Stats>().att_range.ToString() + "\n");
            }
        }
	}
}
