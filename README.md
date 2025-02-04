This project implements Dijkstra's Algorithm in C# to find the shortest path between nodes in a weighted graph. Dijkstra's Algorithm is a greedy algorithm that calculates the shortest distance from a starting node to all other nodes by iteratively selecting the nearest unvisited node and updating its neighbors' distances.

Project :
- Graph Representation: The project represents a graph using a Vertex class (nodes) and a Connection class (edges with weights).
- Adjacency Matrix: It builds an adjacency matrix to store the graph's connection costs.
Dijkstra's Algorithm:
- Finds the shortest path between two nodes.
- Uses a priority-based approach to update distances.
- Tracks the previous nodes to reconstruct the shortest path.
Path Display: Shows the computed shortest path with its total cost.
Example Graph: The Main method initializes a graph with labeled vertices and connections with different distances.
Output:
- Prints the adjacency matrix.
- Displays the shortest path from a given start node to a target node.
This implementation is useful for network routing, GPS navigation, and optimization problems where finding the shortest path is essential. 🚀
