using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject atkPrefab;
    [SerializeField] GameObject endPotal;
    [SerializeField] ButtonType button;
    [SerializeField] List<Transform> atkSpawnTR = new List<Transform>();

    Rigidbody2D rigi;
    bool isDie;
    public int count;
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        for (int i = 0; i < transform.childCount; i++)
        {
            atkSpawnTR.Add(transform.GetChild(i).transform);            
        }
        
    }
    private void Update()
    {
        if (count == 0)
        {
            count++;
            StartCoroutine(Atk());
            
        }
        else if (count == 2)
        {
            if (!isDie)
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
                GetComponent<SpriteRenderer>().flipY = true;
            }
            StopCoroutine(Atk());
            isDie = true;
            //endPotal.SetActive(true);            
        }
        if (button.laver && count == 1)
            count++;
    }

    IEnumerator Atk()
    {
        while (!isDie)
        {            
          
            rigi.AddForce(new Vector2(100,500));
            yield return new WaitForSeconds(2.0f);
            rigi.AddForce(new Vector2(-100, 500));
            yield return new WaitForSeconds(2.0f);
            Instantiate(atkPrefab.gameObject, atkSpawnTR[Random.Range(0, 2)].position, Quaternion.identity, transform.parent);
            yield return new WaitForSeconds(1.5f);
            Instantiate(atkPrefab.gameObject, atkSpawnTR[Random.Range(0, 2)].position, Quaternion.identity, transform.parent);
            yield return new WaitForSeconds(1.5f);
            Instantiate(atkPrefab.gameObject, atkSpawnTR[Random.Range(0, 2)].position, Quaternion.identity, transform.parent);
            yield return new WaitForSeconds(1.5f);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            
    }
}
