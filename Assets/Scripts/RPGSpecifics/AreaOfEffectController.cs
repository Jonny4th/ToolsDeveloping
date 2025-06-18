using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaOfEffectController : MonoBehaviour
{
    public Collider AreaOfEffect;
    public float damage;
    public HashSet<IDamagable> damagables = new HashSet<IDamagable>();

    void Start()
    {
        AreaOfEffect = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<IDamagable>();
        if(health == null) return;
        damagables.Add(health);
    }

    void OnTriggerExit(Collider other)
    {
        var health = other.GetComponent<IDamagable>();
        if(health == null) return;
        damagables.Remove(health);
    }

    //Write effect logic here.
    protected virtual void ApplyEffect(IDamagable damagable)
    {
        damagable.TakeDamage(damage);
    }

    public virtual void TriggerAreaOfEffect()
    {
        damagables.ToList().ForEach(ApplyEffect);
    }
}
