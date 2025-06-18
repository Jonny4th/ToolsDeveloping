using UnityEngine;

namespace PhysicsRelated
{
    public class CenterOfMass : MonoBehaviour
    {
        public Vector3 m_CenterOfMass = new();
        Rigidbody m_Rigidbody;

        // Start is called before the first frame update
        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            m_Rigidbody.centerOfMass = m_CenterOfMass;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position + m_CenterOfMass, 1f);
        }
    }
}
