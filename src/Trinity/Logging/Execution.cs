using System;
using System.Diagnostics;
using Neo4jClient;
using Neo4jClient.Cypher;
using Neo4jClient.Extension.Cypher;

namespace Trinity
{
    public class Execution
    {
        public DateTimeOffset Timestamp { get; set; }
        
        public string QueryText { get; set; }

        public string FormattedDebugText { get; set; }

        public Exception Expection { get; set; }

        public bool IsSuccess => Expection != null;

        public Execution()
        {
            Timestamp = DateTimeOffset.Now;
        }

        public static Execution FromOperationCompleted(OperationCompletedEventArgs e)
        {
            var execution= new Execution();
            execution.Expection = e.Exception;
            execution.Timestamp = DateTimeOffset.Now;
            execution.QueryText = CypherExtension.GetFormattedCypher(e.QueryText);
            execution.FormattedDebugText = CypherExtension.GetFormattedCypher(e.QueryText); // need to update the neo4jclient to get the debug text
            return execution;
        }
        
        public override string ToString()
        {
            if (!IsSuccess)
            {
                return $"{Expection}\r\n{FormattedDebugText}";
            }
            return FormattedDebugText;
        }
    }
}