using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // World effector
    public  GameObject  worldGO;
    // UI Lnk
    public GameObject UIGO;
    // Upgrade status
    public bool acquired_existence      = false;
    public bool acquired_void_collision = false;
    public bool acquired_all_collision  = false;
    public bool acquired_tongue         = false;
    public bool acquired_attack         = false;

    // Stats
    public int level = 0;
    public int hp = 1;
    public float immunity = 0.5F;
    private float immunity_timer = 0;

    // Movements
    public bool IsControllable = false;
    public bool IsVisible = false;
    public float Speed = 100;
    // Tongue / Attack
    public GameObject TongueTip;
    public float AttackTime;
    private float AttackCounter;
    private Vector2 TongueHitPosition;
    private Vector2 AttackDirection;
    private Vector2 LastCollisionDirection;
    public float tongue_speed = 1;
    public LineRenderer LR;
    public bool IsRealAttack = false;
    public bool IsAttacking = false;
    public bool IsFirstAttackFrame = false;
    public bool OncePerFrame = true;
    public bool IsTranslating = false;
    public bool IsDamaging = false;
    public bool IsDamaged = false;
    public bool IsDamageable = true;
    public bool IsOnWater = false;
    private bool RetourTongueLock = false;
    // Inputs
    private float MoveX = 0;
    private float MoveY = 0;
    private bool Button_Space;
    private bool Button_Ctrl;
    private bool Button_Attack;
    private bool Button_Tongue;
    public int Player_Facing_Direction; // 0 = Right, 1 = Left, 2 = Up, 3 = Down 
    // Physics
    private Rigidbody2D RB2D;
    private SpriteRenderer SR;
    private BoxCollider2D BC;
    // Animation
    private Animator Animator;

    public void levelUp()
    {
        level++;
        switch (level)
        {
            case 1:
                acquired_void_collision = true;
                break;
            case 2:
                acquired_all_collision = true;
                break;
            case 3:
                acquired_tongue = true;
                break;
            case 4:
                acquired_attack = true;
                break;
            default:
                break;
        }
        Animator.SetInteger("Form", level);
    }

    public bool isSpirit()
    { return level < 2 ; }

    public void dead()
    {
        // GAME OVER
        Destroy(gameObject);
    }

    public void ResetAttack()
    {
        // Reset attacks value
        IsAttacking = false;
        IsControllable = true;
        //IsTranslating = false; Attacking = false when translating= true
        IsFirstAttackFrame = false;
        AttackCounter = 0;
        RetourTongueLock = false;
        // Reset tongue tip values
        TongueTip.transform.localPosition = new Vector3(0, 0, 0);
        TongueTip.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

        //LR.enabled = false;
    }

    public void ResetInputs()
    {
        Button_Ctrl = false;
        Button_Space = false;
        Button_Attack = false;
        Button_Tongue = false;
        MoveX = 0;
        MoveY = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        BC = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();

        LR = gameObject.GetComponent<LineRenderer>();
        LR.startWidth=0.1F;

        if(!IsVisible)
        {
            SR.enabled = false;
            BC.enabled = false;
        }

        worldGO = GameObject.Find(Constants.WORLD_GO_ID);
        level = 0;
        UIGO = GameObject.Find(Constants.UI_GO_ID);
    }

    public void TongueHit(Vector2 Position)
    {
        //if (!IsTranslating) // Avoid hit duplication
        {
            // Start translation to the TonguthitPosition
            IsTranslating = true;
            IsControllable = false; // Should laready be but just in case
            IsAttacking = false;
            TongueHitPosition = Position;
            // Should probably be in FixedUpdate
            RB2D.velocity = (TongueHitPosition - new Vector2(transform.position.x, transform.position.y)).normalized * Speed * Time.deltaTime;
        }
    }

    public void WaterDead()
    {
        transform.position = GameObject.Find("Player Water Spawn").transform.position;
    }

    public void StartAttack()
    {
        if (!IsTranslating)
        {
            IsAttacking = true;
            IsControllable = false;
            LR.enabled = true;

            Debug.Log("StartAttack");
            GameObject.Find("Audio Manager").GetComponent<AudioManager>().Play(Constants.SOUND_TONGUE_START);

            AttackCounter = 0;
            IsFirstAttackFrame = true;
            if (Player_Facing_Direction == 0)
            {
                AttackDirection = new Vector2(1, 0);
            }
            if (Player_Facing_Direction == 1)
            {
                AttackDirection = new Vector2(-1, 0);
            }
            if (Player_Facing_Direction == 2)
            {
                AttackDirection = new Vector2(0, 1);
            }
            if (Player_Facing_Direction == 3)
            {
                AttackDirection = new Vector2(0, -1);
            }
            TongueTip.transform.position = RB2D.transform.position;
            TongueTip.GetComponent<Rigidbody2D>().velocity = AttackDirection * tongue_speed;
            RB2D.velocity = new Vector2(0, 0);
        }
    }

    public void ReturnTongue()
    {
        if (!RetourTongueLock)
        {
            Debug.Log("Return Tongue");
            TongueTip.GetComponent<Rigidbody2D>().velocity = -AttackDirection * tongue_speed;
            GameObject.Find("Audio Manager").GetComponent<AudioManager>().Play(Constants.SOUND_TONGUE_START);
            RetourTongueLock = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (OncePerFrame)
        {
            // If we were translating after a tongue hit then we reset the player state
            if (IsTranslating)
            {
                if (!collision.gameObject.name.Contains("void"))
                {
                    IsDamageable = true;
                    immunity_timer = 0;
                    ResetAttack();
                    IsTranslating = false;
                    LastCollisionDirection = -AttackDirection.normalized;
                    transform.position = transform.position - new Vector3(0.1F * AttackDirection.normalized.x, 0.1F * AttackDirection.normalized.y, 0);

                }
            }
            OncePerFrame = false;
        }
    }

    private void LaunchAttack()
    {
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().Play(Constants.SOUND_PLAYER_ATTACK);
        IsRealAttack = true;
        var WB = GetComponentInChildren<WeaponBehavior>();
        WB.Attack();
        int Face = 0;
        if (Player_Facing_Direction == 0) Face = 0;
        if (Player_Facing_Direction == 1) Face = 2;
        if (Player_Facing_Direction == 2) Face = 1;
        if (Player_Facing_Direction == 3) Face = -1;
        WB.transform.RotateAround(transform.position, new Vector3(0,0,1), Face * 90);
        if(WB.transform.localRotation.eulerAngles.z < 0)
        {
            var l = WB.transform.localRotation;
            var a = l.eulerAngles;
            a.z += 360;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("void"))
        {
            if (OncePerFrame)
            {
                // If we were translating after a tongue hit then we reset the player state
                if (IsTranslating)
                {
                    IsDamageable = true;
                    immunity_timer = 0;
                    ResetAttack();
                    IsTranslating = false;
                    LastCollisionDirection = -AttackDirection.normalized;
                    transform.position = transform.position - new Vector3(0.2F * AttackDirection.normalized.x, 0.2F * AttackDirection.normalized.y, 0);
                }
                OncePerFrame = false;
            }
        }
        //transform.position = transform.position - new Vector3(0.1F * LastCollisionDirection.x, 0.1F * LastCollisionDirection.y, 0);
    }

    void ComputeFacingDirection()
    {
        if (Player_Facing_Direction == 0)
        {
            if (MoveX < 0)
            {
                Player_Facing_Direction = 1;
            }
            if (MoveX == 0 && MoveY > 0)
            {
                Player_Facing_Direction = 2;
            }
            if (MoveX == 0 && MoveY < 0)
            {
                Player_Facing_Direction = 3;
            }
        }
        else if (Player_Facing_Direction == 1)
        {
            if (MoveX > 0)
            {
                Player_Facing_Direction = 0;
            }
            if (MoveX == 0 && MoveY > 0)
            {
                Player_Facing_Direction = 2;
            }
            if (MoveX == 0 && MoveY < 0)
            {
                Player_Facing_Direction = 3;
            }
        }
        else if (Player_Facing_Direction == 2)
        {
            if (MoveY < 0)
            {
                Player_Facing_Direction = 3;
            }
            if (MoveY == 0 && MoveX > 0)
            {
                Player_Facing_Direction = 0;
            }
            if (MoveY == 0 && MoveX < 0)
            {
                Player_Facing_Direction = 1;
            }
        }
        else if (Player_Facing_Direction == 3)
        {
            if (MoveY > 0)
            {
                Player_Facing_Direction = 2;
            }
            if (MoveY == 0 && MoveX > 0)
            {
                Player_Facing_Direction = 0;
            }
            if (MoveY == 0 && MoveX < 0)
            {
                Player_Facing_Direction = 1;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        OncePerFrame = true;
        // check if dead
        if (hp <= 0)
            dead();

        if(IsDamageable)
        {
            immunity_timer += Time.deltaTime;
            if( immunity_timer > immunity)
            {

            }
        }

        // Get inputs
        MoveX = Input.GetAxisRaw("Horizontal");
        MoveY = Input.GetAxisRaw("Vertical");
        Button_Space |= Input.GetButtonDown("Jump");
        Button_Ctrl |= Input.GetButtonDown("Fire1");
        Button_Attack |= Input.GetButtonDown("Fire2");
        Button_Tongue |= Input.GetButtonDown("Fire3");
        
        
        // Animation states
        if (IsControllable)
        {
            // Compute facing direction for diagonals
            ComputeFacingDirection();
            if (MoveX != 0 && MoveX < 0)
            {
                SR.flipX = true;
            }
            else if (MoveX != 0)
            {
                SR.flipX = false;
            }
            Animator.SetInteger("Facingdirection", Player_Facing_Direction);
            Animator.SetBool("IsIdle", (MoveX == 0 && MoveY==0));
            // SetInt(Form) too here?
        }

        BC.enabled = true;        

        // > Check for player upgrade updates
        // Space => Make the player pop
        if ( Button_Space && !acquired_existence && !!worldGO)
        {
            WorldEvents worldEvents = worldGO.GetComponent<WorldEvents>();
            worldEvents.player_acquired();
            acquired_existence = true;

            DialogController dc = UIGO.GetComponent<DialogController>();
            if (!!dc)
                dc.startExitenceLine();
        }
        // Ctrl => Make the Player controllable
        if ( Button_Ctrl && acquired_existence )
        {
            DialogController dc = UIGO.GetComponent<DialogController>();
            if (!!dc)
                dc.startControlLine();
        }


        if(IsAttacking || IsTranslating)
        {
            // Draw tongue between Player position and TonguePosition
            Vector3[] positions = new Vector3[2];
            positions[0] = transform.position;
            positions[1] = TongueTip.transform.position;
            LR.SetPositions(positions);
        }
        else
        {
            LR.enabled = false;
        }

        // Attack timer
        
        if (IsAttacking)
        {
            AttackCounter += Time.deltaTime;
            // If Attack is ending
            if(AttackCounter > AttackTime)
            {
                ResetAttack();
            }

            if (AttackCounter > AttackTime / 2F)
            {
                ReturnTongue();
            }
        }
        if(IsTranslating)
        {
            // Make the Tongue Tip move according the the Player local space
            // Be careful not to start a new TongueHit.
            // var HitPoint_Local = transform.InverseTransformPoint(new Vector3(TongueHitPosition.x, TongueHitPosition.y, 0));
            // TongueTip.transform.localPosition = HitPoint_Local;
            TongueTip.transform.position = TongueHitPosition; 
        }

        if (acquired_tongue && !IsAttacking && !IsTranslating  && !IsRealAttack && Button_Tongue)
        {
            // Test attack
            StartAttack();
        }

        if(acquired_attack && !IsAttacking && !IsTranslating && !IsRealAttack && Button_Attack)
        {
            LaunchAttack();
        }
    }//! Update

    private void FixedUpdate()
    {
        //Move
        if (IsControllable && !IsOnWater)
        {
            var V = new Vector2(MoveX, MoveY).normalized;
            RB2D.velocity = new Vector2(V.x * Time.deltaTime * Speed, V.y * Time.deltaTime * Speed);  
        }
        if(IsVisible && Button_Ctrl)
        {
            IsControllable = true;
        }
        if(!IsVisible && Button_Space)
        {
            SR.enabled = true;
            IsVisible = true;
        }
        if (IsFirstAttackFrame)
        {
            
            IsFirstAttackFrame = false;
        }

        //Debug.Log(TongueTip.GetComponent<Rigidbody2D>().position);
        // Reset inputs
        ResetInputs();
    }
}
