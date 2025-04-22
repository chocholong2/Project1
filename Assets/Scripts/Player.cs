using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    Vector3 startPos;
    Quaternion rot;

    
    protected override void Start()
    {
        base.Start();

        startPos = transform.position;
        rot = transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if(m_Target == null)
        {
            FindClosetTarget(Spawner.m_Monsters.ToArray());
            float targetPos = Vector3.Distance(transform.position, startPos);
            if(targetPos > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime);
                transform.LookAt(startPos);
                AnimatorChange("isMOVE");
            }
            else
            {
                transform.rotation = rot;
                AnimatorChange("isIDLE");
            }
            return;
        }
        float targetDistance = Vector3.Distance(transform.position, m_Target.position);
        if(targetDistance <= target_Range && targetDistance > Attack_Range && isATTAK == false) // 추격범위안에는 있지만 공격범위안에는 없을때
        {
            AnimatorChange("isMOVE");
            transform.LookAt(m_Target.position);
            transform.position =Vector3.MoveTowards(transform.position,m_Target.position, Time.deltaTime);
        }else if(targetDistance <= Attack_Range&&isATTAK ==false)
        {
            isATTAK = true;
            AnimatorChange("isATTACK");
            Invoke("InitAttack", 1.0f); //함수를 몇 초 뒤에 실행시켜라 
        }
    }
}
