using UnityEngine;

public class DistanceBetween
{
    private Transform player;
    private Transform enemy;
    //private SpriteRenderer playerview;
    //private SpriteRenderer enemyview;
    //private Color playercurrtexture;
    private CapsuleCollider2D playercollider;
    private CapsuleCollider2D enemycollider;
    //private BoxCollider2D playerhitbox;
    private Animator playerstate;
    private Animator enemystate;
    private float apart;
    private Vector3 direction;
    private float attack_distance = 1.4f;
    

    // Start is called before the first frame update
    public void tagger()
    {
        player = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
        enemy = GameObject.FindWithTag(Tags.ENEMY_TAG).transform;
    }
    /*public void looker()
    {
        playerview = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<SpriteRenderer>();
        //enemyview = GameObject.FindWithTag(Tags.ENEMY_TAG).GetComponent<SpriteRenderer>();
    }*/
    public void CollideHere()
    {
        playercollider = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<CapsuleCollider2D>();
        enemycollider = GameObject.FindWithTag(Tags.ENEMY_TAG).GetComponent<CapsuleCollider2D>();
    }
    public void WhichAnimation()
    {
        playerstate = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<Animator>();
        enemystate = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<Animator>();
    }
    /*public void HitboxCollider()
    {
        playerhitbox = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponentInChildren<BoxCollider2D>();
    }*/
    public Transform GetPlayer()
    {
        return player;
    }
    public Transform GetEnemy()
    {
        return enemy;
    }
    public CapsuleCollider2D GetEnemyCollider()
    {
        return enemycollider;
    }
    // Update is called once per frame
    public float getdistance()
    {
        apart = Vector3.Distance(player.position, enemy.position);
        //Debug.Log(apart);
        return apart;
    }
    public Vector3 getdirection()
    {
        direction = player.position - enemy.position;
        direction.Normalize();
        return direction;
    }
    public float getattackdistance()
    {
        return attack_distance;
    }

    /*public bool lookeachother()
    {
        if (playerview.flipX != enemyview.flipX)
            return true;
        else
            return false;
    }*/

    public void IgnoreCollisionHere()
    {
        //Debug.Log(playerstate.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        if (playerstate.GetCurrentAnimatorClipInfo(0)[0].clip.name == "dash" || enemystate.GetCurrentAnimatorClipInfo(0)[0].clip.name == "dashE")
        {
            Physics2D.IgnoreCollision(playercollider, enemycollider);
        }
        else
            Physics2D.IgnoreCollision(playercollider, enemycollider, false);
    }

    //Failed attempt to extract the texture data in the sprite and detect collision
    /*public void TextureExtractor()
    {
        int success = 0;
        for(int i=412;i<512;i++)
        {
            for(int j=350;j<450;j++)
            {
                playercurrtexture = playerview.sprite.texture.GetPixel(i, j);
                if(playercurrtexture.r != 0 || playercurrtexture.g != 0 || playercurrtexture.b != 0)
                {
                    success = 1;
                    break;
                }
            }
        }
        if(success == 1)
            Debug.Log("Yes");
    }*/
}
