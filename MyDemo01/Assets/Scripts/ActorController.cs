using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public bool m_Respawning = false;



    public GameObject model;
    public PlayerInput pi;
    private BowScript bs;
    public CameraController cancom;
    private float walkSpeed = 5f;
    private float runSpeed = 1.6f;
    private float lockSpeed = 0.7f;
    public float JumpVelocity;

    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionNormal;
    public PhysicMaterial frictionZero;


    public bool stopMove;
    private bool canAttack;
    private bool firstAir = true;
    private bool m_Grounded;
    public bool canFireSword;
    private Animator anim;
    public Rigidbody rigid;
    public Vector3 planarVec;
    private Vector3 thrustVec = Vector3.zero;
    private Vector3 deltaPos;
    private Vector3 contactNormal = Vector3.zero;
    private int stepsSineceLastGrounded;
    private CapsuleCollider col;
    private MyTime fireSwordTime = new MyTime();
    public ParticleSystem[] parical;

    
    [Range(30, 80)] public float slopeLimit = 75f;

    //******************************以下是跟随滑块移动逻辑
    public Move_Cube Move_CubeScript;
    //private float lerpTarget;
    /// <summary>
    /// 活动事件
    /// </summary>
    public delegate void OnActionDelegate();
    public event OnActionDelegate OnAction;
    /// <summary>
    /// 锁定平面向量
    /// </summary>
    private bool lockPlanar = false;
    private bool trackDirection = false;
    void Awake()
    {
        model = transform.Find("unitychan").gameObject;
        anim = model.GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        cancom = pi.transform.Find("CameraHandle").transform.GetComponentInChildren<CameraController>();
        parical = transform.Find("SlashEffects").GetComponentsInChildren<ParticleSystem>();
        bs = GetComponent<BowScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        CheckSlopeLimit();
        anim.SetBool("isGround", m_Grounded);
        //用于检测自从接地以来经历了多少物理步长
        //stepsSineceLastGrounded++;
        if (m_Grounded )
        {
            //stepsSineceLastGrounded = 0;
            canAttack = true;
            firstAir = true;
           // col.enabled = true;
        }
        else
        {
            // col.enabled = false;
            //planarVec = Vector3.Lerp(planarVec, Vector3.zero, Time.deltaTime);
        }
        fireSwordTime.Tick();
        //移动
        if (!cancom.lockState)
        {
            anim.SetBool("lock", false);
            anim.SetFloat("right", 0);
            //anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (pi.run) ? 2.0f : 1.0f, 0.3f));
            if (!pi.run)
            {
                anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), 1.0f, 0.8f));
            }
            else
            {
                anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), 2.0f, 0.1f));

            }

        }
        else
        {
            anim.SetFloat("forward", transform.InverseTransformVector(pi.Dvec).z );
            anim.SetFloat("right", transform.InverseTransformVector(pi.Dvec).x );
        }
        if (!cancom.lockState)
        {
            if (pi.Dmag > 0.1f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);

            }
            //锁速度值
            if (!lockPlanar)
            {
                planarVec = pi.Dmag * model.transform.forward * (stopMove ? 0 : walkSpeed * ((pi.run) ? runSpeed : 1.0f));

            }

        }
        else
        {
            anim.SetBool("lock", true);
            if (pi.roll)
            {

                anim.SetTrigger("roll");
            }

            if (!trackDirection)
            {

                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = planarVec.normalized;
            }
            if (!lockPlanar)
            {
                planarVec = pi.Dvec * (stopMove ? 0 : walkSpeed*lockSpeed);
            }
        }
        //跳跃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
         
        }


        //攻击
        if (Input.GetMouseButtonDown(0) && canAttack && !CheckState("conterBack2"))
        {
            anim.SetTrigger("attack");

           
        }
        if (Input.GetMouseButtonDown(0) && canAttack && cancom.lockState &&Input.GetKey(KeyCode.S) && !CheckState("conterBack2") )
        {
            anim.SetTrigger("attack");
            anim.SetBool("downKey", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("downKey", false);
        }
        //发射幻影剑&处决
        if (Input.GetMouseButtonDown(0) && canAttack  && Input.GetKey(KeyCode.W) && !CheckState("conterBack2"))
        {
            OnAction.Invoke();
        }

        //魔法攻击
        if (GameData.leveName == "level4Enemy")
        {
            if (cancom.lockState)
            {
                float dist = Vector3.Distance(transform.position, cancom.lockTarget.em.transform.position);
                if (Input.GetMouseButtonDown(1) && canAttack
                     && !CheckState("conterBack2")
                    && canFireSword
                    && GameData.canFireAttack && !CheckState("lock")
                    && dist >= 1.5f)
                {
                    anim.SetTrigger("fireSword");
                    //anim.SetBool("forwardKey", true);
                    StartTimer(fireSwordTime, 2f);
                }
                //if (!canFireSword)
                //{
                //    anim.SetBool("forwardKey", false);
                //}
                if (fireSwordTime.state == MyTime.STATE.RUN)
                {
                    canFireSword = false;
                }
                else
                {
                    canFireSword = true;
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(1) && !CheckStateTag("AttackL") && !CheckState("conterBack2") && GameData.canMagic && m_Grounded)
            {
                if (!cancom.lockState)
                {
                    bs.img_Fire.enabled = true;

                }
                if (!bs.shootRest && !bs.isAiming)
                {
                    anim.SetBool("magic", true);
                    bs.canShoot = false;
                    bs.isAiming = true;
                    model.transform.eulerAngles = this.transform.eulerAngles;
                    StopCoroutine(bs.PrepareSequence());
                    StartCoroutine(bs.PrepareSequence());
                    Transform bow = bs.bowZoomTransform;
                    bs.arrowModel.transform.localPosition = bs.arrowOriginalPos;

                }

            }

        }

        if (Input.GetMouseButtonUp(1) && !CheckStateTag("AttackL") && !CheckState("conterBack2") && GameData.canMagic && m_Grounded && GameData.leveName != "level4Enemy")
        {
            if (!cancom.lockState)
            {
                bs.img_Fire.enabled = false;

            }
            if (!bs.shootRest && bs.isAiming)
            {
                anim.SetBool("magic", false);
                StartCoroutine(bs.ShootSequence());
               

            }
        }
        //GP
        if (Input.GetKeyDown(KeyCode.Q) && canAttack && !CheckState("conterBack2") && GameData.canConterBack )
        {
            anim.SetTrigger("conterBack");
        }
        //回血
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MVC.SendEvent(GameDefine.command_AddHP, 20f);
        }
        //锁定功能
        cancom.LockUnlock();
        //暂停功能
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.ShowUI(E_UiId.StopUI);
        }

    }
    /// <summary>
    /// 锁死键盘方向输入和速度值
    /// </summary>
    private void LockDead()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }




    private void FixedUpdate()
    {
        if (m_Grounded)
        {
        rigid.position += deltaPos;
        //rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        Vector3 targetPosition = rigid.position + planarVec* Time.fixedDeltaTime;
        Vector3 targetVeloctiy = (targetPosition - transform.position) / Time.fixedDeltaTime;
        targetVeloctiy.y = rigid.velocity.y;
        rigid.velocity = targetVeloctiy;
        //rigid.position += planarVec * Time.fixedDeltaTime + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;

        }
       

        //*******以下是角色跟随滑块移动逻辑
        if (Move_CubeScript != null)
        {
            //让角色跟随滑块移动
            if (Move_CubeScript.GetValue01_value02() < 0)
            {
               
                this.transform.Translate(new Vector3(0, 0, 0.1f) * Move_CubeScript.GetSpeed() * Time.fixedDeltaTime);
            }
            else
            {

                this.transform.Translate(new Vector3(0, 0, -0.1f) * Move_CubeScript.GetSpeed() * Time.fixedDeltaTime);

            }
        }
    }
    
    /// <summary>
    /// 获取状态机当前所在状态
    /// </summary>
    /// <param name="stateName"></param>
    /// <param name="layerName"></param>
    /// <returns></returns>
    public bool CheckState(string stateName, string layerName = "Base Layer")
    {

        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }
    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {

        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(tagName);
    }


    public Animator GetAnim()
    {
        return anim;
    }

    private void OnJumpEnter()
    {
        LockDead();
        rigid.AddForce(transform.up * 400f);


    }

    private void OnJumpExit()
    {

    }


    private void OnRollEnter()
    {
        trackDirection = true;
        canAttack = false;

    }
    private void OnGroundEnter()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
        col.material = frictionOne;
        trackDirection = false;
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }
    private void OnGroundExit()
    {
        col.material = frictionZero;
        Physics.gravity = new Vector3(0, -20f, 0);

    }
    private void OnFallEnter()
    {
        LockDead();
    }

    


    private void Attack1Update()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1AVlocity");
        //  anim.SetLayerWeight(anim.GetLayerIndex("Attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.4f));
    }
    private void OnAttack1Enter()
    {
        pi.inputEnabled = false;
        //lerpTarget = 1.0f;
        parical[0].Play();
    }
    private void OnAttack2Enter()
    {
        parical[1].Play();

    }
    private void OnAttack3Enter()
    {
        parical[2].Play();
    }
    //private void OnAttackIdleEnter()
    //{
    //    pi.inputEnabled = true;
    //    //lerpTarget = 0f;

    //}
    //private void OnAttackIdleUpdate()
    //{
    //    anim.SetLayerWeight(anim.GetLayerIndex("Attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("Attack")), lerpTarget, 0.1f));

    //}
    private void OnFlyAttackEnter()
    {
        pi.inputEnabled = false;
        StartCoroutine(FlyTime());
        parical[3].Play();
    }
    private void OnAirAttackEnter()
    {
        //if (firstAir)
        //{
        //    StartCoroutine(AirTime());

        //}
        rigid.useGravity = false;
        rigid.isKinematic = true;
        firstAir = false;
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
    }

    private void OnAirAttackExit()
    {
        rigid.useGravity = true;
        rigid.isKinematic = false;
        firstAir = true;
        pi.inputEnabled = true;
    }


    private void OnFireSwordEnter()
    {
        pi.inputEnabled = false;
    }
    private void OnDownAttackEnter()
    {
        rigid.AddForce(-transform.up*200f);
        rigid.useGravity = true;
    }
    private void OnMagicEnter()
    {
        pi.inputEnabled = false;
    }
    //private void OnDefenseEnter()
    //{
    //    pi.inputEnabled = false;

    //    anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 1f);
    //}

    //private void OnDefenseIdle()
    //{
    //    pi.inputEnabled = true;
    //    anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 0f);
    //}
    private void OnConterBack1Enter()
    {
        pi.inputEnabled = false;
    }
    private void OnConterBack2Enter()
    {
        pi.inputEnabled = false;
    }
    private void OnHitEnter()
    {
        canAttack = false;
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    private void OnHitExit()
    {
        canAttack = true;
    }

    private void OnDieEnter()
    {
        pi.inputEnabled = false;
        cancom.lockState = false;
        planarVec = Vector3.zero;
        m_Respawning = true;
        model.SendMessage("WeaponDisable");
        UIManager.Instance.ShowUI(E_UiId.ResetGameUI);
        Cursor.lockState = CursorLockMode.None;

    }

    private void OnLockEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
    }
    public void OnUpdateRM(object _deltaPos)
    {
       
    }


    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
    public void IssueBool(string triggerName,bool value)
    {
        anim.SetBool(triggerName, value);
    }
   
    private void CheckGrounded()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.8f * 0.5f, -Vector3.up);
        m_Grounded = Physics.Raycast(ray, out hit, 0.8f, Physics.AllLayers,
            QueryTriggerInteraction.Ignore);
    }

    private void CheckSlopeLimit()
    {
       
        if (pi.Dvec.sqrMagnitude < 0.1f)
        {
            return;
        }
        RaycastHit hitinfo;
        var hitAngle = 0f;
        //Physics.Linecast用于确定两个场景对象间是否存在一条连续路径
        if (Physics.Linecast(transform.position + Vector3.up * (col.height * 0.5f), transform.position + pi.Dvec.normalized * (col.radius + 0.2f), out hitinfo, 1<<LayerMask.NameToLayer("Ground")))
        {
            //计算A点与B点以世界坐标原点为夹角的角度
            hitAngle = Vector3.Angle(Vector3.up, hitinfo.normal);

            var targetPoint = hitinfo.point + pi.Dvec.normalized * col.radius;
            if ((hitAngle > slopeLimit) && Physics.Linecast(transform.position + Vector3.up*(col.height*0.5f),targetPoint,out hitinfo,1 << LayerMask.NameToLayer("Ground")))
            {
               
                hitAngle = Vector3.Angle(Vector3.up, hitinfo.normal);
                if (hitAngle > slopeLimit && hitAngle < 85f)
                {
                   stopMove = true;
                    return;
                }
            }
        }
        stopMove = false;
    }
    //用于使角色移动时始终贴着地面
    private bool SnapToGround()
    {
        RaycastHit hit;
        if (stepsSineceLastGrounded > 1)
        {
            return false;
        }
        float speed = rigid.velocity.magnitude;
        if (speed > 100f)
        {
            return false;
        }
        if (!Physics.Raycast(rigid.position,Vector3.down,out  hit,1f,1<<LayerMask.NameToLayer
            ("Ground")))
        {
            return false;
        }
        Debug.Log(hit.normal);
        //stepsSineceLastGrounded = 1;
        //contactNormal = hit.normal;
        float dot = Vector3.Dot(rigid.velocity, hit.normal);
        Debug.Log("dot:" + dot.ToString());
        Debug.Log("velocity:" + rigid.velocity.y.ToString());
        if (dot > 0f)
        {
             rigid.velocity = (rigid.velocity - hit.normal * dot).normalized * speed;
        }
        return true;
    }
    private void StartTimer(MyTime timer, float duration)
    {
        timer.duration = duration;
        timer.Go();
    }
    IEnumerator OpenGravity()
    {
        yield return new WaitForSeconds(1f);
        rigid.useGravity = true;
    }
    IEnumerator FlyTime()
    {
        
        yield return new WaitForSeconds(0.5f);
       //transform.DOJump(new Vector3(transform.position.x,transform.position.y+GameData.airHigh,transform.position.z),0,0, 0.3f);
        rigid.AddForce(transform.up * 450f);
       
    }
    IEnumerator AirTime()
    {
        rigid.useGravity = false;
        rigid.isKinematic = true;
        yield return new WaitForSeconds(1.5f);
        rigid.useGravity = true;
        rigid.isKinematic = false;
    }
    
}
