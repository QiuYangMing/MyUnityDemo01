using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class EnInput : MonoBehaviour {

    public float Dup;
    public float Dright;
    public float Dmag;
    public float Jup;
    public float Jright;
    public float battleRange;
    public float attackRange;
    private float dist;           //与玩家间的距离，仅在输入抽象类中可以使用
    public Vector3 Dvec;
    public bool lockon;
    public bool run;
    [Space]
    public GameObject Player;
    public NavMeshAgent navMeshAgent;
    public string EnName;
    public Vector3 requiredPoint;
    protected Vector3 playerPos;
    protected Vector3 creatPoint;
    protected Animator anim;
    protected EnStateManager esm;
    protected BlackKnightController bc;
    [Space]
    private float m_Theta = 0.1f ;
    private Color m_Color = Color.green;

    protected virtual void Awake()
    {
        Player = GameObject.Find("PlayerHandle");
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        navMeshAgent.enabled = false;
        creatPoint = transform.position;
    }
    protected virtual void Start()
    {
        esm = GetComponent<EnStateManager>();
        bc = GetComponent<BlackKnightController>();
    }
   
    protected virtual void FixedUpdate()
    {
        dist = Vector3.Distance(transform.position, Player.transform.position);
        if (dist < battleRange)
        {
            LockPlayer();
            Movement();
        }
        else
        {
            BackCreatPoint();
        }
    }
    protected virtual void OnDrawGizmos()
    {
        Matrix4x4 defaultMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        //设置颜色
        Color defaultColor = Gizmos.color;
        Gizmos.color = m_Color;
        //绘制圆环
        Vector3 beginPoint = Vector3.zero;
        Vector3 firstPoint = Vector3.zero;
        for (float theta = 0; theta < 2 * Math.PI; theta += m_Theta)
        {
            float x = battleRange * Mathf.Cos(theta);
            float y = battleRange * Mathf.Sin(theta);
            Vector3 endPoint = new Vector3(x,0, y);
            if (theta == 0)
            {
                firstPoint = endPoint;
            }
            else
            {
                Gizmos.DrawLine(beginPoint, endPoint);
            }
            beginPoint = endPoint;
        }
        //绘制最后一条线段
        Gizmos.DrawLine(firstPoint, beginPoint);
        //恢复默认颜色
        Gizmos.color = defaultColor;
        //恢复默认矩阵
        Gizmos.matrix = defaultMatrix;
    }
    public virtual void LockPlayer()
    {
        if (Player != null && anim.GetBool("Grounded") && !esm.isAttack && !esm.isBlocked)
        {
            playerPos = Player.transform.position;
            playerPos.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(playerPos);
            Vector3 fromTarget = transform.position - Player.transform.position;
            fromTarget.y = 0;
            requiredPoint = Player.transform.position + fromTarget.normalized * 2f * 0.9f;
            lockon = true;
        }
    }

    public void Movement()
    {
        if (Player != null && !Player.GetComponent<ActorController>().m_Respawning && dist > attackRange && !esm.isAttack && bc.canRun && !esm.isLock && !esm.isHit)
        {

            navMeshAgent.enabled = true;
            navMeshAgent.destination = Player.transform.position;
            anim.SetFloat("forward", Mathf.Clamp(transform.InverseTransformVector(bc.ei.navMeshAgent.desiredVelocity).z, 0, 1));
        }
        else
        {
            navMeshAgent.enabled = false;
            bc.IssueFloat("forward", Mathf.Lerp(anim.GetFloat("forward"), 0, 2f));
        }
    }

    public void BackCreatPoint()
    {
        float distCreatPoint = Vector3.Distance(transform.position, creatPoint);
        if (!esm.isAttack && bc.canRun && !esm.isLock && distCreatPoint > 1f)
        {
        navMeshAgent.enabled = true;
        navMeshAgent.destination = creatPoint;
        bc.IssueFloat("forward", Mathf.Clamp(transform.InverseTransformVector(bc.ei.navMeshAgent.desiredVelocity).z, 0, 1));
        }
        else
        {
            navMeshAgent.enabled = false;

        }
    }


}
