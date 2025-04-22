using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 몬스터는 여러 마리가 몇 초 마다 수시로 스폰이 되어야 한다.
    

    public int m_Count; //몬스터의 수 
    public int m_SpawnCount; //몇 초마다 

    public static List<Monster> m_Monsters = new List<Monster>();
    public static List<Player> m_Players = new List<Player>();

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }
    IEnumerator SpawnCoroutine()
    {
        Vector3 pos;
        //Random.insideUnitSphere = Vector3(x,y,z)
        //Random.insideUnitCircle = Vector2(x,y)
        for(int i = 0; i < m_Count ; i++) {

            pos = Vector3.zero + Random.insideUnitSphere * 10.0f;
            pos.y = 0.0f;


            while( Vector3.Distance(pos, Vector3.zero) <= 5.0f){ //생성된 위치가 3 보다 가까우면 다시 배치해라. 
                pos = Vector3.zero + Random.insideUnitSphere * 10.0f;
                pos.y = 0.0f;
            }

            var goObj = Base_Mng.Pool.Pooling_OBJ("Monster").Get((value) =>
            {

                value.GetComponent<Monster>().Init();
                value.transform.position = pos;
                value.transform.LookAt(Vector3.zero);
                m_Monsters.Add(value.GetComponent<Monster>());
            });

            
        }
        yield return new WaitForSeconds(m_SpawnCount);
        StartCoroutine(SpawnCoroutine());
    }


}
