using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaksymFedun.RobotChallenge;
using Robot.Common;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MaksymFedun.RobotChallenge
{
    [TestClass]
    public class AlgorithmUnitTest
    {
        [TestMethod]
        public void TestDistance()
        {
            var p1 = new Position(1, 1);
            var p2 = new Position(2, 4);

            int distance = MaksymFedunAlgo.DistanceHelper.FindDistance(p1, p2);

            Assert.AreEqual(distance, 10);
        }

        [TestMethod]
        public void TestMove()
        {
            var algoritm = new MaksymFedunAlgo();
            var map = new Map();
            var stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot() { Energy = 100, Position = new Position(5, 1) } };

            var command = algoritm.DoStep(robots, 0, map);


            Assert.IsTrue(command is MoveCommand);
            Assert.AreEqual(stationPosition, ((MoveCommand)command).NewPosition);
        }

        [TestMethod]
        public void UnitCollect()
        {
            var algoritm = new MaksymFedunAlgo();
            var map = new Map();
            var stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot() { Energy = 100, Position = new Position(1, 1) } };

            var command = algoritm.DoStep(robots, 0, map);


            Assert.IsTrue(command is CollectEnergyCommand);
        }

        [TestMethod]
        public void UnitCollectOnDistance()
        {
            var algoritm = new MaksymFedunAlgo();
            var map = new Map();
            var stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot() { Energy = 20, Position = new Position(2, 1) } };

            var command = algoritm.DoStep(robots, 0, map);


            Assert.IsTrue(command is CollectEnergyCommand);
        }

        [TestMethod]
        public void UnitCreateRobot()
        {
            var algoritm = new MaksymFedunAlgo();
            var map = new Map();
            var stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot() { Energy = 350, Position = new Position(1, 1) } };

            var command = algoritm.DoStep(robots, 0, map);


            Assert.IsTrue(command is CreateNewRobotCommand);
        }

        [TestMethod]
        public void UnitGetOutWhenStationNotFree()
        {
            var algoritm = new MaksymFedunAlgo();
            var map = new Map();
            var stationPosition = new Position(1, 1);
            var stationPosition2 = new Position(30, 30);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition2, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot() { Energy = 120, Position = new Position(1, 1) },
                    new Robot.Common.Robot() { Energy = 120, Position = new Position(2, 2) } };

            var command = algoritm.DoStep(robots, 1, map);


            Assert.IsTrue(command is MoveCommand);
        }

        [TestMethod]
        public void UnitAttackWhenNoFreeStations()
        {
            var algoritm = new MaksymFedunAlgo();
            var map = new Map();
            var stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot() { Energy = 120, Position = new Position(1, 1), OwnerName = "John" },
                    new Robot.Common.Robot() { Energy = 120, Position = new Position(2, 2), OwnerName = "Peter" } };

            var command = algoritm.DoStep(robots, 1, map);


            Assert.IsTrue(command is MoveCommand);
            Assert.AreEqual(stationPosition, ((MoveCommand)command).NewPosition);
        }

        [TestMethod]
        public void UnitCollectOnDistanceWhenEnergyLow()
        {
            var algoritm = new MaksymFedunAlgo();
            var map = new Map();
            var stationPosition = new Position(1, 1);
            var stationPosition2 = new Position(10, 10);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition2, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot() { Energy = 20, Position = new Position(1, 1) },
                    new Robot.Common.Robot() { Energy = 20, Position = new Position(2, 1) } };


            var command = algoritm.DoStep(robots, 1, map);

            Assert.IsTrue(command is CollectEnergyCommand);
        }

        [TestMethod]
        public void UnitNoActions()
        {
            var algoritm = new MaksymFedunAlgo();
            var map = new Map();

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot() { Energy = 80, Position = new Position(1, 1) } };


            var command = algoritm.DoStep(robots, 0, map);

            Assert.IsTrue(command is MoveCommand);
            Assert.AreEqual(robots[0].Position, ((MoveCommand)command).NewPosition);
        }

        [TestMethod]
        public void UnitFindNearestFreeStation()
        {
            var algoritm = new MaksymFedunAlgo();
            algoritm.MAX_ROBOT_AMOUNT = 2;
            var map = new Map();
            var stationPosition1 = new Position(1, 1);
            var stationPosition2 = new Position(4, 1);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition1, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition2, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot() { Energy = 200, Position = new Position(2, 4), OwnerName = "Peter" } };


            algoritm.DoStep(robots, 0, map);
            var nearestFreeStation = algoritm.NearestFreeStation(robots, 0, map);

            Assert.AreEqual(nearestFreeStation, stationPosition1);
        }
    }
}
