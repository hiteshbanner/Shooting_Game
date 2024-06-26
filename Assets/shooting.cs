using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
   
    public Transform camera;
    public GameObject explosion;
    public AudioSource popSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot(){
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position,camera.transform.forward,out hit)){
            Destroy(hit.transform.gameObject);
            Instantiate(explosion,hit.transform.position,Quaternion.identity);
            popSound.Play();
        }
    }
}
