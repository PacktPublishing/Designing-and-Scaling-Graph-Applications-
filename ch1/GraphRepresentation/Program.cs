namespace GraphRepresentation
{
    using System;
    using System.Collections.Generic;

    class GraphAdjucencyList
    {
        public static void Main()
        {
            int vertexCount = 5;
            LinkedList<int>[] linkedList = new LinkedList<int>[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                linkedList[i] = new LinkedList<int>();
            }

            AddEdge(linkedList, 0, 1);
            AddEdge(linkedList, 0, 4);
            AddEdge(linkedList, 1, 2);
            AddEdge(linkedList, 1, 3);
            AddEdge(linkedList, 1, 4);
            AddEdge(linkedList, 2, 3);
            AddEdge(linkedList, 3, 4);

            PrintGraph(linkedList);

            Console.ReadKey();
        }

        static void AddEdge(LinkedList<int>[] adj, int u, int v)
        {
            adj[u].AddLast(v);
            adj[v].AddLast(u);
        }

        static void PrintGraph(LinkedList<int>[] adj)
        {
            for (int i = 0; i < adj.Length; i++)
            {
                Console.WriteLine("\nAdjacency list of vertex " + i);
                Console.Write("head");

                foreach (var item in adj[i])
                {
                    Console.Write(" -> " + item);
                }
                Console.WriteLine();
            }
        }
    }
}
