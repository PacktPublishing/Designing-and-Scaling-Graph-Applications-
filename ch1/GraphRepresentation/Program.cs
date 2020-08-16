namespace GraphRepresentation
{
    using System;
    using System.Collections.Generic;

    class Graph
    {
        public static void Main()
        {
            // Creating a graph with 5 vertices's
            int V = 5;
            LinkedList<int>[] adj = new LinkedList<int>[V];

            for (int i = 0; i < V; i++)
            {
                adj[i] = new LinkedList<int>();
            }

            // Adding edges one by one
            AddEdge(adj, 0, 1);
            AddEdge(adj, 0, 4);
            AddEdge(adj, 1, 2);
            AddEdge(adj, 1, 3);
            AddEdge(adj, 1, 4);
            AddEdge(adj, 2, 3);
            AddEdge(adj, 3, 4);

            PrintGraph(adj);

            Console.ReadKey();
        }

        // A utility function to add an edge in an undirected graph
        static void AddEdge(LinkedList<int>[] adj, int u, int v)
        {
            adj[u].AddLast(v);
            adj[v].AddLast(u);
        }

        // A utility function to print the adjacency list representation of graph
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
