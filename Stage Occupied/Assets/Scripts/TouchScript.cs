using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{

    public GameObject obg;
    public GameObject touchPosition;
    public static TouchScript instance;
    public int canRenline;
    // Update is called once per frame
    private void Start()
    {
        TouchScript.instance = this;
    }
    void Update()
    {
      if(Input.GetMouseButtonUp(0))
        {
          
            canRenline = 2;
        }
    }
    public void Spawn()
    {
        this.touchPosition = Instantiate(obg, Vector3.zero, Quaternion.identity);
    }
    public void Destroy()
    {
        
        Destroy(this.touchPosition.gameObject);
    }
}
