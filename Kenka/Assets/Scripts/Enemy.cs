using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Calling objects
    //private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anima;
    private Switcher switcho;
    private DistanceBetween distancex;
    [SerializeField]
    private EnemyStats Enemypatternbalanced;
    [SerializeField]
    private EnemyStats Enemypatterndefense;
    [SerializeField]
    private EnemyStats Enemypatternoffense;
    //Enemy following
    //private bool followPlayer;
    [SerializeField]
    private float speed;
    private EnemyStats Enemypattern;
    //Store Player 1 stats in arrays. One for attack pattern and another for crouch
    //private int[] attackpattern = new int[4];
    private int[] finalattackpattern = new int[5];
    private int[] movementpattern = new int[2];
    private bool move;
    private int enemyattackchoice;
    private int probabilitytotal;
    //private int[] probabilitymatrix = new int[4];
    //private float closeorfar;
    //private float currentstattimer;
    //private float basestattimer = 30f;
    private float aihittimer;
    private float aistandardtimer = 0.5f;
    //private int finalchoice;
    //private bool predictprobability;
    //private int[] specialpattern = new int[2];
    //Special attacks will be implemented and then this will be activated.

    void Awake()
    {
        switcho = new Switcher();
        distancex = new DistanceBetween();
        //body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //followPlayer = true;

        //NOTE: ADD LOGIC TO ASSIGN THE CORRECT ENEMY PROFIEL BASED ON WHAT USER CHOOSES IN MENU
        //
        //

        //Keeping a default profile for now, will change based on above logic after menu is created.
        Enemypattern = Enemypatternbalanced;
        for (int i = 0; i < Enemypattern.GetTotal().Length-1; i++)
        {
            //attackpattern[i] = 0;
            finalattackpattern[i] = Enemypattern.GetTotal()[i];
        }
        for (int i = 0; i < movementpattern.Length; i++)
        {
            movementpattern[i] = 0;
        }
        //currentstattimer = basestattimer;
        move = false;
        //closeorfar = 0;
        //predictprobability = true;
        aihittimer = aistandardtimer;
        distancex.tagger();
        distancex.CollideHere();
        distancex.WhichAnimation();
        probabilitytotal = Enemypattern.GetTotal()[Enemypattern.GetTotal().Length-1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowTarget();
    }
    void Update()
    {
        distancex.IgnoreCollisionHere();
        //AttackScript();
        //AttackAnimate();
    }
    public void FollowTarget()
    {
        //Reducing hittimer every frame
        aihittimer -= Time.deltaTime;

        //Logic to flip the character based on the direction and also only when it is allowed to move
        if (distancex.getdirection().x < 0 && switcho.Getmovement() == true)
        {
            sprite.flipX = true;
        }
        else if (distancex.getdirection().x > 0 && switcho.Getmovement() == true)
        {
            sprite.flipX = false;
        }

        //Logic to handle the movement of AI and control the attack pattern when its not moving
        if (distancex.getdistance() > distancex.getattackdistance())
        {
            if (move == false)
            {
                int random = Random.Range(1, 10);
                if (random != 1)
                    move = true;
            }
            if (switcho.Getmovement() == true && move == true)
            {
                transform.position += speed * Time.fixedDeltaTime * new Vector3(distancex.getdirection().x, 0, 0);
                anima.SetBool(AnimationTags.ENEMY_MOVEMENT, true);
            }
            else if (switcho.Getattack() == true && move == false && aihittimer <= 0)
            {
                aihittimer = aistandardtimer;
                FarAttackProbabiltiy();
            }
            //closeorfar = Random.Range(0f, 2f);
        }
        else
        {
            if (move == true)
            {
                move = false;
                anima.SetBool(AnimationTags.ENEMY_MOVEMENT, false);
            }
            if (aihittimer <= 0)
            {
                aihittimer = aistandardtimer;
                //CloseAttackProbability();
                CloseAttackChoice();
            }
            //closeorfar = Random.Range(0f, 1.1f);
        }
    }

    /*public void AttackScript()
    {
        currentstattimer -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.W))
        {
            attackpattern[0] += 1;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            attackpattern[1] += 1;
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            attackpattern[2] += 1;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            attackpattern[3] += 1;
        }
        if (currentstattimer <= 0)
        {
            float attacksum = 0;
            for (int i = 0; i < attackpattern.Length; i++)
            {
                attacksum += attackpattern[i];
            }
            for (int i = 0; i < finalattackpattern.Length; i++)
            {
                finalattackpattern[i] = (int)(attackpattern[i] *100/ attacksum);
            }
            currentstattimer = basestattimer;
            for (int i = 0; i < attackpattern.Length; i++)
            {
                attackpattern[i] = 0;
            }
        }
    }*/

    public void FarAttackProbabiltiy()
    {
        Debug.Log("Will be implemented later");
    }

    public void CloseAttackChoice()
    {
        //To pick a number between 1 and 100
        int random = Random.Range(1, probabilitytotal);
        int lowlimit = 1;
        int highlimit;
        for (int i = 0;i < finalattackpattern.Length;i++)
        {
            highlimit = lowlimit + finalattackpattern[i];
            if(random>=lowlimit && random<=highlimit)
            {
                enemyattackchoice = i;
                break;
            }
            lowlimit = highlimit;
        }
        if (enemyattackchoice == 0 && switcho.Getattack() == true)
        {
            anima.SetBool(AnimationTags.E_PUNCH1_TRIGGER, true);
        }
        if (enemyattackchoice == 1 && switcho.Getattack() == true)
        {
            anima.SetBool(AnimationTags.E_PUNCH2_TRIGGER, true);
        }
        if (enemyattackchoice == 2 && switcho.Getattack() == true)
        {
            anima.SetBool(AnimationTags.E_PUNCH3_TRIGGER, true);
        }
        if (enemyattackchoice == 3 && switcho.Getlowkick() == true && switcho.Getattack() == true)
        {
            anima.SetBool(AnimationTags.E_KICK1_TRIGGER, true);
        }
        if (enemyattackchoice == 4 && switcho.Getattack() == true)
        {
            anima.SetBool(AnimationTags.E_KICK2_TRIGGER, true);
        }
    }

    /*public void CloseAttackProbability()
    {
        //To pick a number between 1 and 100
        //int random = Random.Range(1, 100); Required but need to calculate the total first
        //Debug.Log(random);
        int lowlimit = 1;
        int highlimit = 0;
        for(int i = 0; i<finalattackpattern.Length; i++)
        {
            highlimit = lowlimit + finalattackpattern[i];
            if (random>=lowlimit && random<=highlimit)
            {
                //Debug.Log(random);
                enemyattackchoice = i;
                break;
            }
            lowlimit = highlimit;
        }
        if(enemyattackchoice == 0 && switcho.Getattack() == true)
        {
            anima.SetBool(AnimationTags.E_PUNCH1_TRIGGER, true);
        }
        if(enemyattackchoice == 1 && switcho.Getlowkick() == true && switcho.Getattack() == true)
        {
            anima.SetBool(AnimationTags.E_KICK1_TRIGGER, true);
        }
        if (enemyattackchoice == 2 && switcho.Getattack() == true)
        {
            anima.SetBool(AnimationTags.E_PUNCH2_TRIGGER, true);
        }
        if(enemyattackchoice == 3 && switcho.Getattack() == true)
        {
            anima.SetBool(AnimationTags.E_KICK2_TRIGGER, true);
        }
    }*/


    //Start of Animation event helper functions.......................................................
    private void DisableAll()
    {
        switcho.SetDisableAttack();
        switcho.SetDisableMovement();
    }//end of disable everything

    private void EnableAll()
    {
        switcho.SetEnableAttack();
        switcho.SetEnableMovement();
        switcho.SetEnableLowkick();
    }//end of enable everything

    private void DisableLowKick()
    {
        switcho.SetDisableLowkick();
        switcho.SetDisableMovement();
    }//end of disable only lowkicks
}