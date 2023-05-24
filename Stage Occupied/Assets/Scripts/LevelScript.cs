using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public static LevelScript instance;
    public int maxUnitEmpty = 10;
    public int maxUnitState = 50;
    public Transform gameArea;
    public List<GameObject> State;
    public float speed;
    public GameObject uiArea;
    private void Awake()
    {
        gameArea = this.transform.Find("Game_area");
        uiArea = GameObject.Find("Ui_area");
     
    }
    private void Start()
    {
        LevelScript.instance = this;
        GamePlayManager.g.goat = State.Count;
        foreach (GameObject g in State)
        {
            if(g.gameObject.tag == "MyStage")
            {
                GamePlayManager.g.myStates++;
            }
        }
        
    }
    private void Update()
    {
        if(GamePlayManager.g.isPlaying)
        Destroy(uiArea);
    }
}
