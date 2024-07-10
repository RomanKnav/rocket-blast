using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRotation : MonoBehaviour
{
    private Quaternion my_rotation;
    // Start is called before the first frame update
    void Start()
    {
       transform.rotation = Quaternion.identity;
    }
 
    // Update is called once per frame
    void LateUpdate()
    {
 
    }
}
