using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//modify code to use the new Input Engine for keyboard and mouse inputs
//modify code to map the menu keyboard/mouse settings with in-game code
//modify code to be OOP compliant
//try to find the best algorithm for enemy fighting pattern - make it agressive, balanced and defensive based on probabilities.
//ignore.physics for collision ignore for a certain moment

public class Raiha : MonoBehaviour
{
    //Movement paramters
    [SerializeField]
    private float moveForce;
    [SerializeField]
    private float jumpForce;
    private float boostForce;
    private float movementX;

    //Calling objects
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anima;
    private Switcher switchup;
    private DistanceBetween distancer;
    private BoxCollider2D player1hitbox;
    
    //Combo variables
    private bool activateTimer;
    private const float defaultComboTimer = 0.3f;
    private float currentComboTimer;

    //KeyCode Groups
    //private bool attack = true;
    //private bool movement = true;
    //private bool lowkick = true;

    //Dash variables
    private int dashcountleft = 0;
    private int dashcountright = 0;
    private bool dashtimer;
    private const float defaultDashTimer = 0.3f;
    private float currentDashTimer;

    private const float totdashtime = 5f;
    private float currtotdashtime;
    private const int totaldashes = 2;
    private int currentdashes = 0;
    private bool dash;
    private int basicpunchcounter = 0;

    //pause screen when heavy attacks derive perfect score
    private float defpausetimer = 0.1f;
    private float actpausetimer;
    //Raiha optimal attack distance
    //private float raiha_crit_dist = 1.4f;
    //private float raiha_KO_dist = 0.8f;

    //ALL NORMAL ATTACK AND THEIR STATES
    public enum ComboState
    {
        NONE,
        PUNCH1,
        PUNCH2,
        PUNCH3,
        KICK1,
        KICK2
    }
    
