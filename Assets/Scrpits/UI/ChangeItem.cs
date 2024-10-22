using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItem : MonoBehaviour
{
    public Washing washing;

    // Start is called before the first frame update
    void Start()
    {
        washing = GameObject.Find("Main Camera").GetComponent<Washing>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWashing()
    {
        washing.canShower = true;
    }
}
