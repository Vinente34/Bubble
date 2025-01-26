using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSensor : MonoBehaviour
{
    
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

}
