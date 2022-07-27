using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabit : MonoBehaviour
{
    public Transform hole;
    public bool detected,IsWandering,IsWalking;
    public float speed,wanderspeed;
    public float rotationSpeed;
    public bool isRotateingLeft, isRotateingRight;
    Rigidbody rb;
    public Animator anim;
    public GameObject daynight;
    public bool isngiht;
    public bool iseat;
    public bool isdead;
    BoxCollider bx;

    // Start is called before the first frame update
    void Start()
    {
        bx = GetComponent<BoxCollider>();   
        rb = GetComponent<Rigidbody>();
        daynight = GameObject.FindGameObjectWithTag("daynight");

    }

    // Update is called once per frame
    void Update()
    {

        // wandering 
            if (IsWandering == false && !detected && !isdead)
            {
                StartCoroutine(Wander());   
            }

            // check if he has to go to the hole
            if ((detected || isngiht) && !isdead)
            {
                transform.LookAt(hole);
                 var step = speed * Time.deltaTime;
             transform.position = Vector3.MoveTowards(transform.position, hole.transform.position, step);
            anim.SetInteger("State", 3);
            
            }
        // rotation
        if (isRotateingRight && !isdead)
                transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
            if (isRotateingLeft && !isdead)
                transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);

            //walk system and animation
        if (IsWalking && !isdead)
        {
            anim.SetInteger("State", 3);
            gameObject.transform.Translate(0, 0, wanderspeed * 0.03f);
        }
        //eating and animation
        else if (!IsWalking && !detected && !isngiht && !isdead)
        {
            
            anim.SetInteger("State", 1);
            StartCoroutine(eat());
            
           
        }
        //dead animation
        if (isdead)
        {
            anim.SetInteger("State", 0);
        }
       
    }
    //wandering
    IEnumerator Wander()
    {
        int rotationTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int rotateDirection = Random.Range(1, 2);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(2, 5);

        IsWandering = true;

        yield return new WaitForSeconds(walkWait);

        IsWalking = true;

        yield return new WaitForSeconds(walkTime);

        IsWalking = false;

        yield return new WaitForSeconds(rotateWait);

        if(rotateDirection == 1)
        {
            isRotateingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotateingLeft = false;
        }
        if (rotateDirection == 2)
        {
            isRotateingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotateingRight = false;
        }

        IsWandering = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "rabitsphere")
        {
          
            detected = true;
        }
       
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "rabitsphere" && !daynight.GetComponent<DayNight>().isnight)
            detected = false;
        if (other.gameObject.tag == "rabithole" && (detected || isngiht))
        {
            gameObject.SetActive(false); detected = false;
            isngiht = false; 
        }
    }
 

    IEnumerator eat()
    {
        anim.SetInteger("State", 2);
        iseat = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("State", 1);
        iseat = false;
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wolf" && !isdead)
        {
            isdead = true;
            collision.gameObject.GetComponent<wolf>().target = null;
            gameObject.tag = "Untagged";
          
            bx.isTrigger = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            collision.gameObject.GetComponent<wolf>()._eat();
            DeadAnim();
        }
    }
    public void DeadAnim()
    {
        anim.SetInteger("State", 0);
        anim.SetTrigger("die");
    }
}
