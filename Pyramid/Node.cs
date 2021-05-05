namespace Pyramid
{
    public interface Node
    {
        Node Left { get; }
        Node Right { get; }

        int Compute();
    }

    public class IntNode : Node
    {
        public int Value { get; }

        public Node Left => null;

        public Node Right => null;

        public IntNode(int value) => Value = value;

        public int Compute() => Value;
    }

    public class PlusNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public PlusNode(Node left, Node right)
        {
            Left = left;
            Right = right;
        }

        public int Compute() => Left.Compute() + Right.Compute();
    }

    public class MinusNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public MinusNode(Node left, Node right)
        {
            Left = left;
            Right = right;
        }

        public int Compute() => Left.Compute() - Right.Compute();
    }

    public class MulNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public MulNode(Node left, Node right)
        {
            Left = left;
            Right = right;
        }

        public int Compute() => Left.Compute() * Right.Compute();
    }

    public class DivNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public DivNode(Node left, Node right)
        {
            Left = left;
            Right = right;
        }

        public int Compute() => Left.Compute() / Right.Compute();
    }

    public class BitwiseShiftNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public bool RightDirection { get; }

        public BitwiseShiftNode(Node left, Node right, bool rightDir = true)
        {
            Left = left;
            Right = right;
            RightDirection = rightDir;
        }

        public int Compute() => RightDirection
            ? Left.Compute() >> Right.Compute()
            : Left.Compute() << Right.Compute();
    }

    public class AndNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public AndNode(Node left, Node right)
        {
            Left = left;
            Right = right;
        }

        public int Compute() => Left.Compute() & Right.Compute();
    }

    public class XorNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public XorNode(Node left, Node right)
        {
            Left = left;
            Right = right;
        }

        public int Compute() => Left.Compute() ^ Right.Compute();
    }

    public class OrNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public OrNode(Node left, Node right)
        {
            Left = left;
            Right = right;
        }

        public int Compute() => Left.Compute() | Right.Compute();
    }
}
