using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
    
    public int unit;
    private float timer;
    public TextMeshPro myNumber;
    public int manager = 0;
    //public GameObject obg;
   
    //public GameObject touchPosition;
    public LineRenderer line;
    public int Atk = 0;
    public GameObject target;
    public GameObject unitPb;
    private bool canMake;
    private float secondTimer;
    public float botTimer;
    private int canRenLine = 0;
    private string targetName;
    public bool lineOn = false;
    // Start is called before the first frame update
    void Start()
    {
        myNumber.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canMake == false)
        {
            
            timer += 2 * Time.deltaTime;
            if(this.gameObject.tag =="EnemyStage")
            botTimer += 1 * Time.deltaTime;
        }
        if(canMake == true)
        {
            secondTimer += 1 * Time.deltaTime;
            if (secondTimer > 2)
            {
                canMake = false;
                secondTimer = 0;
            }
        }
        if (timer >= 1)
        {
            if ((this.gameObject.tag == "EmptyStage" && this.unit < LevelScript.instance.maxUnitEmpty)||(this.unit< LevelScript.instance.maxUnitState && this.gameObject.tag !="EmptyStage"))
            {
                unit += 1;
                myNumber.text = unit.ToString();
                timer = 0;
                
            }
        }
        if (manager == 1 )
        {
            TouchScript.instance.touchPosition.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, +10));
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, TouchScript.instance.touchPosition.transform.position);

        }
        if (Atk == 2)
        {
            if(target.transform.name != this.transform.name)
            {
                canMake = true;
                StartCoroutine(MakeUnit());
                Atk = 3;
            }
           
          
        }
        if (this.gameObject.tag == "EnemyStage"  && botTimer >3 )
        {
            botTimer = 0;
            foreach (GameObject state in LevelScript.instance.State)
            {
                if (state.GetComponent<AreaScript>().unit <= this.unit && state.transform.tag != this.transform.tag)
                {
                    Debug.Log(state.transform.name);
                    StartCoroutine(MakeUnit());
                    target = state;
                    break;
                }
               
            }      
        }
  
        if(TouchScript.instance.manager == 1 && this.manager == 1)
        {
            manager = 0;
            TouchScript.instance.Destroy();
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
            target = TouchScript.instance.target;
            //Destroy(line.gameObject);
            //if (Atk == 1)
            //{
                Atk = 2;
            //}
        }


    }
    IEnumerator MakeUnit()
    {
        while (unit > 0)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject u = Instantiate(unitPb, this.transform.position, Quaternion.identity);
            u.GetComponent<UnitScript>().target = target;
            //u.GetComponent<SpriteRenderer>().color = this.GetComponent<SpriteRenderer>().color;
            u.gameObject.tag = this.gameObject.tag;
            u.GetComponent<UnitScript>().sender = this.transform.name;
            u.GetComponent<UnitScript>().cl = this.GetComponent<SpriteRenderer>().color;
            u.gameObject.name = "unit";
            unit--;
            if(unit>=0)
            myNumber.text = unit.ToString();
            secondTimer = 0;
           
            if ( unit == 0)
            {
                Atk = 0;
                //canMake = false;
            }
            if (unit > 0)
            {
                canMake = true;
            }
        }
    }
    private void OnMouseDown()
    {
        TouchScript.instance.manager = 0;
        if (this.gameObject.tag == "MyStage")
        {
            TouchScript.instance.Spawn();
            manager = 1;
            Instantiate(line, this.transform.position, Quaternion.identity);
            TouchScript.instance.touchPosition.GetComponent<TouchPosition>().myArea = this.gameObject;
            TouchScript.instance.touchPosition.GetComponent<TouchPosition>().myTag = this.gameObject.tag;
            TouchScript.instance.touchPosition.GetComponent<TouchPosition>().myAreaname = this.transform.name;
        }
    }
    private void OnMouseUp()
    {
        TouchScript.instance.manager = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag != this.gameObject.transform.tag && collision.transform.tag != "Touch" && this.gameObject.name == collision.GetComponent<UnitScript>().target.transform.name) 
        {
           
            Destroy(collision.gameObject);
            if (unit > 0)
            {
                canMake = true;
                unit--;
                myNumber.text = unit.ToString();
               
               
            }
            else
            {
                this.transform.tag = collision.transform.tag;
                this.GetComponent<SpriteRenderer>().color = collision.GetComponent<UnitScript>().cl;
              
            }

        }
        if (collision.transform.tag == this.gameObject.transform.tag && collision.transform.tag != "Touch" && this.gameObject.name == collision.GetComponent<UnitScript>().target.transform.name)
        {
            
            if (collision.GetComponent<UnitScript>().sender != this.transform.name)
            {
                canMake = true;
                Destroy(collision.gameObject);
                unit++;
                myNumber.text = unit.ToString();
               
            }
        }
        //if (collision.transform.tag == "Touch" && this.gameObject.tag == "MyStage"&& canRenLine == 0)
        //{
        //    Instantiate(line, this.transform.position, Quaternion.identity);
        //    TouchScript.instance.canRenline = 1;
        //}
    }
}
