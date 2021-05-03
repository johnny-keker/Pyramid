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
}
