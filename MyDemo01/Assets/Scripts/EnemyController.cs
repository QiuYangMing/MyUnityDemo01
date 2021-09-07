using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[DefaultExecutionOrder(-1)]
public class EnemyController : MonoBehaviour {
    /// <summary>
    /// 插入转向
    /// </summary>
    public bool interpolateTurning = false;
    public bool applyAnimationRotation = false;
    public Animator animator { get { return m_Animator; } }
    /// <summary>
    /// 外力
    /// </summary>
    public Vector3 externalForce { get { return m_ExternalForce; } }
    public NavMeshAgent navmeshAgent { get { return m_NavMeshAgent; } }
    public bool followNavmeshAgent { get { return m_FollowNavmeshAgent; } }
    public bool grounded { get { return m_Grounded; } }

    protected NavMeshAgent m_NavMeshAgent;
    protected bool m_FollowNavmeshAgent;
    protected Animator m_Animator;
    protected bool m_UnderExternalForce;
    protected bool m_ExternalForceAddGravity = true;
    protected Vector3 m_ExternalForce;
    protected bool m_Grounded;

    protected Rigidbody m_Rigidbody;

    const float k_GroundedRayDistance = 0.8f;

    private void OnEnable()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponentInChildren<Animator>();
        //当打开时，动画将在物理循环中执行。这只对运动刚体有用
        m_Animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
        m_NavMeshAgent.updatePosition = false;
        m_Rigidbody = GetComponentInChildren<Rigidbody>();
        if (m_Rigidbody == null)
        {
            m_Rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;
        //插值使刚体变得平滑
        m_Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        m_FollowNavmeshAgent = true;
    }

    private void FixedUpdate()
    {
        animator.speed = PlayerInput.Instance != null ? 1.0f : 0.0f;
        CheckGrounded();
        if (m_UnderExternalForce)
            ForceMovement();
    }
    void CheckGrounded()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * k_GroundedRayDistance * 0.5f, -Vector3.up);
        m_Grounded = Physics.Raycast(ray, out hit, k_GroundedRayDistance, Physics.AllLayers,
            QueryTriggerInteraction.Ignore);
    }

    void ForceMovement()
    {
        if (m_ExternalForceAddGravity)
            m_ExternalForce += Physics.gravity * Time.deltaTime;

        RaycastHit hit;
        Vector3 movement = m_ExternalForce * Time.deltaTime;
        if (!m_Rigidbody.SweepTest(movement.normalized, out hit, movement.sqrMagnitude))
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }

        m_NavMeshAgent.Warp(m_Rigidbody.position);
    }
    private void OnAnimatorMove()
    {
        if (m_UnderExternalForce)
            return;
        if (m_FollowNavmeshAgent)
        {
            m_NavMeshAgent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude;
            transform.position = m_NavMeshAgent.nextPosition;
        }
        else
        {
            RaycastHit hit;
            if (!m_Rigidbody.SweepTest(m_Animator.deltaPosition.normalized, out hit,
                m_Animator.deltaPosition.sqrMagnitude))
            {
                m_Rigidbody.MovePosition(m_Rigidbody.position + m_Animator.deltaPosition);
            }
        }
        if (applyAnimationRotation)
        {
            transform.forward = m_Animator.deltaRotation * transform.forward;
        }
    }
    //在需要的时候禁用navmesh
    public void SetFollowNavmeshAgent(bool follow)
    {
        if (!follow && m_NavMeshAgent.enabled)
        {
            m_NavMeshAgent.ResetPath();
        }
        else if (follow && !m_NavMeshAgent.enabled)
        {
            m_NavMeshAgent.Warp(transform.position);
        }

        m_FollowNavmeshAgent = follow;
        m_NavMeshAgent.enabled = follow;
    }

    public void AddForce(Vector3 force, bool useGravity = true)
    {
        if (m_NavMeshAgent.enabled)
            m_NavMeshAgent.ResetPath();

        m_ExternalForce = force;
        m_NavMeshAgent.enabled = false;
        m_UnderExternalForce = true;
        m_ExternalForceAddGravity = useGravity;
    }

    public void ClearForce()
    {
        m_UnderExternalForce = false;
        m_NavMeshAgent.enabled = true;
    }

    public void SetForward(Vector3 forward)
    {
        Quaternion targetRotation = Quaternion.LookRotation(forward);

        if (interpolateTurning)
        {
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
                m_NavMeshAgent.angularSpeed * Time.deltaTime);
        }

        transform.rotation = targetRotation;
    }

    

    public void IssueBool(string triggerName, bool value)
    {
        m_Animator.SetBool(triggerName, value);
    }
}
