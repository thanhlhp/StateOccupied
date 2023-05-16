using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public static LevelScript instance;
    public int maxUnitEmpty = 10;
    public int maxUnitState = 50;
    public List<GameObject> State;
    private void Start()
    {
        LevelScript.instance = this;
    }
}
