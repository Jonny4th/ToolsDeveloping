using FacilityRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AgentRelated
{
    public class Person : MonoBehaviour
    {
        [SerializeField] protected string firstName;
        [SerializeField] protected string lastName;
        [SerializeField] protected int age;
    }
}
