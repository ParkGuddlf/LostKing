using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    [SerializeField] int mapNum = 0;
    [SerializeField] List<GameObject> mapInfo;
    public bool isSpwan;

    public AudioClip[] clips;
    AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);

        // 맵을 생성
        switch (SceneManager.GetActiveScene().name)
        {
            case "Loading":
                if (isSpwan)
                {
                    isSpwan = false;
                }
                break;

        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {      
        switch (SceneManager.GetActiveScene().name)
        {
            //로딩씬에서 맵번호르르 올린다
            case "Loading":
                if (isSpwan)
                {
                    audioSource.Stop();
                    mapNum++;
                    isSpwan = false;
                }
                break;
            case "Tutorial":
                
                if (mapNum == mapInfo.Count)
                {
                    mapNum = 0;
                }
                //맵이 나와있지않으면 리스트에서 맵을 생성한다
                else if (!isSpwan)
                {
                    //리스트에서 맵생성
                    Instantiate(mapInfo[mapNum]);

                    //마지막 맵에서만 오디오 소스변경
                    if (mapNum < mapInfo.Count-1)
                        audioSource.clip = clips[0];
                    else
                        audioSource.clip = clips[1];
                    isSpwan = true;

                    if (GameManager.instance.clearOrFail.activeSelf == false)
                        audioSource.Play();                    
                }
                if (GameManager.instance.clearOrFail.activeSelf == true)
                    audioSource.Stop();
                break;
        }
    }
}
