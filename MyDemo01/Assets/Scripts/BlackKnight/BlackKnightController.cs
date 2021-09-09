using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightController : MonoBehaviour
{

    public EnInput ei;
    public Skeletion_AAttack s_AA;
    public GameObject modle;
    //public float disToTarget;
    public float speed;
    public float runSpeed;
    private float airTime;
    public bool canAttack ;
    public bool canRun;
    private bool m_Grounded;
    private Animator anim;
    public Rigidbody rigid;
   
    private Vector3 planarVec;
    private Vector3 thrustVec = Vector3.zero;
    private Vector3 deltaPos;
    private GameObject dieModle;
    private GameObject sensor;
    private GameObject caster;
    void Awake () {

        sensor = transform.Find("sensor").gameObject;
        caster = transform.Find("caster").gameObject;
        ei = GetComponent<BlackKnightInput>();
        if (ei == null)
        {
        ei = GetComponent<Skeletion_AInput>();
            if (ei == null)
            {
                ei = GetComponent<Skeletion_Archer_AInput>();
            }
           
        }
        anim = GetComponentInChildren<Animator>();
        anim.updateMode = AnimatorUpdateMode.AnimatePhysics;
        modle = anim.gameObject;
        rigid = GetComponent<Rigidbody>();
        rigid.interpolation = RigidbodyInterpolation.Interpolate;
        canAttack = true;
        canRun = false;
        
    }
    private void Start()
    {
        SetAttackFunc();
    }
    // Update is called once per frame
    void Update () {
        CheckGrounded();
        anim.SetBool("Grounded", m_Grounded);
        if (!m_Grounded)
        {
            airTime += Time.deltaTime;
        }
        if (airTime >= 3f)
        {
            rigid.useGravity = true;
        }
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        deltaPos = Vector3.zero;
    }


    private void SetAttackFunc()
    {
        if (ei.Player != null && !ei.Player.GetComponent<ActorController>().m_Respawning)
        {
            switch (ei.EnName)
            {
                case "Skeletion_A":
                    if (s_AA == null)
                    {
                        s_AA = GetComponent<Skeletion_AAttack>();
                    }
                    break;
                case "BlackKnight":
                    break;
                default:
                    break;
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

    
    
    public void StartTimer(MyTime timer, float duration)
    {
        timer.duration = duration;
        timer.Go();
    }
    private void OnUpdateRM(object _deltaPos)
    {
        if (CheckStateTag("Attack") )
        {
            deltaPos += (Vector3)_deltaPos;
        }
        if ( CheckStateTag("Shield") || CheckStateTag("Move"))
        {
            deltaPos += (Vector3)_deltaPos;
        }
    }
    
    private void OnEGroundEnter()
    {
        canRun = true;
      
    }
    private void OnBlockedEnter()
    {
        canRun = false;
    }
    private void OnBlockedExit()
    {
        
        caster.GetComponent<BoxCollider>().enabled = false;
        ei.Player.transform.Find("sensor").GetComponent<InteractionManager>().ClearOverlap();
    }
    private void OnAttackExit()
    {
        deltaPos = Vector3.zero;
    }
    private void OnLockEnter()
    {
        sensor.GetComponent<CapsuleCollider>().enabled = false;
        caster.GetComponent<BoxCollider>().enabled = false;
        caster.SetActive(false);
    }

    private void OnLockExit()
    {
        sensor.GetComponent<CapsuleCollider>().enabled = true;
    }
    private void OnAirHitEnter()
    {
        rigid.useGravity = false;
        rigid.isKinematic = true;
    }

    private void OnAirHitExit()
    {
        rigid.useGravity = true;
        rigid.isKinematic = false;
    }

    private void OnEDeshEnter()
    {
        sensor.GetComponent<CapsuleCollider>().enabled = false;
        switch (ei.EnName)
        {
            case "Skeletion_A":
                dieModle = Resources.Load<GameObject>("EnemyHandle_S_ADie");
                break;
            case "BlackKnight":
                dieModle = Resources.Load<GameObject>("BlackKnightDie");
                GameData.Win = true;
                GameData.restPlayer = false;
                UIManager.Instance.ShowUI(E_UiId.ResetGameUI);
                Cursor.lockState = CursorLockMode.None;
                break;
            case "Skeleton_archer_A":
                dieModle = Resources.Load<GameObject>("EnemyHandle_Archer_ADie");
                break;
            default:
                break;
        }
       
        StartCoroutine(EnmeyDie());
    }
   
    IEnumerator EnmeyDie()
    {
        yield return new WaitForSeconds(1.0f);
        Instantiate(dieModle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
    public void IssueBool(string triggerName,bool IsOrNo)
    {
        anim.SetBool(triggerName,IsOrNo);
    }
    public void IssueFloat(string triggerName, float value)
    {
        anim.SetFloat(triggerName, value);
    }

    public Animator GetAnim()
    {
        return anim;
    }
   
    void CheckGrounded()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.8f * 0.5f, -Vector3.up);
        m_Grounded = Physics.Raycast(ray, out hit, 0.8f, Physics.AllLayers,
            QueryTriggerInteraction.Ignore);
    }
}
