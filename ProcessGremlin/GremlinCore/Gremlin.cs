using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessGremlins
{
    public class Gremlin<T> : IGremlin<T>
    {
        private readonly Action<T> action;

        public Gremlin(Action<T> action)
        {
            this.action = action;
        }

        public void Invoke(T data)
        {
            this.action.Invoke(data);
        }
    }
}
