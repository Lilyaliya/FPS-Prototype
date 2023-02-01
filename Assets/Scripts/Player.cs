using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerLook look;
    private AudioSource sound;
    private float fireDelay = 0.5f;
    [SerializeField] private Text Score;
    [SerializeField] private float weaponSpan; 
    [HideInInspector] public bool gameOver;
    // Start is called before the first frame update
    void Awake()
    {
        //lock the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //initializing onFoot action in InputSystem
        playerInput = new PlayerInput();
        look = GetComponent<PlayerLook>();
        onFoot = playerInput.OnFoot;
        //sound for fire
        sound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        fireDelay -= Time.deltaTime;
        //when we can fire again
        if (fireDelay <= 0 &&  Input.GetMouseButtonDown(0) && !gameOver)
        {
            fireDelay = 0.5f;
            sound.Play();
            RaycastHit hit;
            //detect an object to hit
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponSpan))
            {
                if (hit.collider.tag == "Target")
                {
                    var enemy = hit.collider.gameObject;
                    enemy.GetComponent<TargetScript>().isHit = true; //set the flag for animating a current target
                    enemy.GetComponentInParent<Enemy>().appliedDamage = 1f; //sets the applied damage to the aim
                    UpScore(); //increase a player's score
                }
            }

        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //ability for looking around
        if (!gameOver)
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        //disable onFoot action
        else if (gameOver)
            OnDisable();
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
    private void UpScore()
    {
        int currScore = int.Parse(Score.text);
        currScore += 1;
        Score.text = currScore.ToString();
    }
}
