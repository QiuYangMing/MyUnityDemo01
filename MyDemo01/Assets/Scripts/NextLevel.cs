using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private AudioManager audioM;
    private void OnEnable()
    {
        audioM = GameObject.Find("Canvas(Clone)").transform.Find("UnitySingletonObj").GetComponent<AudioManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameData.leveName == "level1Enemy")
        {
            Time.timeScale = 0;
            GameData.leveName = "level2Enemy";
            UIManager.Instance.HideSingleUI(E_UiId.InforUI);
            audioM.PlayMusic(4);
            GameSceneManager.Instance.LoadNextSceneAsyn("level02", delegate
            {

                UIManager.Instance.ShowUI(E_UiId.InforUI);
                Time.timeScale = 1;
                GameTool.SetInt("Leve2Enter", 1);
                GameData.leve2Enter = GameTool.GetInt("Leve2Enter");
            });
        }
        else if (GameData.leveName == "level2Enemy")
        {
            Time.timeScale = 0;
            GameData.leveName = "level3Enemy";
            audioM.PlayMusic(6);
            UIManager.Instance.HideSingleUI(E_UiId.InforUI);
            GameSceneManager.Instance.LoadNextSceneAsyn("level03", delegate
            {

                UIManager.Instance.ShowUI(E_UiId.InforUI);
                Time.timeScale = 1;
                GameTool.SetInt("Leve3Enter", 1);
                GameData.leve3Enter = GameTool.GetInt("Leve3Enter");
            });
        }
        
    }

}
