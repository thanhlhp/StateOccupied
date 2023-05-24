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
    public int areaCl = 0;
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
    public int units = 1;
    private int index = 0;
    public bool lineOn = false;
    public List<GameObject> unitList;
    // Start is called before the first frame update
    void Start()
    {
        myNumber.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePlayManager.g.isPlaying)
        {
            if (canMake == false)
            {

                timer += 2 * Time.deltaTime;
                if (this.gameObject.tag == "EnemyStage")
                    botTimer += 1 * Time.deltaTime;
            }
            if (canMake == true)
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
                if ((this.gameObject.tag == "EmptyStage" && this.unit < LevelScript.instance.maxUnitEmpty) || (this.unit < LevelScript.instance.maxUnitState && this.gameObject.tag != "EmptyStage"))
                {
                    unit += 1;
                    myNumber.text = unit.ToString();
                    timer = 0;

                }
            }
            if (manager == 1)
            {
                TouchScript.instance.touchPosition.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, +10));
                line.SetPosition(0, this.transform.position);
                line.SetPosition(1, TouchScript.instance.touchPosition.transform.position);

            }
            if (Atk == 2)
            {
                if (target.transform.name != this.transform.name)
                {
                    canMake = true;
                    StartCoroutine(MakeUnit());
                    Atk = 3;
                }


            }
            if (this.gameObject.tag == "EnemyStage" && botTimer > 5)
            {
                botTimer = 0;
                float minDistance = 9999f;
                foreach (GameObject state in LevelScript.instance.State)
                {
                    if (state.GetComponent<AreaScript>().unit <= this.unit && state.GetComponent<AreaScript>().areaCl != this.areaCl)
                    {
                        float distance = Vector3.Distance(state.transform.position, this.transform.position);
                        if(minDistance>distance)
                        {
                            minDistance = distance;
                            target = state;
                        } 
                    }
                }
                StartCoroutine(MakeUnit());
       
            }

            if (TouchScript.instance.manager == 1 && this.manager == 1)
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
    }
    IEnumerator MakeUnit()
    {
        unitList = new List<GameObject>();
        while (unit > 0)
        {
         
            GameObject u = Instantiate(unitPb, this.transform.position, Quaternion.identity);
            u.GetComponent<CircleCollider2D>().enabled = false;
            u.GetComponent<UnitScript>().target = target;
            u.GetComponent<UnitScript>().speed = LevelScript.instance.speed;
            //u.GetComponent<SpriteRenderer>().color = this.GetComponent<SpriteRenderer>().color;
            u.gameObject.tag = this.gameObject.tag;
            u.GetComponent<UnitScript>().sender = this.transform.name;
            u.GetComponent<UnitScript>().areaCl = this.areaCl;
            u.GetComponent<UnitScript>().cl = this.GetComponent<SpriteRenderer>().color;
            u.gameObject.name = "unit";
            unitList.Add(u);
            unit--;
        }
        
        myNumber.text = unit.ToString();
        units = unitList.Count;
        GameObject[] unitArr = unitList.ToArray();
        index = 0;
        while(units>0)
        {
            
            if (units <= 0)
            {
                Atk = 0;
            }
            else
            {
   
                canMake = true;
                yield return new WaitForSeconds(0.3f);
                for (int i = 1; i <= 5; i++)
                {
                    if (index < unitArr.Length)
                    {
                        unitArr[index].GetComponent<UnitScript>().moveValue = i;
                        unitArr[index].GetComponent<CircleCollider2D>().enabled = true;
                    }
                    index++;
                    units--;
                }
            }
        }
    
    }
    private void OnMouseDown()
    {
        if (GamePlayManager.g.isPlaying)
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
    }
    private void OnMouseUp()
    {
        if (GamePlayManager.g.isPlaying)
        {
            TouchScript.instance.manager = 1;
        }
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
               
                if (this.gameObject.tag == "MyStage")
                {
                    GamePlayManager.g.myStates--;
                }
                if (collision.tag == "MyStage")
                {
                    GamePlayManager.g.myStates++;
                }
                this.transform.tag = collision.transform.tag;
                this.GetComponent<SpriteRenderer>().color = collision.GetComponent<UnitScript>().cl;
                this.areaCl = collision.GetComponent<UnitScript>().areaCl;
                GamePlayManager.g.CheckWin();

            }

        }
        if (collision.transform.tag == this.gameObject.transform.tag && collision.transform.tag != "Touch" && this.gameObject.name == collision.GetComponent<UnitScript>().target.transform.name)
        {

            if (collision.GetComponent<UnitScript>().sender != this.transform.name&&collision.GetComponent<UnitScript>().areaCl == this.areaCl)
            {
                canMake = true;
                Destroy(collision.gameObject);
                unit++;
                myNumber.text = unit.ToString();
               
                

            }
            if (collision.GetComponent<UnitScript>().sender != this.transform.name && collision.GetComponent<UnitScript>().areaCl != this.areaCl)
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
                    this.areaCl = collision.GetComponent<UnitScript>().areaCl;
                    GamePlayManager.g.CheckWin();

                }
            }
        }
        //if (collision.transform.tag == "Touch" && this.gameObject.tag == "MyStage"&& canRenLine == 0)
        //{
        //    Instantiate(line, this.transform.position, Quaternion.identity);
        //    TouchScript.instance.canRenline = 1;
        //}
    }
}
