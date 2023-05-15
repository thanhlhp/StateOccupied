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
    private int canRenLine = 0;

    // Start is called before the first frame update
    void Start()
    {
        myNumber.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(secondTimer);
        if (canMake == false)
        {
            timer += 2 * Time.deltaTime;
        }
        if(canMake == true)
        {
            secondTimer += 1 * Time.deltaTime;
            if (secondTimer > 3)
            {
                canMake = false;
                secondTimer = 0;
            }
        }
        if (timer >= 1)
        {
            unit += 1;
            myNumber.text = unit.ToString();
            timer = 0;
        }
        if (manager == 1)
        {
            TouchScript.instance.touchPosition.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, +10));
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, TouchScript.instance.touchPosition.transform.position);

        }
        if (Atk == 2)
        {
            canMake = true;
            StartCoroutine(MakeUnit());
            Atk = 3;
        }    
    }
    IEnumerator MakeUnit()
    {
        while (unit > 0)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject u = Instantiate(unitPb, this.transform.position, Quaternion.identity);
            u.GetComponent<UnitScript>().target = target;
            u.gameObject.tag = this.gameObject.tag;
            u.GetComponent<UnitScript>().sender = this.transform.name;
            u.GetComponent<UnitScript>().cl = this.GetComponent<SpriteRenderer>().color;
            u.gameObject.name = "unit";
            unit--;
            myNumber.text = unit.ToString();
            secondTimer = 0;
            if( unit == 0)
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

        //touchPosition = Instantiate(obg, Vector3.zero, Quaternion.identity);
      
        TouchScript.instance.Spawn();
        manager = 1;
        Instantiate(line, this.transform.position, Quaternion.identity);
        TouchScript.instance.touchPosition.GetComponent<TouchPosition>().myArea = this.gameObject;
        TouchScript.instance.touchPosition.GetComponent<TouchPosition>().myTag = this.gameObject.tag;
        TouchScript.instance.touchPosition.GetComponent<TouchPosition>().myAreaname = this.transform.name;

    }
    private void OnMouseUp()
    {
        manager = 0;
        //Destroy(touchPosition);
        TouchScript.instance.Destroy();
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
        if (Atk == 1)
        {
            Atk = 2;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != this.gameObject.transform.tag && collision.transform.tag != "Touch")
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
        if (collision.transform.tag == this.gameObject.transform.tag && collision.transform.tag != "Touch")
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
