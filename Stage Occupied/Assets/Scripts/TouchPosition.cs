 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPosition : MonoBehaviour
   
{
    public GameObject myArea;
    public string myTag;
    public string myAreaname;
  
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
          
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.transform.name != "unit" && collision.transform.name != myAreaname)
        {
            myArea.GetComponent<AreaScript>().target = collision.gameObject;
            myArea.GetComponent<AreaScript>().Atk = 1;
            Debug.Log("cham vao dich");
        }   
    }

}
