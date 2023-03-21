using PRN_HE160006;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN_HE160006.DataAccess
{
    public class DAO
    {
        public DAO() { }
        PrnContext context = new PrnContext();
        public List<Room> getListRooms()
        {
            List<Room> rooms = new List<Room>();
            rooms = context.Rooms.ToList();
            return rooms;
        }

        public List<Room> getListSearchRooms(string nameSearch, string statusSearch)
        {
            if (statusSearch != "Tat ca")
            {
                var status = statusSearch.Equals("Trong") ? 0 : 1;
                var rooms = context.Rooms.ToList();
                List<Room> searchRooms = new List<Room>();
                if (nameSearch != null)
                {
                    foreach (var room in rooms)
                    {
                        if (room.Name.Contains(nameSearch) && room.Status == status)
                        {
                            searchRooms.Add(room);
                        }
                    }
                }
                else
                {
                    foreach (var room in rooms)
                    {
                        if (room.Status == status && room.Name.Contains(nameSearch))
                        {
                            searchRooms.Add(room);
                        }
                    }
                }
                return searchRooms;
            }

            else if(statusSearch == "Tat ca")
            {
                var rooms = context.Rooms.ToList();
                List<Room> searchRooms = new List<Room>();
                if (nameSearch != null)
                {
                    foreach (var room in rooms)
                    {
                        if (room.Name.Contains(nameSearch))
                        {
                            searchRooms.Add(room);
                        }
                    }
                    return searchRooms;
                }
                else return getListRooms();
            }
            else return getListRooms();
        }

        // DAO Contract
        public List<Contract> getListContractsByRoomId(int roomId)
        {
            List<Contract> listContracts = new List<Contract>();
            foreach(var contract in context.Contracts)
            {
                if(contract.RoomId == roomId)
                {
                    listContracts.Add(contract);
                }
            }
            return listContracts;
        }
        
    }
}
