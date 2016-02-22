using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinity
{
    public interface ICypherable
    {
        string AsCypher { get; }
    }

    public class Node : ICypherable
    {
        protected virtual string DelimiterStart => "(";
        protected virtual string DelimiterEnd => ")";
        
        public string Identifier { get; set; }

        public string Label { get; set; }

        public string AsCypher => ToString();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(DelimiterStart);
            sb.Append(Identifier);
            if (!string.IsNullOrEmpty(Label))
            {
                sb.Append(":");
                sb.Append(Label);
            }
            sb.Append(DelimiterEnd);
            return sb.ToString();
        }
    }

    public class Edge : Node
    {
        protected override string DelimiterStart => "[";
        protected override string DelimiterEnd => "]";
    }

    public enum EdgeArrowHead
    {
        None = 0,
        To,
        From
    }

    public class RelationshipLink : ICypherable
    {
        public EdgeArrowHead Direction { get; set; }

        public Node From { get; set; }

        public ICypherable To { get; set; }

        public Edge Relationship { get; set; }

        public string AsCypher => ToString();

        public RelationshipLink()
        {
            To = new Node();
            From = new Node();
            Relationship = new Edge();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(From);

            if (Direction == EdgeArrowHead.From)
            {
                sb.Append("<");
            }

            sb.Append("-");
            sb.Append(Relationship);
            sb.Append("-");

            if (Direction == EdgeArrowHead.From)
            {
                sb.Append(">");
            }

            sb.Append(To.AsCypher);

            return sb.ToString();
        }
    }

    public class CypherBuilderContext
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }

    public static class CypherBuilder
    {
        public static CypherBuilderContext Relationship(string fromIdentifer, string toIdentifier)
        {
            var rlink = new RelationshipLink();
            rlink.From.Identifier = fromIdentifer;
            rlink.To = new Node {Identifier = toIdentifier};return Relationship(rlink);
        }

        internal static CypherBuilderContext Relationship(RelationshipLink link)
        {
            var c = new CypherBuilderContext();
            c.Value = link.ToString();
            return c;
        }
    }
    
}
