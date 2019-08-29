using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid_spawn : MonoBehaviour
{

    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefab);
        prefab.transform.position = new Vector3(transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
