using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DayNight : MonoBehaviour
{
    public bool isnight;
   public GameObject[] rabits, wolfs,wolftrig;
    public GameObject light;
   // public Text conditiontext;
    public Camera cam;
    public Color daycolor, nightcolor;
    public Image background, label;
    public Sprite Sun, moon, daytext, nighttext;
    // Start is called before the first frame update
    void Start()
    {
        
        rabits = GameObject.FindGameObjectsWithTag("rabit");
        wolfs = GameObject.FindGameObjectsWithTag("wolf");
        wolftrig = GameObject.FindGameObjectsWithTag("wolftrig");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void daynight()
    {
        if (!isnight )
        {
            for (var i = 0; i < rabits.Length; i++)
            {
                rabits[i].GetComponent<rabit>().isngiht = true;
            }
            for(var j = 0; j < wolfs.Length; j++)
            {
                wolfs[j].GetComponent<wolf>().GetComponent<SphereCollider>().radius = 20;
                wolftrig[j].transform.localScale = new Vector3(50, 50, 50);
            }

            cam.backgroundColor = nightcolor;
            background.sprite = moon;
            label.sprite = nighttext;
            //  light.SetActive(false);
           // conditiontext.text = "Night";
            isnight = true;
            return;
        }
        else
        {
            for (var i = 0; i < rabits.Length; i++)
            {
                rabits[i].GetComponent<rabit>().isngiht = false;
                rabits[i].GetComponent<rabit>().IsWandering = false;
                StartCoroutine(wait());
                rabits[i].SetActive(true);
            }
            for (var j = 0; j < wolfs.Length; j++)
            {
                wolfs[j].GetComponent<wolf>().GetComponent<SphereCollider>().radius = 10;
                wolftrig[j].transform.localScale = new Vector3(21, 21, 21);
            }
            cam.backgroundColor = daycolor;
            background.sprite = Sun;
            label.sprite = daytext;
            //  light.SetActive(true);
           // conditiontext.text = "Day";
            isnight = false;
           
            return;
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        for (var i = 0; i < rabits.Length; i++)
        {
          
            rabits[i].GetComponent<rabit>().IsWandering = true;
        }
    }
   
}
