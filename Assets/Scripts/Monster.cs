using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public float m_Speed;
    Animator animator;

    bool isSpawn =false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Init()
    {
        StartCoroutine(Spawn_Start());
    }

    IEnumerator Spawn_Start()
    {
        float current = 0.0f;
        float percent = 0.0f;
        float start = 0.0f;
        float end = transform.localScale.x;
        while(percent < 1) { 
            current += Time.deltaTime;
            percent = current / 1.0f;
            float LerpPos = Mathf.Lerp(start, end, percent);
            transform.localScale = new Vector3(LerpPos, LerpPos, LerpPos);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
        isSpawn = true; 
    }

    private void Update()
    {
       
        transform.LookAt(Vector3.zero);

        if (isSpawn == false) return; //isSpawn이 true이면 즉, 몸집이 다 커지고 나서 이동하게 한다. 
        float targetdistance = Vector3.Distance(transform.position, Vector3.zero);
        if(targetdistance <= 0.5f)
        {
            AnimatorChange("isIDLE");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * m_Speed);
            AnimatorChange("isMOVE");
        }
       
    }

    private void AnimatorChange(string temp)
    {
        animator.SetBool("isIDLE",false);
        animator.SetBool("isMOVE",false);

        animator.SetBool(temp, true);
    }


}
