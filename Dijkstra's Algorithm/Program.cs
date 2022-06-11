using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_s_Algorithm
{
    internal class Program
    {
        private static string FindminCostNode(Dictionary<string, int> costs , List<string> processed)
        {
            int minCost = int.MaxValue;
            string minCostNode = null ;

            foreach (KeyValuePair<string, int> node in costs)
            {
                if (node.Value < minCost && processed.Contains(node.Key) == false)
                {
                    minCost = node.Value;
                    minCostNode = node.Key;
                }
            }
            return minCostNode;
        }
        private static void PrintShortestPath(Dictionary<string, int> costs, Dictionary<string, string> parents)
        {
            Console.WriteLine("Cheapest Cost to Reach Finish Node is "+costs["Finish"]);
            Console.WriteLine("Shortest Path to Finish Node is");

            string p = parents["Finish"];

            List<string> shortestPathList = new List<string>();
            GetParentNodesList("Finish", parents, shortestPathList);
            shortestPathList.Reverse();

            foreach(string s in shortestPathList)
            {
                Console.Write(" -> " + s + "\t");
            }
            Console.WriteLine();
        }
        private static void GetParentNodesList(string targetNode, Dictionary<string, string> parents, List<string> shortestPathList)
        {
            shortestPathList.Add(targetNode);

            if (targetNode == "A" || targetNode == "B")
                return;

            string parent = parents[targetNode];

            GetParentNodesList(parent, parents, shortestPathList);
        }

        static void Main(string[] args)
        {
            //          6         1
            //        -----> A ----->
            //       |       ^       |
            //  Start       3|      Finish
            //       |       |       |
            //        -----> B ----->
            //          2         5  
            // 
            // 

            // 1 - Graph Hash Table
            Dictionary<string, Dictionary<string, int>> graph = new Dictionary<string, Dictionary<string, int>>();
            List<string> processed = new List<string>();

            Dictionary<string, int> Start = new Dictionary<string, int>();
            Start["A"] = 6;
            Start["B"] = 2;

            Dictionary<string, int> A = new Dictionary<string, int>();
            A["Finish"] = 1;

            Dictionary<string, int> B = new Dictionary<string, int>();
            B["A"] = 3;
            B["Finish"] = 5;

            Dictionary<string, int> Finish = new Dictionary<string, int>();

            graph["Start"] = Start;
            graph["A"] = A;
            graph["B"] = B;
            graph["Finish"] = Finish;


            // 2 - Costs Hash Table
            Dictionary<string, int> costs = new Dictionary<string, int>();

            costs["A"] = 6;
            costs["B"] = 2;
            costs["Finish"] = int.MaxValue;


            // 3 - parents Hash Table
            Dictionary<string, string> parents = new Dictionary<string, string>();

            parents["A"] = "Start";
            parents["B"] = "Start";
            parents["Finish"] = "none";


            // Dijkstra's code
            string node = FindminCostNode(costs, processed);
            int cost = costs[node];
            int newCost;
            Dictionary<string, int> neighbors = new Dictionary<string, int>();

            while (node != null)
            {
                neighbors = graph[node];

                if(neighbors.Count != 0)
                {
                    foreach (KeyValuePair<string, int> n in neighbors)
                    {
                        newCost = cost + n.Value;

                        if (newCost < costs[n.Key])
                        {
                            costs[n.Key] = newCost;
                            parents[n.Key] = node;
                        }
                    }
                    processed.Add(node);
                    node = FindminCostNode(costs, processed);
                    if (node == null)
                        break;
                    cost = costs[node];
                }
                else
                {
                    processed.Add(node);
                    node = FindminCostNode(costs, processed);
                    if (node == null)
                        break;
                    cost = costs[node];
                }
            }

            PrintShortestPath(costs, parents);
        }
    }
}
