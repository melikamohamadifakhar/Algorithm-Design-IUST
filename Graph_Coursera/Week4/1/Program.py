import sys
import queue

def distance(adj, cost, s, t):
    longest = []
    for item in cost:
        longest.extend(item)
    dist = [sum(longest)+1 for i in adj]
    prev = [-1  for i in adj]
    dist[s] = 0
    def ChangePriority(H, v):
        H = sorted(H, key=lambda x:dist[x])
        return H 

    H = [i[0] for i in sorted(enumerate(dist), key=lambda x:x[1])]

    while H:
        u = H[0]
        H.remove(u)
        for i, v in enumerate(adj[u]):
            if dist[v] > dist[u] + cost[u][i]:
                dist[v] = dist[u] + cost[u][i]
                prev[v] = u
                H = ChangePriority(H, v)

    if dist[t] == sum(longest)+1:
        return -1
    return dist[t]


if __name__ == '__main__':
    input = sys.stdin.read()
    data = list(map(int, input.split()))
    n, m = data[0:2]
    data = data[2:]
    edges = list(zip(zip(data[0:(3 * m):3], data[1:(3 * m):3]), data[2:(3 * m):3]))
    data = data[3 * m:]
    adj = [[] for _ in range(n)]
    cost = [[] for _ in range(n)]
    for ((a, b), w) in edges:
        adj[a - 1].append(b - 1)
        cost[a - 1].append(w)
    s, t = data[0] - 1, data[1] - 1
    print(distance(adj, cost, s, t))




# import queue # change to import queue if using python 3
# from queue import PriorityQueue
# class Node(object):
#     def __init__(self, index, distance):
#         self.index = index
#         self.distance = distance
#     def __cmp__(self, other):
#         return cmp(self.distance, other.distance)

# def distance(adj, cost, s, t):
#     #write your code here
#     dist=[float('inf')]*len(adj)
#     dist[s] = 0
#     pq = PriorityQueue()
#     pq.put(Node(s, dist[s]))
#     while not pq.empty():
#         u = pq.get()
#         u_index = u.index
#         for v in adj[u_index]:
#             v_index = adj[u_index].index(v)
#             if dist[v] > dist[u_index] + cost[u_index][v_index]:
#                 dist[v] = dist[u_index] + cost[u_index][v_index]
#                 pq.put(Node(v, dist[v]))
#     if dist[t] == float('inf'):
#         return -1
#     return dist[t]


# if __name__ == '__main__':
#     n, m = map(int, input().split())
#     adj = [[] for _ in range(n)]
#     cost = [[] for _ in range(n)]
#     for i in range(m):
#         a, b, w = map(int, input().split())
#         adj[a - 1].append(b - 1)
#         cost[a - 1].append(w)
#     s, t = map(int, input().split())
#     s, t = s-1, t-1
#     print(distance(adj, cost, s, t))
    
#     '''
#     input = sys.stdin.read()
#     data = list(map(int, input.split()))
#     n, m = data[0:2]
#     data = data[2:]
#     edges = list(zip(zip(data[0:(3 * m):3], data[1:(3 * m):3]), data[2:(3 * m):3]))
#     data = data[3 * m:]
#     adj = [[] for _ in range(n)]
#     cost = [[] for _ in range(n)]
#     for ((a, b), w) in edges:
#         adj[a - 1].append(b - 1)
#         cost[a - 1].append(w)
#     s, t = data[0] - 1, data[1] - 1
#     print(distance(adj, cost, s, t))
#     '''


















# from queue import Priorityqueue
# import heapq
# from black import nullcontext

# class Node:
#     index = 0
#     index = 0
#     adjacents = []
#     Costs = {}
#     visited = False
#     Previous = nullcontext
#     def __init__(self, v, init_dist):
#         self.index = v
#         self.index = init_dist

# def Dijkstra(Nodes, startNode, endNode):
#     p_queue = []
#     for node in Nodes:
#         heapq.heappush(p_queue, (node.index, node))
#     heapq.heapify(p_queue)
#     return p_queue

# //     class Program
# //     {
# //         static double Dijkstra(List<Node> Nodes, Node startNode, Node endNode)
# //         {
# //             SimplePriorityqueue<Node, double> p_queue = new SimplePriorityqueue<Node, double>();
# //             foreach (var node in Nodes){
# //                 p_queue.Enqueue(node, node.index);
# //             }
# //             p_queue.UpdatePriority(startNode, 0);
# //             startNode.index = 0;
# //             while(p_queue.Count != 0)
# //             {
# //                 Node dequeuedNode = p_queue.Dequeue();
# //                 foreach (var node in dequeuedNode.adjacents){
# //                     double newcost = dequeuedNode.index + dequeuedNode.Costs[node.index];
# //                     if(node.index > newcost)
# //                     {
# //                         node.index = newcost;
# //                         node.Previous = dequeuedNode;
# //                         p_queue.UpdatePriority(node, newcost);
# //                     }
# //                 }
# //             }
# //             return endNode.index;
# //         }
# //         static void Main(string[] args)
# //         {
# //             string[] strs = Console.ReadLine().Split();
# //             long nodeCount = Int64.Parse(strs[0]);
# //             long edgeCount = Int64.Parse(strs[1]);
# //             List<Node> Nodes = new List<Node>();
# //             for(int i = 1; i <= nodeCount; i++){
# //                 Nodes.Add(new Node(i, double.PositiveInfinity));
# //             }
# //             for(int i = 0; i < edgeCount; i++)
# //             {
# //                 string[] node = Console.ReadLine().Split();
# //                 long n = Int64.Parse(node[0]);
# //                 long adj = Int64.Parse(node[1]);
# //                 long cost = Int64.Parse(node[2]);
# //                 Nodes[(int)n-1].adjacents.Add(Nodes[(int)adj-1]);
# //                 Nodes[(int)n-1].Costs.Add(adj, cost);
# //             }
# //             string[] start_end = Console.ReadLine().Split();
# //             long StartNode = Int64.Parse(start_end [0]);
# //             long EndNode = Int64.Parse(start_end [1]);
# //             double min_dist = Dijkstra(Nodes, Nodes[(int)StartNode-1], Nodes[(int)EndNode-1]);
# //             if(Double.IsInfinity(min_dist)) { min_dist = -1; }
# //             System.Console.WriteLine((long)min_dist);
# //         }
# //     }
# // }

# n = Node(1, 2)
# x = Node(2, 3)
# list = []
# list.append(x)
# list.append(n)
# q = Dijkstra(list, x, n)
# for i in q:
#     print(str(i[0]) + " " + str(i[1].index))