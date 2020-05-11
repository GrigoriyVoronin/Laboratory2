using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    public class DiskTreeTask
    {
        public static IEnumerable Solve(List<string> input)
        {
            var main = new Node(null, "main");
            foreach (var directories in input
                .Select(path => path.Split('\\')))
                AddPathToTree(directories, main);

            return CreateDirectorysEnumerable(main, new List<string>());
        }

        private static List<string> CreateDirectorysEnumerable(Node main, List<string> directorys)
        {
            foreach (var directory in main.SubDirectories)
            {
                directorys.Add(new string(' ', directory.Level - 1) + directory.Name);
                directorys = CreateDirectorysEnumerable(directory, directorys);
            }

            return directorys;
        }

        private static void AddPathToTree(string[] directories, Node main)
        {
            var parent = main;
            var currentNode = main;
            foreach (var directory in directories)
            {
                currentNode = currentNode.SubDirectories.FirstOrDefault(x => x.Name == directory);
                if (currentNode == null)
                {
                    var newNode = new Node(parent, directory);
                    parent.SubDirectories.Add(newNode);
                    currentNode = newNode;
                }

                parent = currentNode;
            }
        }

        private sealed class Node
        {
            public Node(Node parent, string name)
            {
                Name = name;
                Level = parent?.Level + 1 ?? 0;
            }

            public string Name { get; }

            public int Level { get; }

            public SortedSet<Node> SubDirectories { get; } = new SortedSet<Node>(new StrCompareOrd());

            public override string ToString() => Name;
        }

        private sealed class StrCompareOrd : IComparer<Node>
        {
            public int Compare(Node x, Node y) => string.CompareOrdinal(x.Name, y.Name);
        }
    }
}