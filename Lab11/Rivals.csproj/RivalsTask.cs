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

        private static bool TryGoInDirection(Map map, OwnedLocation position, MoveDirection direction,
            out OwnedLocation potentialPoint)
        {
            potentialPoint = new OwnedLocation(
                position.Owner,
                position.Location + OffsetToDirection[direction],
                position.Distance + 1);
            return map.InBounds(potentialPoint.Location) &&
                   map.Maze[potentialPoint.Location.X, potentialPoint.Location.Y] == MapCell.Empty;
        }

        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            var queues = new List<Queue<OwnedLocation>>();
            var occupied = new HashSet<Point>();
            InitPlayers(queues, occupied, map);
            while (queues.Count(x => x.Count > 0) > 0)
                foreach (var queue in queues.Where(x => x.Count > 0))
                {
                    var currentPoint = queue.Dequeue();
                    if (!occupied.Contains(currentPoint.Location))
                        continue;

                    yield return currentPoint;

                    for (var i = 0; i < 4; i++)
                        if (TryGoInDirection(map, currentPoint, (MoveDirection) i, out var newPoint) &&
                            !occupied.Contains(newPoint.Location) && occupied.Add(newPoint.Location))
                            queue.Enqueue(newPoint);
                }
        }

        private static void InitPlayers(List<Queue<OwnedLocation>> queues, HashSet<Point> occupied, Map map)
        {
            for (var i = 0; i < map.Players.Length; i++)
            {
                queues.Add(new Queue<OwnedLocation>());
                queues[i].Enqueue(new OwnedLocation(i, map.Players[i], 0));
                occupied.Add(map.Players[i]);
            }
        }

        private enum MoveDirection
        {
            Right,
            Up,
            Down,
            Left
        }
    }
}