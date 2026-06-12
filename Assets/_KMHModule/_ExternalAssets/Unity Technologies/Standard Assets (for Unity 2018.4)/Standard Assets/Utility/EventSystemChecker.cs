using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemChecker : MonoBehaviour
{
    //public GameObject eventSystem;

	// Use this for initialization
	void Awake ()
	{
	    if(FindAnyObjectByType<EventSystem>() == false)
        {
           //Instantiate(eventSystem);
            GameObject obj = new GameObject("EventSystem");
            obj.AddComponent<EventSystem>();
#pragma warning disable 0618
            obj.AddComponent<StandaloneInputModule>().forceModuleActive = true;
#pragma warning restore 0618
        }
    }
}
