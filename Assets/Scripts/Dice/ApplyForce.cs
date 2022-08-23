using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space"))
        {
            _ApplyForce();
        }
    }

    public void _ApplyForce()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(Vector3.up  * forceMagnitude, ForceMode.Acceleration);
            rigidbody.AddTorque(RandomVector3() * forceMagnitude);
        }
    }

    private Vector3 RandomVector3()
    {
        float x = Random.Range(-1f,1f);
        float y = Random.Range(-1f,1f);
        float z = Random.Range(-1f,1f);

        Vector3 vector = new Vector3(x,y,z).normalized;
        Debug.Log(vector);
        return vector;
    }
}
