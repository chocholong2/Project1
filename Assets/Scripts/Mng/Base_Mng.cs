using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Mng : MonoBehaviour
{
    public static Base_Mng Instance = null;

    private static Pool_Mng s_Pool = new Pool_Mng();
    public static Pool_Mng Pool {get { return s_Pool; }}
    private void Awake()
    {
        Initalize();
    }

    private void Initalize()
    {
        if(Instance == null)
        {
            Instance = this;
            Pool.Initialize(transform);

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject Instantiate_Path(string path)
    {
        return Instantiate(Resources.Load<GameObject>(path));    
    }
}
