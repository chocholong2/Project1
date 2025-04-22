using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform m_Target;
    Vector3 m_TargetPos;
    double m_DMG;
    string m_CharacterName;

    Dictionary<string, GameObject> m_Projectiles = new Dictionary<string, GameObject>();
    Dictionary<string, ParticleSystem> m_Muzzles = new Dictionary<string, ParticleSystem>();

    [SerializeField]
    private float m_Speed;

    private void Awake()
    {
        Transform projectiles = transform.GetChild(0); //각각 불렛트랜스폼의 첫번째 자식
        Transform muzzles = transform.GetChild(1); //두번째 자식 

        for (int i = 0; i < projectiles.childCount; i++)
        {
            m_Projectiles.Add(projectiles.GetChild(i).name,projectiles.GetChild(i).gameObject);

        }
        for(int i = 0; i < muzzles.childCount; i++)
        {
            m_Muzzles.Add(muzzles.GetChild(i).name, muzzles.GetChild(i).GetComponent<ParticleSystem>());
        }
    }
    public void Init(Transform target, double dmg, string Character_Name)
    {
        m_Target = target;
        transform.LookAt(m_Target);
        m_TargetPos = m_Target.position;

        m_DMG = dmg;
        m_CharacterName = Character_Name;
        m_Projectiles[m_CharacterName].gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position =Vector3.MoveTowards(transform.position, m_TargetPos, Time.deltaTime*m_Speed);

        if(Vector3.Distance(transform.position, m_TargetPos) <= 0.1f)
        {
            if(m_Target != null)
            {
                m_Target.GetComponent<Character>().HP -= m_DMG;
                m_Projectiles[m_CharacterName].gameObject.SetActive(false);
                m_Muzzles[m_CharacterName].Play();

                StartCoroutine(ReturnObject(m_Muzzles[m_CharacterName].duration));
            }
        }
    }

    IEnumerator ReturnObject(float timer)
    {
        yield return new WaitForSeconds(timer);
        Base_Mng.Pool.m_pool_Dictionary["Bullet"].Return(this.gameObject);
            
     
    }
}
