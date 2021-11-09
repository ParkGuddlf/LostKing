using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }    

    public GameObject[] keyParent;
    public GameObject[] keychild;
    public GameObject clearOrFail;
    public Vector3 savePoint;

    public bool playerHold; //플레이어 잠금
    Player player;

    [SerializeField] GameObject pausePanel;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        savePoint = Vector3.zero;
        playerHold = true;
    }
    private void Update()
    {
        if (player.isDie)
        {
            clearOrFail.SetActive(true);
            GameObject fail = clearOrFail.transform.Find("Fail").gameObject;
            fail.SetActive(true);
        }
        else if (player.isClear)
        {
            clearOrFail.SetActive(true);
            GameObject clear = clearOrFail.transform.Find("Clear").gameObject;
            clear.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //캐릭터 사망 재시작
    public void Restart()
    {       
        //플레이어 위치를 세이브 포인트
        player.transform.position = savePoint;
        //Clear/Fail Canvas off
        clearOrFail.SetActive(false);
        //플레이어 Die Clear 불값 false;
        player.isDie = false;
        player.isClear = false;
        //맵 리스폰 키캡위치 원위치        
        GameObject map = FindObjectOfType<Grid>().gameObject;
        Destroy(map);
        MapManager.instance.isSpwan = false;
        foreach (var item in keyParent)
        {
            item.GetComponent<Button>().onClick.Invoke();
        }        

    }
    public void NextScene()
    {
        Loading.LoadScene("Tutorial");
    }
    public void MoveToMain()
    {
        SceneManager.LoadScene("Main");
    }
    //이동키 클릭 원위치
    public void Button(int num)
    {   
        if(keyParent[num] != keychild[num].transform.parent)
            keychild[num].transform.SetParent(keyParent[num].transform);
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

}
