using UnityEngine;
using Utilities;

public class DefensiveStatusManager : MonoBehaviour, IHandler
{
    [SerializeField]
    int m_BaseDefense; // to physical

    public int Defense => m_BaseDefense;

    IHandler nextHandler;

    public void Handle(Request request)
    {
        if(nextHandler == null) return;

        if(request is not DamageReceiveRequest req) return;

        var input = req.Damage;
        var output = input - Defense;

        req.SetDamage(output);

        nextHandler.Handle(req);
    }

    public void SetNext(IHandler handler)
    {
        nextHandler = handler;
    }
}
