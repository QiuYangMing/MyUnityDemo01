using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowScript : MonoBehaviour
{

    [Header("Bow")]
    public Transform bowModel;//攻击的位置，人物的话用手的位置代替
    private Vector3 bowOriginalPos, bowOriginalRot;
    public Transform bowZoomTransform;
    [Space]

    [Header("Arrow")]
    public GameObject arrowPrefab;
    public Transform arrowSpawnOrigin;//要发射的位置
    public Transform arrowModel;
    public Vector3 arrowOriginalPos;
    [Space]

    [Header("Parameters")]
    public Vector3 arrowImpulse;
    public float timeToShoot;
    public float shootWait;
    public bool canShoot;
    public bool shootRest = false;
    public bool isAiming = false;
    [Space]

    public float zoomInDuration;
    public float zoomOutDuration;

    private float camOriginalFov;
    public float camZoomFov;
    private Vector3 camOriginalPos;
    public Vector3 camZoomOffset;

    [Space]

    [Header("Particles")]
    public ParticleSystem prepareParticles;
    public ParticleSystem aimParticles;
    public GameObject circleParticlePrefab;

    [Space]

    [Header("Canvas")]
    public RectTransform reticle;
    public CanvasGroup reticleCanvas;
    public Image centerCircle;
    public Image img_Fire;
    private Vector2 originalImage;
    private Vector3 firePostion;
    private void Start()
    {
        camOriginalPos = Camera.main.transform.localPosition;
        camOriginalFov = Camera.main.fieldOfView;
        bowOriginalPos = bowModel.transform.localPosition;
        bowOriginalRot = bowModel.transform.localEulerAngles;
        arrowOriginalPos = arrowModel.transform.localPosition;
        reticle = GameTool.GetTheChildComponent<RectTransform>(GameObject.Find("Canvas(Clone)"), "Img_Lock");
        img_Fire = reticle.GetComponent<Image>();
        originalImage = reticle.sizeDelta;
        firePostion = Camera.main.ScreenToWorldPoint(reticle.position);
        //ShowReticle(false, 0);
    }
    public void ShowArrow(bool state)
    {
        bowModel.GetChild(0).gameObject.SetActive(state);
    }
    public IEnumerator PrepareSequence()
    {

        prepareParticles.Play();
        

        yield return new WaitForSeconds(timeToShoot);

        canShoot = true;

        
        aimParticles.Play();
        

    }
    public IEnumerator ShootSequence()
    {

        yield return new WaitUntil(() => canShoot == true);

        shootRest = true;

        isAiming = false;
        canShoot = false;

        //ShowReticle(false, zoomOutDuration);

        //CameraZoom(camOriginalFov, camOriginalPos, bowOriginalPos, bowOriginalRot, zoomOutDuration, true);
        //arrowModel.transform.localPosition = arrowOriginalPos;

        GameObject circles =  Instantiate(circleParticlePrefab, arrowSpawnOrigin.position, Quaternion.identity);

        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnOrigin.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody>().AddForce(transform.forward * arrowImpulse.z + transform.up * arrowImpulse.y, ForceMode.Impulse);
        //ShowArrow(false);

        yield return new WaitForSeconds(shootWait);
        shootRest = false;
        aimParticles.Stop();
        Destroy(circles);
    }
    //public void CameraZoom(float fov, Vector3 camPos, Vector3 bowPos, Vector3 bowRot, float duration, bool zoom)
    //{
    //    Camera.main.transform.DOComplete();
    //    Camera.main.DOFieldOfView(fov, duration);
    //    Camera.main.transform.DOLocalMove(camPos, duration);
    //    bowModel.transform.DOLocalRotate(bowRot, duration).SetEase(Ease.OutBack);
    //    bowModel.transform.DOLocalMove(bowPos, duration).OnComplete(() => ShowArrow(zoom));
    //}
    //public void ShowReticle(bool state, float duration)
    //{
    //    float num = state ? 1 : 0;
    //    reticleCanvas.DOFade(num, duration);
    //    Vector2 size = state ? originalImage / 2 : originalImage;
    //    reticle.DOComplete();
    //    reticle.DOSizeDelta(size, duration * 4);

    //    if (state)
    //    {
    //        centerCircle.DOFade(1, .5f).SetDelay(duration * 3);
    //    }
    //    else
    //    {
    //        centerCircle.DOFade(0, duration);
    //    }
    //}
}