    //The current combostate from the group of combostates defined above
    private ComboState currentComboState;
    
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
        switchup = new Switcher();
        distancer = new DistanceBetween();
        player1hitbox = GetComponentInChildren<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;
        currentDashTimer = defaultDashTimer;
        currtotdashtime = totdashtime;
        dash = true;
        boostForce = 1f;
        distancer.tagger();
        distancer.CollideHere();
        actpausetimer = defpausetimer;
        //distancer.looker();
    }

    // Update is called once per frame..................................................................
    void Update()
    {
        //Updates for movement
        movementX = Input.GetAxisRaw(Axis.HORIZONTAL_AXIS);
        MovePlayer();

        //Updates for attack
        AttackEnemy();
        ResetComboState();

        //distancer.TextureExtractor();
        //Updates for Dash
        DashPlayer();
        ResetDash();
        //DisableMovement();
        ResetPause();
    }

    private void FixedUpdate()
    {
        if(switchup.Getmovement())

            transform.position += boostForce * moveForce * Time.fixedDeltaTime * new Vector3(movementX, 0f, 0f);
    }

    //Start of Movement Functions.......................................................................
    private void MovePlayer()
    {
        if (movementX > 0 && switchup.Getmovement() == true)
        {
            anima.SetBool(AnimationTags.RUN_MOVEMENT, true);
            //sprite.flipX = false;
            transform.localScale = new Vector3(0.8f, 0.8f, 1);

        }
        else if (movementX < 0 && switchup.Getmovement() == true)
        {
            anima.SetBool(AnimationTags.RUN_MOVEMENT, true);
            //sprite.flipX = true;
            transform.localScale = new Vector3(-0.8f, 0.8f, 1);
        }
        else
        {
            anima.SetBool(AnimationTags.RUN_MOVEMENT, false);
        }
    }//end of move function

    private void DashPlayer()
    {
        if (switchup.Getattack() == true && dash == true)
        {
            //Since dash done when movementX >/< 0, no extra code is required
            //to prevent attacking when dashing
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                dashtimer = true;
                dashcountleft++;
                if (dashcountleft == 2)
                {
                    boostForce = 3f;
                    anima.SetTrigger(AnimationTags.DASH_MOVEMENT);
                    currentdashes++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                dashtimer = true;
                dashcountright++;
                if (dashcountright == 2)
                {
                    boostForce = 3f;
                    anima.SetTrigger(AnimationTags.DASH_MOVEMENT);
                    currentdashes++;
                }
            }
        }
    }//end of dash function

    //Start of attack functions......................................................................
    private void AttackEnemy()
    {
        if (!anima.GetBool(AnimationTags.RUN_MOVEMENT) && switchup.Getattack() == true && StaticObjects.pausestate == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                activateTimer = true;
                if (currentComboState == ComboState.KICK1)
                {
                    if (activateTimer)
                        currentComboState = ComboState.PUNCH2;
                    else
                        currentComboState = ComboState.PUNCH1;
                }
                if (currentComboState == ComboState.NONE)
                {
                    currentComboState = ComboState.PUNCH1;
                }
                if (currentComboState == ComboState.PUNCH2)
                {
                    StaticObjects.pausestate = true;
                    anima.SetTrigger(AnimationTags.PUNCH2_TRIGGER);
                }
                if (currentComboState == ComboState.PUNCH1)
                {
                    anima.SetTrigger(AnimationTags.PUNCH1_TRIGGER);
                    basicpunchcounter += 1;
                    if (basicpunchcounter == 3)
                    {
                        currentComboState = ComboState.PUNCH2;
                        basicpunchcounter = 0;
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.S) && switchup.Getlowkick() == true)
            {
                if(currentComboState == ComboState.NONE || currentComboState == ComboState.PUNCH1 || currentComboState == ComboState.PUNCH2)
                {
                    if (activateTimer)
                        currentComboState = ComboState.KICK2;
                    else
                        currentComboState = ComboState.KICK1;
                }
                activateTimer = true;
                if (currentComboState == ComboState.KICK1)
                {
                    anima.SetTrigger(AnimationTags.KICK1_TRIGGER);
                }
                if(currentComboState == ComboState.KICK2)
                {
                    anima.SetTrigger(AnimationTags.KICK2_TRIGGER);
                }
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                anima.SetTrigger(AnimationTags.EVADE_TRIGGER);
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                anima.SetTrigger(AnimationTags.PUNCH3_TRIGGER);
            }
        }
    } //end function for attack enemy

    //Start of actual hit logic ...................................................................
    private void CharacterHit()
    {
      //pass. not implemented yet !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    //Start of RESET timer functions................................................................
    private void ResetComboState()
    {
        if(activateTimer)
        {
            currentComboTimer -= Time.deltaTime;
            if(currentComboTimer <= 0f)
            {
                currentComboState = ComboState.NONE;
                currentComboTimer = defaultComboTimer;
                activateTimer = false;

            }
        }
    }//end of reset combo state
    private void ResetDash()
    {
        if(currentdashes == 1)
            currtotdashtime -= Time.deltaTime;
        if (currentdashes == totaldashes)
        {
            currtotdashtime -= Time.deltaTime;
            dash = false;
        }
        if (dashtimer)
        {
            currentDashTimer -= Time.deltaTime;
            if (currentDashTimer <= 0f)
            {
                dashtimer = false;
                dashcountleft = 0;
                dashcountright = 0;
                currentDashTimer = defaultDashTimer;
                boostForce = 1f;
            }
        }
        if(currtotdashtime <= 0f)
        {
            dash = true;
            currtotdashtime = totdashtime;
            currentdashes = 0;
        }
    }//end of reset dash

    public void ResetPause()
    {
        if (actpausetimer <= 0f)
        {
            StaticObjects.pausestate = false;
            Time.timeScale = 1f;
            actpausetimer = defpausetimer;
        }
        if (StaticObjects.pausestate == true)
        {
            Time.timeScale = 0f;
            actpausetimer = defpausetimer - Time.unscaledDeltaTime;
        }
    }
    //Start of Animation event helper functions.......................................................
    
    private void DisableAll()
    {
        switchup.SetDisableAttack();
        switchup.SetDisableMovement();
    }//end of disable everything

    private void EnableAll()
    {
        switchup.SetEnableAttack();
        switchup.SetEnableMovement();
        switchup.SetEnableLowkick();
    }//end of enable everything

    private void DisableLowKick()
    {
        switchup.SetDisableLowkick();
        switchup.SetDisableMovement();
    }//end of disable only lowkicks

    /*private void DisableMovement()
    {
        if(distancex.getdistance() <= distancex.getattackdistance())
            switchup.SetDisableMovement();
        if(distancex.lookeachother() == false)
        {
            switchup.SetEnableMovement();
        }
    }*/
    //code to disable movement based on distance and enable movmement based on player directions

    //Start of collision detection functions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player1hitbox.IsTouching(distancer.GetEnemyCollider()))
            Debug.Log(gameObject.name + " collided with" + collision.name);
    }
}
