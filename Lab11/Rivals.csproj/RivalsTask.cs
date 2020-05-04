using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Rivals
{
    public class RivalsTask
    {
        private static readonly Dictionary<MoveDirection, Size> OffsetToDirection = new Dictionary<MoveDirection, Size>
        {
            {MoveDirection.Up, new Size(0, -1)},
            {MoveDirection.Down, new Size(0, 1)},
            {MoveDirection.Left, new Size(-1, 0)},
            {MoveDirection.Right, new Size(1, 0)}
        };

        private static bool WalkInDirection(Map map, OwnedLocation position, MoveDirection direction,
            out OwnedLocation newPoint)
        {
            newPoint = new OwnedLocation(position.Owner, position.Location + OffsetToDirection[direction], position.Distance + 1);
            return map.InBounds(newPoint.Location) &&
                   map.Maze[newPoint.Location.X, newPoint.Location.Y] == MapCell.Empty;
        }

        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            var players = map.Players;
            var queues = new List<Queue<OwnedLocation>>();
            var visited = new HashSet<Point>();

            var owned = new HashSet<OwnedLocation>();
            var distanse = 0;
            for (var i = 0; i < players.Length; i++)
            {
                queues.Add(new Queue<OwnedLocation>());
                var p = new OwnedLocation(i, map.Players[i], distanse);
                queues[i].Enqueue(p);
                owned.Add(p);
            }

            while (queues.Count > 0)
            {
                for (var j = 0; j < queues.Count; j++)
                {
                    if (queues[j].Count == 0)
                    {
                        queues.RemoveAt(j);
                        continue;
                    }

                    var currentPoint = queues[j].Dequeue();
                    if (!visited.Add(currentPoint.Location))
                        continue;

                    if (!owned.Contains(currentPoint))
                        continue;

                    yield return currentPoint;

                    for (var i = 0; i < 4; i++)
                        if (WalkInDirection(map, currentPoint, (MoveDirection) i, out var newPoint))
                        {
                            if (!visited.Contains(newPoint.Location) && owned.All(x => x.Location != newPoint.Location))
                            {
                                queues[j].Enqueue(newPoint);
                                owned.Add(newPoint);
                            }
                        }
                }

                distanse++;
            }
        }

        private enum MoveDirection
        {
            Right,
            Up,
            Left,
            Down
        }
    }
}