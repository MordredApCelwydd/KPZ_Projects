using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MaksymFedun.RobotChallenge
{
    public class MaksymFedunAlgo : IRobotAlgorithm
    {
        public const int MAX_COLLECT_DISTANCE = 1;

        public int MAX_ROBOT_AMOUNT = 70;

        public bool init = false;

        Dictionary<int, int> robotsToStationIndexes = new Dictionary<int, int>();

        public int RoundCounter { get; set; }
        public string Author { get => "Fedun Maxym"; }
        public MaksymFedunAlgo()
        {
            RoundCounter = 0;
            Robot.Common.Logger.OnLogRound += RountCounterLogger;
        }

        public void RountCounterLogger(object sender, LogRoundEventArgs e)
        {
            RoundCounter++;
        }

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            if (!init)
            {
                for (int i = 0; i < map.Stations.Count; i++)
                {
                    robotsToStationIndexes.Add(i, -1);
                }

                init = true;
            }

            var currentRobot = robots[robotToMoveIndex];

            int currentRobotEnergy = robots[robotToMoveIndex].Energy;

            if (currentRobot.Energy >= 250 && IsCloseToStation(currentRobot, map) && robots.Where(r => r.OwnerName == Author).Count() < MAX_ROBOT_AMOUNT)
            {
  
                //int currentRobotEnergy = robots[robotToMoveIndex].Energy;
                return new CreateNewRobotCommand();
                //return new CreateNewRobotCommand();
            }

            /*
             * if (currentRobot.Energy >= 250 && IsCloseToStation(currentRobot, map) && robots.Where(r => r.OwnerName == Author).Count() < MAX_ROBOT_AMOUNT &&
                    DistanceHelper.FindDistance(NearestFreeStation(robots, robotToMoveIndex, map), currentRobot.Position) < currentRobotEnergy - 150)
            {
  
                //int currentRobotEnergy = robots[robotToMoveIndex].Energy;
                return new CreateNewRobotCommand() { NewRobotEnergy = currentRobotEnergy - 150};
                //return new CreateNewRobotCommand();
            }
             * */

            if (IsOnStation(currentRobot, map))
            {
                return new CollectEnergyCommand();
            }

            if (IsCloseToStation(currentRobot, map) && currentRobot.Energy < 30 && GetNearStation(currentRobot, map).Energy >= 30)
            {
                return new CollectEnergyCommand();
            }

            Position positionToMove = NearestFreeStation(robots, robotToMoveIndex, map);

            if (positionToMove.Equals(currentRobot.Position))
            {
                positionToMove = NearestStationEnemy(robots, robotToMoveIndex, map);
            }

            if (positionToMove.Equals(currentRobot.Position) && IsCloseToStation(currentRobot, map))
            {
                return new CollectEnergyCommand();
            }

            Position nextCell = FindNextCell(robots[robotToMoveIndex], positionToMove);

            return new MoveCommand()
            {
                NewPosition = nextCell,
            };
        }

        public Position NearestFreeStation(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            List<EnergyStation> freeStations = new List<EnergyStation>();

            foreach (EnergyStation station in map.Stations)
            {
                bool free = true;
                foreach (Robot.Common.Robot robot in robots)
                {
                    if ((station.Position.Equals(robot.Position)) ||
                         (robotsToStationIndexes[map.Stations.IndexOf(station)] != -1 &&
                         robotsToStationIndexes[map.Stations.IndexOf(station)] != robotToMoveIndex))
                    {
                        free = false;
                        break;
                    }
                }
                if (free)
                    freeStations.Add(station);
            }

            if (freeStations.Count > 0)
            {
                EnergyStation targetStation = freeStations.OrderBy(s => DistanceHelper.FindDistance(robots[robotToMoveIndex].Position, s.Position)).ElementAt(0);
                robotsToStationIndexes[robotsToStationIndexes.FirstOrDefault(x => x.Value == robotToMoveIndex).Key] = -1;
                robotsToStationIndexes[map.Stations.IndexOf(targetStation)] = robotToMoveIndex;
                return targetStation.Position;
            }
            return robots[robotToMoveIndex].Position;
        }

        public Position NearestStationEnemy(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            List<EnergyStation> enemyStations = new List<EnergyStation>();

            foreach (EnergyStation station in map.Stations)
            {
                foreach (Robot.Common.Robot robot in robots)
                {
                    if (robot.OwnerName != Author)
                    {
                        if (station.Position.Equals(robot.Position) &&
                             !(robotsToStationIndexes[map.Stations.IndexOf(station)] != -1 &&
                        robotsToStationIndexes[map.Stations.IndexOf(station)] != robotToMoveIndex))
                        {
                            enemyStations.Add(station);
                        }
                    }
                }
            }

            if (enemyStations.Count > 0)
            {
                EnergyStation targetStation = enemyStations.OrderBy(s => DistanceHelper.FindDistance(robots[robotToMoveIndex].Position, s.Position)).ElementAt(0);
                robotsToStationIndexes[robotsToStationIndexes.FirstOrDefault(x => x.Value == robotToMoveIndex).Key] = -1;
                robotsToStationIndexes[map.Stations.IndexOf(targetStation)] = robotToMoveIndex;
                return targetStation.Position;
            }
            return robots[robotToMoveIndex].Position;
        }

        public bool IsOnStation(Robot.Common.Robot robot, Map map)
        {
            return map.Stations.Where(s => s.Position.X == robot.Position.X && s.Position.Y == robot.Position.Y).Count() != 0;
        }

        public bool IsCloseToStation(Robot.Common.Robot robot, Map map)
        {
            bool result = false;

            for (int i = -MAX_COLLECT_DISTANCE; i <= MAX_COLLECT_DISTANCE; i++)
            {
                for (int j = -MAX_COLLECT_DISTANCE; j <= MAX_COLLECT_DISTANCE; j++)
                {
                    if (map.Stations.Where(s => s.Position.X + i == robot.Position.X && s.Position.Y + j == robot.Position.Y).Count() != 0)
                    {
                        result = true;
                        break;
                    }
                }
                if (result)
                {
                    break;
                }
            }
            return result;
        }

        public EnergyStation GetNearStation(Robot.Common.Robot robot, Map map)
        {
            for (int i = -MAX_COLLECT_DISTANCE; i <= MAX_COLLECT_DISTANCE; i++)
            {
                for (int j = -MAX_COLLECT_DISTANCE; j <= MAX_COLLECT_DISTANCE; j++)
                {
                    if (map.Stations.Where(s => s.Position.X + i == robot.Position.X && s.Position.Y + j == robot.Position.Y).Count() != 0)
                    {
                        return map.Stations.Where(s => s.Position.X + i == robot.Position.X && s.Position.Y + j == robot.Position.Y).ElementAt(0);
                    }
                }
            }
            return null;
        }

        public Position FindNextCell(Robot.Common.Robot robot, Position target)
        {
            if (robot.Position.Equals(target))
            {
                return robot.Position;
            }
            int steps = CalculateStepsToDestination(robot, target);


            if (steps > 49 - RoundCounter || steps == -1)
            {
                return robot.Position;
            }
            double deltaX = (target.X - robot.Position.X) / (double)steps;
            double deltaY = (target.Y - robot.Position.Y) / (double)steps;

            int newX = (int)Math.Round(robot.Position.X + deltaX);
            int newY = (int)Math.Round(robot.Position.Y + deltaY);

            Position newPosition = new Position(newX, newY);
            return newPosition;
        }

        public int CalculateStepsToDestination(Robot.Common.Robot robot, Position target, bool pushing = false)
        {
            int dist = CalculateManhattanDistance(robot.Position, target);

            if (dist == 0) return 0;

            if (dist > robot.Energy)
            {
                return -1;
            }

            if (dist * dist <= robot.Energy)
            {
                return 1;
            }
            int steps = 2;
            while (steps <= 10)
            {
                List<int> cells = CountEnergyPerStep(dist, steps).ToList();
                int energyNeed = 0;
                foreach (int cell in cells)
                {
                    energyNeed += cell * cell;
                }

                if (pushing)
                {
                    if (energyNeed <= robot.Energy - 10)
                    {
                        return steps;
                    }
                }
                else
                {
                    if (energyNeed <= robot.Energy)
                    {
                        return steps;
                    }
                }
                steps++;
            }

            return -1;
        }
        public static int[] CountEnergyPerStep(int a, int b)
        {
            int quotient = a / b;
            int remainder = a % b; 

            int[] result = new int[b];

            for (int i = 0; i < b; i++)
            {
                result[i] = quotient;
            }

            for (int i = 0; i < remainder; i++)
            {
                result[i]++;
            }

            return result;
        }
        public static int CalculateManhattanDistance(Position a, Position b)
        {
            int deltaX = Math.Abs(a.X - b.X);
            int deltaY = Math.Abs(a.Y - b.Y);

            int distance = deltaX + deltaY;

            return distance;
        }

        public static class DistanceHelper
        {
            public static int FindDistance(Position a, Position b)
            {
                return (int)(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
            }
        }
    }
}
