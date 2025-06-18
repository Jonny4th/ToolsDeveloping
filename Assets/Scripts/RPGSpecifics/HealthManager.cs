using System;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

public class HealthManager : MonoBehaviour, IDamagable, IValueDisplayable, IHandler
{
    [SerializeField]
    private float m_CurrentHealth;

    [SerializeField]
    private float m_MaxHealth;

    DefensiveStatusManager m_DefensiveStatus;

    public float CurrentValue => m_CurrentHealth;

    public float MaxValue => m_MaxHealth;

    public event Action ValueChanged;

    public UnityEvent<object> DamageTaken;

    void Awake()
    {
        SetDamageCalculationFlow();
    }

    public void TakeDamage(float damage)
    {
        var request = new DamageReceiveRequest(damage);
        m_DefensiveStatus.Handle(request);
    }

    public void ValidateValue()
    {
        if(m_CurrentHealth < 0) m_CurrentHealth = 0;
        if(m_CurrentHealth > m_MaxHealth) m_CurrentHealth = m_MaxHealth;
    }

    public void SetNext(IHandler handler) { }

    public void Handle(Request request)
    {
        if(request is not DamageReceiveRequest req) return;

        m_CurrentHealth -= req.Damage;
        ValidateValue();
        ValueChanged?.Invoke();
        DamageTaken?.Invoke(this);
    }

    //Manage Damage calculation flow here.
    void SetDamageCalculationFlow()
    {
        m_DefensiveStatus = GetComponent<DefensiveStatusManager>();
        m_DefensiveStatus.SetNext(this);
    }
}

public class DamageReceiveRequest : Request
{
    public float Damage { get; private set; }

    public DamageReceiveRequest(float damage)
    {
        Damage = damage;
    }

    public void SetDamage(float damage)
    {
        Damage = damage;
    }
}
