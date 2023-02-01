using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]   public float appliedDamage = 0;
    [HideInInspector] public float timeToLive = 2.0f;
    [HideInInspector] public bool GameOver = false;
    //attach current score in inspector
    [SerializeField] public Text currScore;
    [SerializeField] private float delay = 0.05f;
    private float maxPoint = 12.1f; //for replacing an enemy from right side to the left repeatedly
    private bool rightDir = true;   //deafult moving direction
    private float currPos;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Destroys itself when menuScreen is present
        if (GameOver)
        {
            Destroy(gameObject);
            return;
        }
        //move enemy in scene from left wall to the right one and back
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            currPos = transform.position.x;
            rightDir = (Mathf.Abs(currPos) >= maxPoint) ? !rightDir : rightDir;
            transform.position = rightDir ? new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z) : new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
            delay = 0.05f;
        }
        //timeToLive is equal to 2 seconds
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            //decreasing a player's score
            if (appliedDamage == 0)
            {
                int temp = int.Parse(currScore.text);
                currScore.text = temp > 0 ? (temp - 1).ToString() : "0";
            }
            Destroy(gameObject);
            return;
        }
        if (appliedDamage != 0)
        {
            Destroy(gameObject, 0.5f);
            return;
        }
            
    }
}
