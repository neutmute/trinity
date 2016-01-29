using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity
{
    public class ExecutionLog
    {
        #region Singleton

        private static readonly Lazy<ExecutionLog> _lazy = new Lazy<ExecutionLog>(() => new ExecutionLog());

        public static ExecutionLog Instance => _lazy.Value;

        private ExecutionLog()
        {
            _executions = new Queue<Execution>();
        }

        #endregion

        private const int MaxCount = 50;
        private readonly Queue<Execution> _executions;

        public bool Enabled { get; set; }
        
        public void AddIfEnabled(Execution execution)
        {
            if (Enabled)
            {
                _executions.Enqueue(execution);
            }

            // Don't memory leak if someone turns this on in prod
            if (_executions.Count > MaxCount)
            {
                _executions.Dequeue();
            }
        }

        public void Clear()
        {
            _executions.Clear();
        }

        public Execution[] All => _executions.ToArray();
    }
}
