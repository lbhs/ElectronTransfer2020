using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaO_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float Vx = Random.Range(1f, +2f);
        float coinFlip = Random.Range(0, 1);
        if (coinFlip >0.5f)
        {
            Vx = Vx * -1;
        }
        float Vy = Random.Range(-1f, -2f);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Vx, Vy, 0);
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
