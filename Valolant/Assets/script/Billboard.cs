using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //나의 회전방향 = 카메라의 회전방향
        transform.rotation = Camera.main.transform.rotation;
    }
}
