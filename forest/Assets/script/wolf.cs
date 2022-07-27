using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolf : MonoBehaviour
{
    public GameObject target;
    public float speed, wanderspeed;
    public bool ismove,detected;
    public bool IsWandering, IsWalking,iseating,isattacking;
    public float rotationSpeed;
    public bool isRotateingLeft, isRotateingRight;
   public Animator anim;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // Move to target
        if (target != null  && target.active)
        {
            if (ismove && !iseating && !isattacking)
            {
                var step = speed * Time.deltaTime; 
                transform.LookAt(target.transform); // look at object
                gameObject.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step); //move object
                anim.SetInteger("state", 1);
            
            }
          
           
        }
        if (target!= null && !target.active)
        {
            target = null; 
        }

        //Start Wandering
         if (IsWandering == false && target == null && !iseating && !isattacking)
        {
            StartCoroutine(Wander());
              anim.SetInteger("state", 0);
           // anim.SetTrigger("idle");
        }

        //Rotation
        if (isRotateingRight && !iseating && !isattacking)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
              anim.SetInteger("state", 0);
        }
        if (isRotateingLeft && !iseating && !isattacking)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
            anim.SetInteger("state", 0);
        }

        if (IsWalking && !iseating && !isattacking)
        {
            gameObject.transform.Translate(0, 0, wanderspeed * 0.03f);
             anim.SetInteger("state", 1);
        }
        if(iseating)
            anim.SetInteger("state", 2);
        else if (isattacking)
            anim.SetInteger("state", 3);
    }
    
    private void OnTriggerStay(Collider other)
    {
        // check if target colide triger  
        if(other.gameObject.tag == "rabit")
        {
            target = other.gameObject;
            other.gameObject.GetComponent<rabit>().detected = true;
        }

       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Untagged")
        {
            target = null;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        // check if target exit triger  
        if (other.gameObject.tag == "rabit")
        {
            target = null;
            other.gameObject.GetComponent<rabit>().detected = false;
            
        }
    }
   
    IEnumerator Wander()
    {

        int rotationTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int rotateDirection = Random.Range(1, 2);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 3);

        IsWandering = true;

        yield return new WaitForSeconds(walkWait);

        IsWalking = true;

        yield return new WaitForSeconds(walkTime);

        IsWalking = false;

        yield return new WaitForSeconds(rotateWait);

        if (rotateDirection == 1)
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
    public void _eat()
    {
        StartCoroutine(eat());
    }
    IEnumerator eat()
    {
        isattacking = true;
        yield return new WaitForSeconds(1);
        isattacking = false;
        iseating = true;
        yield return new WaitForSeconds(3);
        
        iseating = false;
    }
   
}
