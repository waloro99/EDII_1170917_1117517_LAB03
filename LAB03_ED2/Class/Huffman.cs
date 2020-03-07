using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB03_ED2.Class
{
    public class Huffman
    {

        //Internal Class

        internal class Node : IComparable<Node>
        {
            public char? Value { get; set; } //value the character
            public float Probability { get; set; } //frecuenci / total character
            public Node rc { get; set; } //right child
            public Node lc { get; set; } //left child

            //method builder
            public Node(char? v, float p)
            {
                Value = v;
                Probability = p;
            }

            //compare probability the tow nodes
            public int CompareTo(Node other)
            {
                if (this.Probability > other.Probability) return 1;
                else if (this.Probability < other.Probability) return -1;
                else return 0;
            }
        }

        //
        //End internal class




    }
}
