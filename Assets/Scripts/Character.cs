using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // virtual
    Animator animator;
    public double HP;
    public double ATK;
    public float ATK_Speed;

    protected float Attack_Range =3.0f; //공격하는 공격 범위
    protected float target_Range = 5.0f; //추격하는 범위
    protected bool isATTAK;
    protected Transform m_Target;

    [SerializeField]
    private Transform m_BulletTransform;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void InitAttack() => isATTAK = false;
    protected void AnimatorChange(string temp)
    {

        if(temp == "isATTACK")
        {
            animator.SetTrigger("isATTACK");
        }
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMOVE", false);

        animator.SetBool(temp, true);
    }

    protected void Bullet()
    {
        Base_Mng.Pool.Pooling_OBJ("Bullet").Get((value) =>
        {
            value.transform.position = m_BulletTransform.position;
            value.GetComponent<Bullet>().Init(m_Target, 10, "CH_01");
        });
    }
    protected void FindClosetTarget<T>(T[] targets) where T : Component
    {
        var monsters = targets;
        Transform closetTarget = null;
        float maxDistance = target_Range;

        foreach(var monster in monsters) //5.0f
        {
            float targetDistance = Vector3.Distance(transform.position, monster.transform.position);

            if(targetDistance < maxDistance)
            {
                closetTarget = monster.transform;
                maxDistance = targetDistance;
            }
        }
        m_Target = closetTarget;
        if(m_Target != null) { transform.LookAt(m_Target.position); }
    }
}
