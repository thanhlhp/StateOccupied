 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPosition : MonoBehaviour
   
{
    public GameObject myArea;
    public string myTag;
    public string myAreaname;
    public GameObject target;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
          
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.transform.name != "unit"  )
        {
            //myArea.GetComponent<AreaScript>().target = collision.gameObject;

            //myArea.GetComponent<AreaScript>().Atk = 1;
            //Debug.Log("cham vao dich"+collision.gameObject.name);
            TouchScript.instance.target = collision.gameObject;

        }
       if(collision.transform.tag == "MyStage" && collision.transform.name != "unit")
        {
            if (collision.GetComponent<AreaScript>().manager == 0)
            {
                AreaScript myState = collision.GetComponent<AreaScript>();
            
                myState.manager = 1;
                if (myState.lineOn == false)
                {
                    Instantiate(collision.GetComponent<AreaScript>().line, this.transform.position, Quaternion.identity);
                    myState.lineOn = true;
                } 
            }
           
        }
    }

}
