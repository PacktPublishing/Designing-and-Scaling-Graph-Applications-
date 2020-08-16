namespace GraphColoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// An implementation of the Welch Powell Graph Coloring algorithm
    /// 
    /// Find the degree of each vertex.
    /// List the vertices's in order of descending valence i.e.valence degree(v(i)) >= degree(v(i+1)) .
    /// Color the first vertex in the list.
    /// Go down the sorted list and color every vertex not connected to the colored vertices's above the same color 
    /// then cross out all colored vertices's in the list.
    /// Repeat the process on the uncolored vertices's with a new color-always working 
    /// in descending order of degree until all in descending order of degree until all vertices's are colored.
    /// </summary>
    class Program
    {
        public static void Main()
        {
            Graph graph = new Graph();
            string[] student1 = { "Data Science", "English", "Graph Theory", "Networks" };
            string[] student2 = { "Data Science", "English", "Computer Science", "Philosophy" };
            string[] student3 = { "Graph Theory", "Music", "Philosophy", "Spanish" };
            string[] student4 = { "Graph Theory", "Computer Science", "Maths", "French" };
            string[] student5 = { "English", "Music", "Computer Science", "Maths" };
            string[] student6 = { "Music", "Networks", "Computer Science", "French" };
            string[] student7 = { "Music", "Philosophy", "Maths", "Spanish" };

            graph.AddVertex("Data Science");
            graph.AddVertex("English");
            graph.AddVertex("Graph Theory");
            graph.AddVertex("Networks");
            graph.AddVertex("Computer Science");
            graph.AddVertex("Philosophy");
            graph.AddVertex("Maths");
            graph.AddVertex("French");
            graph.AddVertex("Spanish");
            graph.AddVertex("Music");
            graph.AddEdge(student1);
            graph.AddEdge(student2);
            graph.AddEdge(student3);
            graph.AddEdge(student4);
            graph.AddEdge(student5);
            graph.AddEdge(student6);
            graph.AddEdge(student7);
            Console.WriteLine(graph.ToString());
            ColorGraph color2 = new ColorGraph(graph);
            color2.Color();
            Console.WriteLine(color2);
            Console.Read();
        }
    }

    public class Vertex
    {
        public string Name { get; }

        public Vertex(string name)
        {
            this.Name = name;
            this.Color = 0;
        }

        public void SetColor(int i)
        {
            Color = i;
        }

        public int Color { get; private set; }
    }

    public static class UserInterface
    {
        public static void AddVertices(Graph graph)
        {
            string vertex = "";
            while (vertex != "$")
            {
                Console.WriteLine("Enter name of vertex or $ to continue");
                vertex = Console.ReadLine();
                if (vertex != "$")
                {
                    graph.AddVertex(vertex);
                }
            }
        }

        public static void AddEdge(Graph graph)
        {
            string edges = "";
            while (edges != "$")
            {
                Console.WriteLine("Enter bunch of adjacent nodes separated by commas or $ to continue");
                edges = Console.ReadLine();
                if (edges != "$")
                {
                    string[] adj_nodes = edges.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (adj_nodes.Length > 1)
                        graph.AddEdge(adj_nodes);
                }
            }
        }
    }

    /// <summary>
    /// A representation of the graph data structure as collection of nodes mapped to their adjList with basic operations
    /// </summary>
    public class Graph
    {
        private Dictionary<Vertex, List<Vertex>> graph = new Dictionary<Vertex, List<Vertex>>();
        public List<KeyValuePair<Vertex, int>> degreeList { get; private set; } = new List<KeyValuePair<Vertex, int>>();

        /// <summary>
        /// Adds a new node to graph
        /// </summary>
        /// <param name="vertex">Node to add</param>
        public void AddVertex(string name)
        {
            if (this.ContainsKey(name))
            {
                throw new ArgumentException();

            }
            else
            {
                graph.Add(new Vertex(name), new List<Vertex>());
            }
        }

        /// <summary>
        /// Removes node from graph and all edges connected to it
        /// </summary>
        /// <param name="vertex">node to remove</param>
        public void RemoveVertex(string name)
        {

            if (this.ContainsKey(name))
            {
                foreach (var adjnode in this[name])
                {
                    graph[adjnode].Remove(GetVertexWithName(name));
                }
                graph.Remove(GetVertexWithName(name));
            }
            else
            {
                throw new ArgumentException();

            }
        }

        public bool ContainsKey(string name)
        {
            foreach (var node in graph)
            {
                if (node.Key.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Creates a path between two vertices's if path doesn't already exist 
        /// by adding them to their respective adjacency lists
        /// </summary>
        /// <param name="vertex">first node</param>
        /// <param name="vertex2">second node</param>
        public void addEdge(string vertex, string vertex2)
        {
            if (this.ContainsKey(vertex) && this.ContainsKey(vertex2) && vertex != vertex2)
            {
                if (!this[vertex].Contains(GetVertexWithName(vertex2)) &&
                    !this[vertex2].Contains(GetVertexWithName(vertex)))
                {
                    this[vertex].Add(GetVertexWithName(vertex2));
                    this[vertex2].Add(GetVertexWithName(vertex));
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Vertex GetVertexWithName(string name)
        {
            foreach (var node in graph)
            {
                if (node.Key.Name == name)
                {
                    return node.Key;
                }
            }
            throw new ArgumentException();
        }

        public void AddEdge(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (j != array.Length)
                    {
                        this.addEdge(array[i], array[j]);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the adjacency list of the specified vertex
        /// </summary>
        /// <param name="vertex">specified vertex</param>
        /// <returns>adjacency list of specified vertex</returns>
        public List<Vertex> this[string name]
        {
            get
            {
                foreach (var node in graph)
                {
                    if (node.Key.Name == name)
                    {
                        return graph[node.Key];
                    }
                }
                throw new ArgumentException($"Vertex with name {name} doesn't exist in graph");
            }
        }

        /// <summary>
        /// Gets a collection containing the nodes in the graph
        /// </summary>
        public List<Vertex> Nodes
        {
            get
            {
                return graph.Keys.ToList();
            }
        }

        /// <summary>
        /// Checks adjacency of two vertices's
        /// </summary>
        /// <param name="vertex">first vertex</param>
        /// <param name="vertex2">second vertex</param>
        /// <returns>true if vertices's are adjacent or false if otherwise</returns>
        public bool isAdjacent(string vertex, string vertex2)
        {
            return this[vertex].Contains(GetVertexWithName(vertex2));
        }

        public bool IsAdjacent(Vertex vertex, Vertex vertex2)
        {
            return graph[vertex].Contains(vertex2);
        }

        /// <summary>
        /// Maps the degree of each node appropriately and sorts based on degree
        /// </summary>
        /// <returns>sorted list of node,degree pair based on degree</returns>
        public void GenerateDegreeList()
        {
            var temp = new Dictionary<Vertex, int>();
            foreach (var node in graph)
            {
                temp.Add(node.Key, node.Value.Count);
            }
            degreeList = temp.OrderByDescending(deg => deg.Value).ToList();
        }

        /// <summary>
        /// Prints out graph as adjacency List
        /// </summary>
        /// <returns>String representation of adjacency list</returns>
        public override string ToString()
        {

            var result = new StringBuilder("");
            foreach (var node in graph)
            {
                result.Append($"{node.Key.Name}:{printAdjList(node.Value)}\n");
            }
            return result.ToString();
        }

        private string printAdjList(List<Vertex> list)
        {
            var result = new StringBuilder("[");

            foreach (var element in list)
            {
                result.Append($"{element.Name} ");
            }
            result.Append("]");
            return result.ToString();
        }
    }

    class ColorGraph
    {
        Graph graph;
        public ColorGraph(Graph graph)
        {
            this.graph = graph;
        }

        public List<Vertex> GetNodesWithColor(int color)
        {
            var temp = new List<Vertex>();
            foreach (var node in graph.degreeList)
            {
                if (node.Key.Color == color)
                {
                    temp.Add(node.Key);
                }
            }
            return temp;
        }

        public void Color()
        {
            graph.GenerateDegreeList();
            int color = 1;
            for (int i = 0; i < graph.degreeList.Count; i++)
            {
                if (graph.degreeList.ElementAt(i).Key.Color != 0)
                {
                    continue;
                }
                graph.degreeList.ElementAt(i).Key.SetColor(color);
                for (int j = i + 1; j < graph.degreeList.Count; j++)
                {
                    if (j == graph.degreeList.Count)
                    {
                        continue;
                    }
                    if (graph.IsAdjacent(graph.degreeList.ElementAt(i).Key, graph.degreeList.ElementAt(j).Key) ||
                        graph.degreeList.ElementAt(j).Key.Color != 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (GetNodesWithColor(color).Count == 1)
                        {
                            graph.degreeList.ElementAt(j).Key.SetColor(color);
                        }
                        else
                        {
                            bool isAdj = false;
                            foreach (var node in GetNodesWithColor(color))
                            {
                                if (graph.IsAdjacent(graph.degreeList.ElementAt(j).Key, node))
                                {
                                    isAdj = true;
                                    break;
                                }
                            }
                            if (isAdj == true)
                            {
                                continue;
                            }
                            else
                            {
                                graph.degreeList.ElementAt(j).Key.SetColor(color);
                            }
                        }
                    }
                }
                color++;
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var node in graph.degreeList)
            {
                result.Append($"{node.Key.Name}: {node.Key.Color} \n");
            }
            return result.ToString();
        }
    }
}