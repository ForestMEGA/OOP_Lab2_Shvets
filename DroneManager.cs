using System;
using System.Collections.Generic;

namespace Lab2
{
    public class DroneManager
    {
        private List<Drone> droneList = new List<Drone>();
        private int currentId = 1;

        public void AddNewDrone(string model, double capacity, double speed)
        {
            Drone newDrone = new Drone(currentId, model, capacity, speed);
            droneList.Add(newDrone);
            currentId = currentId + 1;
        }

        public List<Drone> GetList()
        {
            return droneList;
        }

        public Drone FindById(int id)
        {
            for (int i = 0; i < droneList.Count; i++)
            {
                if (droneList[i].Id == id)
                {
                    return droneList[i];
                }
            }
            return null;
        }

        public bool RemoveDrone(int id)
        {
            Drone target = FindById(id);
            if (target != null)
            {
                droneList.Remove(target);
                return true;
            }
            return false;
        }
    }
}
