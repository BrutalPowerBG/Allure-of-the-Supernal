using UnityEngine;
using System.Collections.Generic;
public class Minions : MonoBehaviour {
    List<Transform> minions;
    void Start() {
        // Debug.Log(transform.childCount);
        minions = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            minions.Add( transform.GetChild(i));
        }
        
    }

    public List<Transform> getMinions()
    {
       // Debug.Log(minions);
        return minions;
    }

}
