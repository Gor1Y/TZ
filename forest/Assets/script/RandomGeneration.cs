using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RandomGeneration : MonoBehaviour
{
    public int RabitCount;
    public int WolfCount;
    public Vector2 MapStartEndX, MapStartEndZ;
    public GameObject WolfGm,RabitGm;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        //spawn Wolf and rabit in the random position
        for (var i = WolfCount; i > 0; i--)
            Instantiate(WolfGm, new Vector3(Random.Range(MapStartEndX.x, MapStartEndX.y), 2, Random.Range(MapStartEndZ.x, MapStartEndZ.y)), Quaternion.identity);

        for (var i = RabitCount; i > 0; i--)
            Instantiate(RabitGm, new Vector3(Random.Range(MapStartEndX.x, MapStartEndX.y), 2, Random.Range(MapStartEndZ.x, MapStartEndZ.y)), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        // restart game (R)
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
