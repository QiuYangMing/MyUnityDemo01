using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    public float horizontalSpeed;
    public float verticalSpeed;
    public bool lockState;
    private float tempEulerx;
    private PlayerInput pi;
    private GameObject playerHandle;
    private GameObject cameraHandle;
    private GameObject modle;
    private Vector3 tempModelEuler;
    private Camera cameraMain;
    private Image lockDot;
    private GameObject slashEffects;
    private GameObject canves;
    private RectTransform locDotTrans;

    /// <summary>
    /// 锁定的物体
    /// </summary>
    public LockTarget lockTarget;
    void Start () {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        pi = playerHandle.GetComponent<PlayerInput>();
        modle = playerHandle.transform.Find("unitychan").gameObject;
        slashEffects = playerHandle.transform.Find("SlashEffects").gameObject;
        tempEulerx = 20f;
        cameraMain = Camera.main;
       
        canves = GameObject.Find("Canvas(Clone)");
        if (canves == null)
        {
            canves = GameObject.Find("Canvas");
        }
        lockDot = GameTool.GetTheChildComponent<Image>(canves, "Img_Lock");
        locDotTrans = lockDot.rectTransform;
        lockDot.enabled = false;
        lockState = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
   
    void FixedUpdate() {
        if (lockTarget == null)
        {
        tempModelEuler  = modle.transform.eulerAngles;
        playerHandle.transform.Rotate(Vector3.up,pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
           

        tempEulerx -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerx = Mathf.Clamp(tempEulerx,-40,30);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerx, 0, 0);

        modle.transform.eulerAngles = tempModelEuler;
        slashEffects.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - modle.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            cameraHandle.transform.LookAt(lockTarget.obj.transform);
            slashEffects.transform.LookAt(lockTarget.obj.transform);
        }

        cameraMain.transform.position = Vector3.Lerp(cameraMain.transform.position, transform.position,0.2f);
        //cameraMain.transform.eulerAngles = transform.eulerAngles;
        cameraMain.transform.LookAt(cameraHandle.transform.position);

    }
    public void LockUnlock()
    {
        if (lockTarget ==null && pi.lockon)
        {
            Vector3 modelOrigin1 = modle.transform.position;
            Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
            Vector3 boxCenter = modelOrigin2 + modle.transform.forward * 5.0f;
            Collider[] cols = Physics.OverlapBox(boxCenter,new Vector3(0.7f,0.7f,7f), modle.transform.rotation,LayerMask.GetMask("Enemy"));
            foreach (var col in cols)
            {
                lockTarget = new LockTarget(col.gameObject, col.bounds.extents.y);
                lockDot.enabled = true;
                lockState = true;
            }

            //if (cols.Length > 0)
            //{
            //    for (int i = 0; i < cols.Length; i++)
            //    {
            //        lockTarget = new LockTarget(cols[0].gameObject, cols[0].bounds.extents.y);
            //        lockDot.enabled = true;
            //        lockState = true;
            //        Debug.Log(cols[i]);
                    
            //    }

            //}
        }
        if (!pi.lockon && lockTarget != null)
        {
            lockTarget = null;
            lockState = false;
            lockDot.rectTransform.localPosition = locDotTrans.localPosition;
            if (!Input.GetMouseButton(1))
            {
            lockDot.enabled = false;

            }

        }
    }

    private void Update()
    {
        if (lockTarget != null)
        {
           
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0,lockTarget.halfHeight,0));
            if (lockTarget.em != null && lockTarget.em.esm.HP <= 0)
            {
                lockTarget = null;
                lockDot.enabled = false;
                lockState = false;
            }
            else if (lockTarget.em == null)
            {
                lockTarget = null;
                lockDot.enabled = false;
                lockState = false;
            }
            else if (Vector3.Distance(modle.transform.position,lockTarget.obj.transform.position)>8.5f)
            {
                lockTarget = null;
                lockDot.enabled = false;
                lockState = false;
            }
       
        }
    }
    public class LockTarget
    {
        public GameObject obj;
        public float halfHeight;
        public EnemyManager em;
        public LockTarget(GameObject _obj,float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
            em = _obj.GetComponent<EnemyManager>();
        }
    }


}
