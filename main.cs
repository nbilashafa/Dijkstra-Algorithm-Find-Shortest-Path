using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestPathFinder
{
    public class Connection
    {
        public int Cost;
        public Vertex? Start;
        public Vertex? End;
    }
    
    public class Vertex
    {
        public string Label;
        public Vertex Self;
        public List<Connection> Connections = new List<Connection>();

        public Vertex(string label)
        {
            Label = label;
            Self = this;
        }
        
        public Vertex Link(Vertex target, int cost)
        {
            Connections.Add(new Connection()
            {
                Start = Self,
                End = target,
                Cost = cost
            });
            
            if (!target.Connections.Exists(c => c.Start == target && c.End == Self))
            {
                target.Link(Self, cost);
            }
            return Self;
        }
    }
    
    class Network
    {
        public Vertex? StartVertex;
        public List<Vertex> AllVertices = new List<Vertex>();

        public Vertex InitializeStart(string label)
        {
            StartVertex = RegisterVertex(label);
            return StartVertex;
        }

        public Vertex RegisterVertex(string label)
        {
            var vertex = new Vertex(label);
            AllVertices.Add(vertex);
            return vertex;
        }

        public int?[,] BuildAdjacencyMatrix()
        {
            int?[,] matrix = new int?[AllVertices.Count, AllVertices.Count];
            for (int i = 0; i < AllVertices.Count; i++)
            {
                Vertex nodeA = AllVertices[i];
                for (int j = 0; j < AllVertices.Count; j++)
                {
                    Vertex nodeB = AllVertices[j];
                    var link = nodeA.Connections.FirstOrDefault(c => c.End == nodeB);
                    matrix[i, j] = link?.Cost ?? 0;
                }
            }
            return matrix;
        }
        
        public int FindClosest(int[] distances, bool[] visited)
        {
            int minValue = int.MaxValue, index = 0;
            for (int i = 0; i < distances.Length; i++)
            {
                if (!visited[i] && distances[i] <= minValue)
                {
                    minValue = distances[i];
                    index = i;
                }
            }
            return index;
        }
        
        public List<int> ComputeShortestPath(int?[,] graph, int start, int target)
        {
            int size = graph.GetLength(0);
            int[] distances = new int[size];
            bool[] visited = new bool[size];
            int[] previous = new int[size];

            for (int i = 0; i < size; i++)
            {
                distances[i] = int.MaxValue;
                visited[i] = false;
                previous[i] = -1;
            }
            distances[start] = 0;

            for (int i = 0; i < size - 1; i++)
            {
                int closest = FindClosest(distances, visited);
                visited[closest] = true;
                for (int j = 0; j < size; j++)
                {
                    if (graph[closest, j] > 0)
                    {
                        int totalCost = distances[closest] + graph[closest, j].Value;
                        if (totalCost < distances[j])
                        {
                            distances[j] = totalCost;
                            previous[j] = closest;
                        }
                    }
                }
            }
            
            if (distances[target] == int.MaxValue)
                return new List<int>();
            
            var path = new LinkedList<int>();
            for (int v = target; v != -1; v = previous[v])
                path.AddFirst(v);
            
            return path.ToList();
        }
        
        public void DisplayMatrix(int?[,] matrix, string[] labels)
        {
            Console.Write("     ");
            foreach (var label in labels)
                Console.Write($" {label} ");
            Console.WriteLine();

            for (int i = 0; i < labels.Length; i++)
            {
                Console.Write($" {labels[i]} | [");
                for (int j = 0; j < labels.Length; j++)
                    Console.Write(matrix[i, j] == null ? "  ," : $" {matrix[i, j]},");
                Console.WriteLine(" ]");
            }
        }

        public void DisplayPath(int?[,] graph, string[] labels, string from, string to)
        {
            int start = Array.IndexOf(labels, from);
            int end = Array.IndexOf(labels, to);
            Console.Write($"Shortest path from [{from} -> {to}] : ");
            var path = ComputeShortestPath(graph, start, end);

            if (path.Count > 0)
            {
                int? totalCost = 0;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    int? segment = graph[path[i], path[i + 1]];
                    totalCost += segment;
                    Console.Write($"{labels[path[i]]} [{segment}] -> ");
                }
                Console.WriteLine($"{labels[end]} (Cost: {totalCost})");
            }
            else
            {
                Console.WriteLine("No path found");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var network = new Network();

            var a = network.InitializeStart("A");
            var b = network.RegisterVertex("B");
            var c = network.RegisterVertex("C");
            var d = network.RegisterVertex("D");
            var e = network.RegisterVertex("E");
            var f = network.RegisterVertex("F");
            var g = network.RegisterVertex("G");
            var h = network.RegisterVertex("H");
            var i = network.RegisterVertex("I");
            var j = network.RegisterVertex("J");
            var k = network.RegisterVertex("K");
            var l = network.RegisterVertex("L");

            a.Link(b, 3).Link(c, 2);
            b.Link(c, 5).Link(d, 2).Link(g, 7);
            c.Link(e, 2).Link(f, 9);
            d.Link(e, 8).Link(f, 1);
            e.Link(g, 3);
            f.Link(g, 6).Link(h, 7).Link(k, 8);
            g.Link(i, 6).Link(j, 9);
            h.Link(i, 7).Link(j, 2);
            i.Link(k, 4);
            j.Link(k, 6).Link(l, 4);
            k.Link(l, 5);
            
            string[] labels = network.AllVertices.Select(v => v.Label).ToArray();
            int?[,] adjMatrix = network.BuildAdjacencyMatrix();

            network.DisplayMatrix(adjMatrix, labels);
            network.DisplayPath(adjMatrix, labels, "B", "K");
        }
    }
}
