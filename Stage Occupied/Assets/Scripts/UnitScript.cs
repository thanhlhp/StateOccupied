using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public GameObject target;
    public string sender;
    public Color cl;
    public float speed;
    public Vector3 posBegin;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Move(speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag != this.gameObject.transform.tag && collision.transform.name =="unit")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }    
    }
    private void Move(float speed)
    {
        transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
