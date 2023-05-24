using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public GameObject target;
    public string sender;
    public Color cl;
    public float speed ;
    public Vector3 posBegin;
    public int moveValue = 0;
    public int areaCl = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveValue>0)
            Move(speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "unit" && this.areaCl!=collision.GetComponent<UnitScript>().areaCl && this.gameObject!= null && collision.gameObject != null)
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }    
    }
    private void Move(float speed)
    {

        if (moveValue == 1)
        {
            Vector3 diff = (target.transform.position - this.transform.position).normalized;
            transform.Translate(diff * speed * Time.deltaTime);
        }
          
        if(moveValue == 2)
        {
            Vector3 diff = (target.transform.position - this.transform.position).normalized;
            transform.Translate(diff * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.z + 7f));
        }
        if(moveValue == 3)
        {
            Vector3 diff = (target.transform.position - this.transform.position).normalized;
            transform.Translate(diff * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.z - 7f));
        }
        if(moveValue == 4)
        {
            Vector3 diff = (target.transform.position - this.transform.position).normalized;
            transform.Translate(diff * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.z + 14f));
        }
        if (moveValue == 5)
        {
            Vector3 diff = (target.transform.position - this.transform.position).normalized;
            transform.Translate(diff * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.z - 14f));
        }
    }
}
