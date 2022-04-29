using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUI : MonoBehaviour
{


    #if UNITY_ANDROID || UNITY_IOS

         bool isMobile = true;
    #else

          bool isMobile = false;
    #endif

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(isMobile);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
