using UnityEngine;

namespace Utilities
{
    public interface IHandler
    {
        public void SetNext(IHandler handler);
        public void Handle(Request request);
    }

    public class Request
    { }

}
