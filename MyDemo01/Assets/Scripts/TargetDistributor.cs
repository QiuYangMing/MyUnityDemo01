using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDistributor : MonoBehaviour {

    public class TargetFollower
    {
        //当有目标被获取时就为真
        public bool requireSlot;
        //如果当前没有分配，是否为-1
        public int assignedSlot;
        //追随者想要达到的目标位置
        public Vector3 requiredPoint;

        public TargetDistributor distributor;

        public TargetFollower(TargetDistributor owner)
        {
            distributor = owner;
            requiredPoint = Vector3.zero;
            requireSlot = false;
            assignedSlot = -1;
        }
    }
    //面积指标
    public int arcsCount;
    protected Vector3[] m_WorldDirection;
    protected bool[] m_FreeArcs;
    protected float arcDegree;

    protected List<TargetFollower> m_Followers;

    public void OnEnable()
    {
        m_WorldDirection = new Vector3[arcsCount];
        m_FreeArcs = new bool[arcsCount];

        m_Followers = new List<TargetFollower>();

        arcDegree = 360.0f / arcsCount;
        Quaternion rotation = Quaternion.Euler(0, -arcDegree, 0);
        Vector3 currentDirection = Vector3.forward;
        for (int i = 0; i < arcsCount; ++i)
        {
            m_FreeArcs[i] = true;
            m_WorldDirection[i] = currentDirection;
            currentDirection = rotation * currentDirection;
        }
    }
    public TargetFollower RegisterNewFollower()
    {
        TargetFollower follower = new TargetFollower(this);
        m_Followers.Add(follower);
        return follower;
    }
    //注销TargetFollower
    public void UnregisterFollower(TargetFollower follower)
    {
        if (follower.assignedSlot != -1)
        {
            m_FreeArcs[follower.assignedSlot] = true;
        }


        m_Followers.Remove(follower);
    }
    //将目标位置分配给追踪者
    private void LateUpdate()
    {
        for (int i = 0; i < m_Followers.Count; ++i)
        {
            var follower = m_Followers[i];

            //我们释放这个追随者可能已经有的任何弧线。
            //如果它仍然需要它，它将被再次挑选下一行。
            //如果它改变了位置，就会选择新的。
            if (follower.assignedSlot != -1)
            {
                m_FreeArcs[follower.assignedSlot] = true;
            }

            if (follower.requireSlot)
            {
                follower.assignedSlot = GetFreeArcIndex(follower);
            }
        }
    }
    public Vector3 GetDirection(int index)
    {
        return m_WorldDirection[index];
    }
    public int GetFreeArcIndex(TargetFollower follower)
    {
        bool found = false;
        Vector3 wanted = follower.requiredPoint - transform.position;
        Vector3 rayCastPosition = transform.position + Vector3.up * 0.4f;

        wanted.y = 0;
        float wanteDistance = wanted.magnitude;

        wanted.Normalize();
        //返回个和之后的一个角度
        float angle = Vector3.SignedAngle(wanted, Vector3.forward, Vector3.up);
        if (angle < 0)
        {
            angle = 360 + angle;
        }
        int wantedIndex = Mathf.RoundToInt(angle / arcDegree);

        if (wantedIndex >= m_WorldDirection.Length)
        {
            wantedIndex -= m_WorldDirection.Length;
        }
        int choosenIndex = wantedIndex;

        RaycastHit hit;
        if (!Physics.Raycast(rayCastPosition,GetDirection(choosenIndex),out hit,wanteDistance))
        {
            found = m_FreeArcs[choosenIndex];
        }
        if (!found)
        {
            //我们要用增大的偏移量来测试左右
            int offset = 1;
            int halfCount = arcsCount / 2;
            while (offset <= halfCount)
            {
                int leftIndex = wantedIndex - offset;
                int rightIndex = wantedIndex + offset;

                if (leftIndex < 0)
                {
                    leftIndex += arcsCount;
                }
                if (rightIndex >= arcsCount)
                {
                    rightIndex -= arcsCount;
                }
                if (!Physics.Raycast(rayCastPosition,GetDirection(leftIndex),wanteDistance) &&
                    m_FreeArcs[leftIndex])
                {
                    choosenIndex = leftIndex;
                    found = true;
                    break;
                }
                if (!Physics.Raycast(rayCastPosition, GetDirection(rightIndex), wanteDistance) &&
                    m_FreeArcs[rightIndex])
                {
                    choosenIndex = rightIndex;
                    found = true;
                    break;
                }
                offset += 1;
            }
        }
        if (!found)
        {
            //我们找不到自由方向，返回-1告诉呼叫的人没有自由空间
            return -1;
        }
        m_FreeArcs[choosenIndex] = false;
        return choosenIndex;
    }
    public void FreeIndex(int index)
    {
        m_FreeArcs[index] = true;
    }

}
