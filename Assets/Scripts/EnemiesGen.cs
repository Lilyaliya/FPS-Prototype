using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemiesGen : MonoBehaviour
{
    [SerializeField] private GameObject respawn;
    [SerializeField] private Text startTime;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    [SerializeField] public Text currScore;
    [HideInInspector]public bool GameOver = false;
    [HideInInspector] public bool cancelled = false;
    // Start is called before the first frame update
    void Awake()
    {
            InvokeRepeating("Spawn", Time.deltaTime,2f); //every 2 seconds
    }

    public void Cancelling()
    {
        //cancel respawn
        CancelInvoke("Spawn");
        cancelled = true;
    }
    public void StartInvoke()
    {
        //start respawn
        InvokeRepeating("Spawn", Time.deltaTime, 2f);
        cancelled = false;
    }
    void Spawn()
    {

        if (!GameOver)
        {
            Vector3 newPos = new Vector3(Random.Range(startPoint.transform.position.x, endPoint.transform.position.x),
                                         respawn.transform.position.y, Random.Range(startPoint.transform.position.z, endPoint.transform.position.z));
            respawn.GetComponent<Enemy>().currScore = currScore;
            Instantiate(respawn, newPos, respawn.transform.rotation);
            
        }
    }
}
