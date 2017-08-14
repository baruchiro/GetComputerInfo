using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddToComputersDB.Models
{
    public partial class Memories : IMyEntity
    {
        public override string GetComponentType()
        {
            return "Memory";
        }
        public override void Create()
        {
            id = Program.db.GetNewID();
            Console.WriteLine("CHECK:\t" + id);

            Console.Write("Enter Capacity (MB): ");
            capacity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter memory type (eg:");
            foreach (string s in Program.db.Memories.Select(m => m.type).Distinct())
            {
                Console.Write(", " + Program.RTL(s));
            }
            Console.Write("): ");
            type = Console.ReadLine();

            Console.Write("Enter manufcator: ");
            manufcator = Console.ReadLine();

            Console.Write("Enter Computer type:\n0. For Laptop\n1. For PC\n>");
            computerType = Console.ReadLine()[0] == '1';

            Console.WriteLine("Enter MoBo ID:");
            foreach (MoBos mobo in Program.db.MoBos)
                Console.WriteLine(mobo.id + ". For " + mobo.ToString());
            string result = Console.ReadLine();
            moboID = result == null || result.Length == 0 ? 0 : Convert.ToInt32(result);


            Console.Write("Free to use?:\n0. For False\n1. For True\n>");
            freeToUse = Console.ReadLine()[0] == '1';
        }

        public override string GetFullDetails(string tab)
        {
            string result = base.GetFullDetails(tab);
            MoBos mobo = Program.db.MoBos.Find(moboID);
            if (mobo != null)
                result += mobo.GetFullDetails(tab + "\t");
            return result;
        }

        public override void GetMissData()
        {
            Console.WriteLine(ToString());

            if (capacity == 0)
            {
                Console.WriteLine("Capacity is 0. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Capacity (MB): ");
                    capacity = Convert.ToInt32(Console.ReadLine());
                }
            }

            if (!freeToUse.HasValue)
            {
                Console.WriteLine("Free to use has empty. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Free to use?:\n0. For False\n1. For True\n>");
                    freeToUse = Console.ReadLine()[0] == '1';
                }
            }
        }

        public override bool Search(string word)
        {
            return
                type.Contains(word) ||
                manufcator.Contains(word);
        }

        public override void SetFreeToUse(bool freeToUse)
        {
            this.freeToUse = freeToUse;
        }

        public override string ToString()
        {
            return id + "\t" + type + " " + capacity + "MB";
        }

        public override void Update()
        {
            if (id == 0)
            {
                Console.WriteLine("This component is not exist!");
            }
            else
            {
                Console.WriteLine("capacity: " + capacity + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Capacity (MB): ");
                    capacity = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("memory type: " + Program.RTL(type) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter memory type (eg:");
                    foreach (string s in Program.db.Memories.Select(m => m.type).Distinct())
                    {
                        Console.Write(", " + Program.RTL(s));
                    }
                    Console.Write("): ");
                    type = Console.ReadLine();
                }

                Console.WriteLine("manufcator: " + Program.RTL(manufcator) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter manufcator: ");
                    manufcator = Console.ReadLine();
                }

                Console.WriteLine("computer type: " + (computerType ? "PC" : "Laptop") + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Computer type:\n0. For Laptop\n1. For PC\n>");
                    computerType = Console.ReadLine()[0] == '1';
                }

                Console.WriteLine("mobo: " + Program.db.MoBos.Find(moboID).ToString() + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.WriteLine("Enter MoBo ID:");
                    foreach (MoBos mobo in Program.db.MoBos)
                        Console.WriteLine(mobo.id + ". For " + mobo.ToString());
                    moboID = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("Free to use: " + freeToUse.Value + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Free to use?:\n0. For False\n1. For True\n>");
                    freeToUse = Console.ReadLine()[0] == '1';
                }
            }
        }
    }

    public partial class MoBos : IMyEntity
    {
        public override string GetComponentType()
        {
            return "MoBo";
        }
        private ComponentsDBEntities db;
        public override void Create()
        {
            id = Program.db.GetNewID();
            Console.WriteLine("CHECK:\t" + id);

            Console.Write("Enter manufcator: ");
            manufcator = Console.ReadLine();

            Console.Write("Enter model: ");
            model = Console.ReadLine();

            Console.Write("Enter number Of slots: ");
            slots = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter number of Channels: ");
            Channels = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter memory type (eg:");
            foreach (string s in Program.db.Memories.Select(m => m.type).Distinct())
            {
                Console.Write(", " + Program.RTL(s));
            }
            Console.Write("): ");
            memoryType = Console.ReadLine();

            Console.Write("Enter memory Max size: ");
            memoryMaxSize = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Computer type:\n0. For Laptop\n1. For PC\n>");
            computerType = Console.ReadLine()[0] == '1';

            /*Console.WriteLine("Enter disk ID:");
            foreach (Disks disk in Program.db.Disks)
                Console.WriteLine(disk.id + ". For " + disk.ToString());
            diskID = Convert.ToInt32(Console.ReadLine());*/

            Console.WriteLine("Enter processor ID:");
            foreach (Processors processor in Program.db.Processors)
                Console.WriteLine(processor.id + ". For " + processor.ToString());
            proessorID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter computer ID:");
            foreach (Computers computer in Program.db.Computers)
                Console.WriteLine(computer.id + ". For " + computer.ToString());
            computerID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter number of Sata connectors: ");
            SataConnectors = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Architacture:\n0. For 32 bit\n1. For 64 bit\n>");
            architacture = Console.ReadLine()[0] == '1';

            int socketID = 1;
            while (socketID != 0)
            {
                Console.WriteLine("Enter MoBo socket:");
                foreach (Sockets socket in Program.db.Sockets)
                    Console.WriteLine(socket.Id + ". For " + socket.ToString());
                Console.Write("For end enter 0: ");

                socketID = Convert.ToInt32(Console.ReadLine());
                if (socketID != 0)
                    MobosSockets.Add(new MobosSockets
                    {
                        MoboID = id,
                        SocketID = socketID
                    });
            }


        }

        public override void PrintRecommendations()
        {
            db = Program.db;

            Console.WriteLine("Recommendations for:\t" + ToString() + "\n");

            Console.WriteLine("Processors:");
            PrintProcessorsRecommendations("\t");

            Console.WriteLine("Memories:");
            PrintMemoriesRecommendations("\t");

            Console.WriteLine("Disks:");
            PrintDisksRecommendations("\t");
        }

        private void PrintProcessorsRecommendations(string tab)
        {
            List<Processors> exact = new List<Processors>();

            List<Processors> check = db.Processors.Where(
                p =>
                p.architacture == architacture &&
                p.id != proessorID &&
                p.freeToUse.Value
                ).OrderByDescending(p => p.rank).ToList();

            foreach (Processors processor in check)
            {
                foreach (int socket in processor.ProcessorSocket.Select(s => s.SocketID))
                {
                    if (this.MobosSockets.Where(s => s.SocketID == socket).Count() > 0)
                    {
                        exact.Add(processor);
                    }
                }
            }

            exact = exact.Distinct().ToList();

            if (proessorID != 0 && proessorID != null)
            {
                exact = exact.Where(p =>
                    p.rank > db.Processors.Find(proessorID).rank
                ).ToList();
            }

            foreach (Processors processor in exact)
            {
                Console.WriteLine(tab + "REPLACE TO:\t" + processor.ToString());
            }
            //return new Recommend<Processors> { };
        }

        private void PrintMemoriesRecommendations(string tab)
        {
            /**
             * Memory sould be free to use, with same ComputerType, same DDR, and more capacity
             */

            List<Memories> exact = db.Memories.Where(m =>
            m.freeToUse.Value &&                                  //free to use
            m.computerType == computerType &&          //same computer type
            m.type.Equals(memoryType) &&               //same DDR
            m.moboID != id                              //without my memories
            ).OrderByDescending(m => m.capacity)          //OrderBy Capacity
            .ToList();


            //print Replaceble

            //Move on exist memories
            foreach (Memories myMemory in db.Memories.Where(m => m.moboID == id))
            {
                //find all best memories for this memrie
                foreach (Memories recommendMemory in exact.Where(e => e.capacity > myMemory.capacity))
                {
                    Console.WriteLine(tab + "EXIST: " + myMemory.ToString() + "\tREPLACE WITH\t" + recommendMemory.ToString());
                }
            }

            //print adding- Take the best memories, count of free socets
            foreach (Memories add in exact.Take(slots.Value - db.Memories.Where(m => m.moboID == id).Count()))
            {
                Console.WriteLine(tab + "ADD:\t" + add.ToString());
            }
        }

        private void PrintDisksRecommendations(string tab)
        {


            List<Disks> myDisks = db.Disks.Where(d => d.moboID == id).ToList();//.Find(diskID);

            List<Disks> exact = db.Disks.Where(
                    d =>
                    d.computerType == computerType &&
                    d.freeToUse.Value &&
                    d.moboID != id)
                    .OrderBy(d => d.type)                 //OrderBy SSD
                    .ThenByDescending(d => d.capacity)    //OrderBy Capacity
                    .ToList();

            if (exact.Count > 0)
            {
                foreach (Disks myOneDisk in myDisks)
                {
                    foreach (Disks recommendDisk in exact.Where(
                        d => d.capacity > myOneDisk.capacity ||
                        (!myOneDisk.type.Value && d.type.Value)))
                    {
                        Console.WriteLine(tab + "EXIST: " + myOneDisk.ToString() + "\tREPLACE WITH\t" + recommendDisk.ToString());
                    }
                }

                if (SataConnectors.HasValue)    //For add more Disks
                {
                    foreach (Disks recommendDisk in exact.OrderBy(d => d.type.Value)
                        .Take(SataConnectors.Value - myDisks.Count()))
                    {
                        Console.WriteLine(tab + "ADD: " + recommendDisk.ToString());
                    }
                }
            }
        }

        public override string GetFullDetails(string tab)
        {
            string result = base.GetFullDetails(tab);
            tab += "\t";

            foreach (Disks disk in Program.db.Disks.Where(d => d.moboID == id))
            {
                result += tab + "Disk: " + disk.ToString() + "\n";
            }
            Processors p = Program.db.Processors.Find(proessorID);
            result += tab + "Processor: " + (p == null ? "No one" : p.ToString()) + "\n";

            foreach (Memories memo in Program.db.Memories.Where(m => m.moboID == id))
                result += tab + "Memory: " + memo.ToString() + "\n";

            if (computerID.HasValue)
            {
                Computers myComp = Program.db.Computers.Find(computerID);
                result += tab + "Computer: " + (myComp == null ? "NOT EXIST" : myComp.ToString()) + "\n";
            }
            return result;
        }

        public override void GetMissData()
        {
            Console.WriteLine(ToString());

            if (!slots.HasValue || slots.Value == 0)
            {
                Console.WriteLine("slots is 0 or null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter number Of slots: ");
                    slots = Convert.ToInt32(Console.ReadLine());
                }
            }

            if (!Channels.HasValue || Channels.Value == 0)
            {
                Console.WriteLine("Channels is 0 or null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter number Of Channels: ");
                    Channels = Convert.ToInt32(Console.ReadLine());
                }
            }

            if (!Channels.HasValue || Channels.Value == 0)
            {
                Console.WriteLine("Channels is 0 or null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter number Of Channels: ");
                    Channels = Convert.ToInt32(Console.ReadLine());
                }
            }

            if (!memoryMaxSize.HasValue || memoryMaxSize.Value == 0)
            {
                Console.WriteLine("memoryMaxSize is 0 or null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter memory Max size: ");
                    memoryMaxSize = Convert.ToInt32(Console.ReadLine());
                }
            }

            if (!computerType.HasValue)
            {
                Console.WriteLine("computer type is null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Computer type:\n0. For Laptop\n1. For PC\n>");
                    computerType = Console.ReadLine()[0] == '1';
                }
            }

            if (!architacture.HasValue)
            {
                Console.WriteLine("architacture id is null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Architacture:\n0. For 32 bit\n1. For 64 bit\n>");
                    architacture = Console.ReadLine()[0] == '1';
                }
            }

            if (!SataConnectors.HasValue || SataConnectors.Value == 0)
            {
                Console.WriteLine("Sata Connectors is null or 0. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter number of Sata connectors: ");
                    SataConnectors = Convert.ToInt32(Console.ReadLine());
                }
            }

            if (MobosSockets.Count == 0)
            {
                Console.WriteLine("No MoBo Socets. Add? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    int socketID = 1;
                    while (socketID != 0)
                    {
                        Console.WriteLine("Enter MoBo socket:");
                        foreach (Sockets socket in Program.db.Sockets)
                            Console.WriteLine(socket.Id + ". For " + socket.ToString());
                        Console.Write("For end enter 0: ");

                        socketID = Convert.ToInt32(Console.ReadLine());
                        if (socketID != 0)
                            MobosSockets.Add(new MobosSockets
                            {
                                MoboID = id,
                                SocketID = socketID
                            });
                    }
                }
            }
        }

        public override string ToString()
        {
            return id + "\t" + manufcator + " " + model;
        }

        public override void Update()
        {
            if (id == 0)
            {
                Console.WriteLine("This component is not exist!");
            }
            else
            {
                Console.WriteLine("manufcator: " + Program.RTL(manufcator) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter manufcator: ");
                    manufcator = Console.ReadLine();
                }

                Console.WriteLine("model: " + Program.RTL(model) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter model: ");
                    model = Console.ReadLine();
                }

                Console.WriteLine("slots: " + slots + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter number Of slots: ");
                    slots = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("Channels: " + Channels + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter number of Channels: ");
                    Channels = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("memoryType: " + memoryType + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter memory type (eg:");
                    foreach (string s in Program.db.Memories.Select(m => m.type).Distinct())
                    {
                        Console.Write(", " + Program.RTL(s));
                    }
                    Console.Write("): ");
                    memoryType = Console.ReadLine();
                }

                Console.WriteLine("memory Max Size: " + memoryMaxSize + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter memory Max size: ");
                    memoryMaxSize = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("computer Type: " + (computerType.Value ? "PC" : "Laptop") + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Computer type:\n0. For Laptop\n1. For PC\n>");
                    computerType = Console.ReadLine()[0] == '1';
                }

                /*Console.WriteLine("disk: " + Program.db.Disks.Find(diskID).ToString() + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.WriteLine("Enter disk ID:");
                    foreach (Disks disk in Program.db.Disks)
                        Console.WriteLine(disk.id + ". For " + disk.ToString());
                    diskID = Convert.ToInt32(Console.ReadLine());
                }*/

                Processors myProc = Program.db.Processors.Find(proessorID);
                Console.WriteLine("proessor: " + (myProc == null ? "No One" : myProc.ToString()) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.WriteLine("Enter processor ID:");
                    foreach (Processors processor in Program.db.Processors)
                        Console.WriteLine(processor.id + ". For " + processor.ToString());
                    proessorID = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("computer: " + Program.db.Computers.Find(computerID).ToString() + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.WriteLine("Enter computer ID:");
                    foreach (Computers computer in Program.db.Computers)
                        Console.WriteLine(computer.id + ". For " + computer.ToString());
                    computerID = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("SataConnectors: " + SataConnectors + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter number of Sata connectors: ");
                    SataConnectors = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("architacture: " +
                    (architacture.HasValue ?
                        (architacture.Value ? "64 bit" : "32 bit") :
                        "No Data") +
                    "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Architacture:\n0. For 32 bit\n1. For 64 bit\n>");
                    architacture = Console.ReadLine()[0] == '1';
                }

                Console.Write("sockets: ");
                foreach (string name in MobosSockets.Select(s => s.Sockets.Name))
                    Console.Write(name + ",");
                Console.WriteLine("\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    int socketID = 1;
                    while (socketID != 0)
                    {
                        Console.WriteLine("Enter MoBo socket:");
                        foreach (Sockets socket in Program.db.Sockets)
                            Console.WriteLine(socket.Id + ". For " + socket.ToString());
                        Console.Write("For end enter 0: ");

                        socketID = Convert.ToInt32(Console.ReadLine());
                        if (socketID != 0)
                            MobosSockets.Add(new MobosSockets
                            {
                                MoboID = id,
                                SocketID = socketID
                            });
                    }
                }
            }
        }

        public override bool Search(string word)
        {
            return
                manufcator.Contains(word) ||
                model == null ? false : model.Contains(word) ||
                memoryType.Contains(word) ||
                MobosSockets.Where(m => m.Sockets.Name.Contains(word)).Count() > 0;
        }

        public override bool KillMeAndMyChildrens()
        {
            Console.WriteLine("DELETE:\t" + ToString());
            Console.Write(Program.RTL("האם אתה בטוח?? לא ניתן לשנות את זה לאחר מכן! " + "y/n: "));
            if (Console.ReadLine().ToLower()[0] != 'y')
            {
                bool result = true;

                foreach (Disks disk in Disks)
                    result = result && disk.KillMeAndMyChildrens();

                foreach (Memories memo in Memories)
                    result = result && memo.KillMeAndMyChildrens();

                if (Processors != null)
                    result = result && Processors.KillMeAndMyChildrens();

                Program.db.Entry(this).State = EntityState.Deleted;

                return result;
            }
            return false;
        }
    }

    public partial class Computers : IMyEntity
    {
        public override string GetComponentType()
        {
            return "Computer";
        }
        public override void Create()
        {
            id = Program.db.GetNewID();
            Console.WriteLine("CHECK:\t" + id);

            Console.Write("Enter Name: ");
            name = Console.ReadLine();

            Console.Write("Enter comments: ");
            comments = Console.ReadLine();

            Console.Write("Enter status (eg:");
            foreach (string s in Program.db.Computers.Select(c => c.status).Distinct())
            {
                Console.Write(", " + Program.RTL(s));
            }
            Console.Write("): ");
            status = Console.ReadLine();

            Console.Write("Enter model: ");
            model = Console.ReadLine();
        }
        public override string GetFullDetails(string tab)
        {
            string result = base.GetFullDetails(tab);

            foreach (MoBos mobo in Program.db.MoBos.Where(m => m.computerID == id))
                result += "\t" + tab + "MoBo:" + mobo.GetFullDetails(tab + "\t") + "\n";
            return result;
        }
        public override void GetMissData()
        {
            Console.WriteLine(ToString());
        }
        public override bool Search(string word)
        {
            return
                name.Contains(word) ||
                comments.Contains(word) ||
                status.Contains(word) ||
                model.Contains(word);
        }
        public override string ToString()
        {
            return id + "\t" + Program.RTL(name);
        }
        public override void Update()
        {
            if (id == 0)
            {
                Console.WriteLine("This component is not exist!");
            }
            else
            {
                Console.WriteLine("name: " + Program.RTL(name) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Name: ");
                    name = Console.ReadLine();
                }


                Console.WriteLine("comments: " + Program.RTL(comments) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter comments: ");
                    comments = Console.ReadLine();
                }

                Console.WriteLine("status: " + Program.RTL(status) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter status (eg:");
                    foreach (string s in Program.db.Computers.Select(c => c.status).Distinct())
                    {
                        Console.Write(", " + Program.RTL(s));
                    }
                    Console.Write("): ");
                    status = Console.ReadLine();
                }

                Console.WriteLine("model: " + Program.RTL(model) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter model: ");
                    model = Console.ReadLine();
                }
            }
        }
        public override bool CanGiveRecommendations()
        {
            return MoBos.Count > 0;
        }

        public override void PrintRecommendations()
        {
            if (CanGiveRecommendations())
                MoBos.FirstOrDefault().PrintRecommendations();
            else
                base.PrintRecommendations();
        }
        public override bool KillMeAndMyChildrens()
        {
            Console.WriteLine("DELETE:\t" + ToString());
            Console.Write(Program.RTL("האם אתה בטוח?? לא ניתן לשנות את זה לאחר מכן! " + "y/n: "));
            if (Console.ReadLine().ToLower()[0] == 'y')
            {
                bool result = true;               
                foreach (MoBos m in MoBos)
                    result = result && m.KillMeAndMyChildrens();
                Program.db.Entry(this).State = EntityState.Deleted;
                return result;
            }
            return false;
        }
    }

    public partial class Disks : IMyEntity
    {
        public override string GetComponentType()
        {
            return "Disk";
        }
        public override void Create()
        {
            id = Program.db.GetNewID();
            Console.WriteLine("CHECK:\t" + id);

            Console.Write("Enter Name: ");
            name = Console.ReadLine();

            Console.Write("Enter Capacity (GB): ");
            capacity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Type:\n0. For HDD\n1. For SSD\n>");
            type = Console.ReadLine()[0] == '1';

            Console.Write("Enter comments: ");
            comments = Console.ReadLine();

            Console.Write("Enter status (eg:");
            foreach (string s in Program.db.Disks.Select(d => d.status).Distinct())
            {
                Console.Write(", " + Program.RTL(s));
            }
            Console.Write("): ");
            status = Console.ReadLine();

            Console.Write("Enter Computer type:\n0. For Laptop\n1. For PC\n>");
            computerType = Console.ReadLine()[0] == '1';

            Console.WriteLine("Enter OS id:");
            foreach (OSs os in Program.db.OSs)
                Console.WriteLine(os.id + ". For " + os.ToString());
            String result = Console.ReadLine();
            if (result.Length == 0)
                osID = null;
            else
                osID = Convert.ToInt32(result);

            Console.Write("Enter OS version (eg:");
            foreach (string s in Program.db.Disks.Where(d => d.osVersion != null).Select(d => d.osVersion).Distinct())
            {
                Console.Write(", " + Program.RTL(s));
            }
            Console.Write("): ");
            osVersion = Console.ReadLine();

            Console.WriteLine("Enter MoBo ID:");
            foreach (MoBos mobo in Program.db.MoBos)
                Console.WriteLine(mobo.id + ". For " + mobo.ToString());
            result = Console.ReadLine();
            moboID = result == null || result.Length == 0 ? 0 : Convert.ToInt32(result);

            Console.Write("Free to use?:\n0. For False\n1. For True\n>");
            freeToUse = Console.ReadLine()[0] == '1';
        }

        public override string GetFullDetails(string tab)
        {
            MoBos m = Program.db.MoBos.Find(moboID);
            string result = base.GetFullDetails(tab);
            /*if (moboID != null)
            {
                result += m.GetFullDetails(tab + "\t");
            }
            else if (moboID != 0)
            {
                result += m.GetFullDetails(tab + "\t");

            }
            return result;*/
            result += (moboID == null || moboID == 0 ?
                "" :
                Program.db.MoBos.Find(moboID).GetFullDetails(tab + "\t"));
            return result;
        }

        public override void GetMissData()
        {
            Console.WriteLine(ToString());

            if (!capacity.HasValue || capacity.Value == 0)
            {
                Console.WriteLine("Capacity is 0 or null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Capacity (GB): ");
                    capacity = Convert.ToInt32(Console.ReadLine());
                }
            }

            if (!type.HasValue)
            {
                Console.WriteLine("Type is null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Type:\n0. For HDD\n1. For SSD\n>");
                    type = Console.ReadLine()[0] == '1';
                }
            }

            if (!computerType.HasValue)
            {
                Console.WriteLine("Computer type is null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Computer type:\n0. For Laptop\n1. For PC\n>");
                    computerType = Console.ReadLine()[0] == '1';
                }
            }

            if (!freeToUse.HasValue)
            {
                Console.WriteLine("Free to use has empty. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Free to use?:\n0. For False\n1. For True\n>");
                    freeToUse = Console.ReadLine()[0] == '1';
                }
            }
        }

        public override bool Search(string word)
        {
            return
                name.Contains(word) ||
                comments.Contains(word) ||
                status.Contains(word) ||
                (Program.db.OSs.Find(osID) == null ?
                false :
                Program.db.OSs.Find(osID).name.Contains(word)) ||
                (osVersion == null ? false : osVersion.Contains(word));
        }

        public override void SetFreeToUse(bool freeToUse)
        {
            this.freeToUse = freeToUse;
        }

        public override string ToString()
        {
            return
                id + "\t" +
                Program.RTL(name) + " " +
                capacity.Value + "GB\t" +
                (type.Value ? "SSD" : "HDD") + "\t" +
                (computerType.Value ? "PC" : "Laptop") + "\t" +
                (osID.HasValue ? Program.db.OSs.Find(osID).ToString() : "No OS");
        }

        public override void Update()
        {
            if (id == 0)
            {
                Console.WriteLine("This component is not exist!");
            }
            else
            {
                Console.WriteLine("Name: " + Program.RTL(name) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Name: ");
                    name = Console.ReadLine();
                }

                Console.WriteLine("capacity: " + capacity + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Capacity (GB): ");
                    capacity = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("type: " + (type.Value ? "SSD" : "HDD") + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Type:\n0. For HDD\n1. For SSD\n>");
                    type = Console.ReadLine()[0] == '1';
                }

                Console.WriteLine("comments: " + Program.RTL(comments) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter comments: ");
                    comments = Console.ReadLine();
                }

                Console.WriteLine("status: " + Program.RTL(status) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter status (eg:");
                    foreach (string s in Program.db.Disks.Select(d => d.status).Distinct())
                    {
                        Console.Write(", " + Program.RTL(s));
                    }
                    Console.Write("): ");
                    status = Console.ReadLine();
                }

                Console.WriteLine("computer Type: " + (computerType.Value ? "PC" : "Laptop") + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Computer type:\n0. For Laptop\n1. For PC\n>");
                    computerType = Console.ReadLine()[0] == '1';
                }

                OSs myOS = Program.db.OSs.Find(osID);
                Console.WriteLine("os: " + (myOS == null ? "NOT EXIST" : myOS.ToString()) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.WriteLine("Enter OS id:");
                    foreach (OSs os in Program.db.OSs)
                        Console.WriteLine(os.id + ". For " + os.ToString());
                    String result = Console.ReadLine();
                    if (result == null)
                        osID = null;
                    else
                        osID = Convert.ToInt32(result);
                }

                Console.WriteLine("OS version: " + osVersion + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter OS version (eg:");
                    foreach (string s in Program.db.Disks.Where(d => d.osVersion != null).Select(d => d.osVersion).Distinct())
                    {
                        Console.Write(", " + Program.RTL(s));
                    }
                    Console.Write("): ");
                    osVersion = Console.ReadLine();
                }

                Console.WriteLine("MoBo: " + Program.db.MoBos.Find(moboID).ToString() + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.WriteLine("Enter MoBo ID:");
                    foreach (MoBos mobo in Program.db.MoBos)
                        Console.WriteLine(mobo.id + ". For " + mobo.ToString());
                    string result = Console.ReadLine();
                    moboID = result == null || result.Length == 0 ? 0 : Convert.ToInt32(result);
                }

                Console.WriteLine("Free to use: " + freeToUse.Value + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Free to use?:\n0. For False\n1. For True\n>");
                    freeToUse = Console.ReadLine()[0] == '1';
                }

            }
        }
    }

    public partial class Processors : IMyEntity
    {
        public override string GetComponentType()
        {
            return "Processor";
        }
        public override void Create()
        {
            id = Program.db.GetNewID();
            Console.WriteLine("CHECK:\t" + id);
            Console.Write("Enter Full Name: ");
            name = Console.ReadLine();

            Console.Write("Enter Generation (int): ");
            generation = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Family: ");
            family = Console.ReadLine();

            Console.Write("Enter Max speed (in MHz): ");
            speed = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Rank (From CPUBencmark): ");
            rank = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Architacture:\n0. For 32 bit\n1. For 64 bit\n>");
            architacture = Console.ReadLine()[0] == '1';

            int socketID = 1;
            while (socketID != 0)
            {
                Console.WriteLine("Enter processor socket:");
                foreach (Sockets socket in Program.db.Sockets)
                    Console.WriteLine(socket.Id + ". For " + socket.ToString());
                Console.Write("For end enter 0: ");

                socketID = Convert.ToInt32(Console.ReadLine());
                if (socketID != 0)
                    ProcessorSocket.Add(new ProcessorSocket()
                    {
                        ProcessorID = id,
                        SocketID = socketID
                    });
            }

            Console.Write("Free to use?:\n0. For False\n1. For True\n>");
            freeToUse = Console.ReadLine()[0] == '1';
        }

        public override string GetFullDetails(string tab)
        {
            string result = base.GetFullDetails(tab);
            foreach (MoBos mobo in Program.db.MoBos.Where(m => m.proessorID == id))
                result += mobo.GetFullDetails(tab + "\t");
            return result;
        }

        public override void GetMissData()
        {
            Console.WriteLine(ToString());

            if (!generation.HasValue)
            {
                Console.WriteLine("Generation is null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Generation (int): ");
                    generation = Convert.ToInt32(Console.ReadLine());
                }
            }

            if (!speed.HasValue || speed.Value == 0)
            {
                Console.WriteLine("Generation is null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Max speed (in MHz): ");
                    speed = Convert.ToInt32(Console.ReadLine());
                }
            }

            if (!rank.HasValue || rank.Value == 0)
            {
                Console.WriteLine("Rank is null or 0. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Rank (From CPUBencmark): ");
                    rank = Convert.ToInt32(Console.ReadLine());
                }
            }

            if (!architacture.HasValue)
            {
                Console.WriteLine("Architacture is null. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Architacture:\n0. For 32 bit\n1. For 64 bit\n>");
                    architacture = Console.ReadLine()[0] == '1';
                }
            }

            if (ProcessorSocket.Count == 0)
            {
                Console.WriteLine("No Processor Socets. Add? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    int socketID = 1;
                    while (socketID != 0)
                    {
                        Console.WriteLine("Enter processor socket:");
                        foreach (Sockets socket in Program.db.Sockets)
                            Console.WriteLine(socket.Id + ". For " + socket.ToString());
                        Console.Write("For end enter 0: ");

                        socketID = Convert.ToInt32(Console.ReadLine());
                        if (socketID != 0)
                            ProcessorSocket.Add(new ProcessorSocket()
                            {
                                ProcessorID = id,
                                SocketID = socketID
                            });
                    }
                }
            }

            if (!freeToUse.HasValue)
            {
                Console.WriteLine("Free to use has empty. Change? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Free to use?:\n0. For False\n1. For True\n>");
                    freeToUse = Console.ReadLine()[0] == '1';
                }
            }
        }

        public override bool Search(string word)
        {
            return
                name.Contains(word) ||
                family.Contains(word) ||
                ProcessorSocket.Where(p => p.Sockets.Name.Contains(word)).Count() > 0;
        }

        public override void SetFreeToUse(bool freeToUse)
        {
            this.freeToUse = freeToUse;
        }

        public override string ToString()
        {
            return id + "\t" + Program.RTL(name) + "\tRank:" + rank + (ProcessorSocket.Count() > 0 ? "\tSocket: " + ProcessorSocket.Select(s => s.Sockets.Name).FirstOrDefault() : "");
        }
    }

    public partial class Components : IMyEntity
    {
        public override void Create()
        {
            Id = Program.db.GetNewID();
            Console.WriteLine("CHECK:\t" + Id);

            Console.Write("Enter Name: ");
            Name = Console.ReadLine();

            Console.Write("Enter status (eg:");
            foreach (string s in Program.db.Components.Select(c => c.Status).Distinct())
            {
                Console.Write(", " + Program.RTL(s));
            }
            Console.Write("): ");
            Status = Console.ReadLine();
        }

        public override string GetComponentType()
        {
            return "Component";
        }

        public override void GetMissData()
        {
            Console.WriteLine("No miss data in components.");
        }

        public override bool Search(string word)
        {
            return
                Name.Contains(word) ||
                Status.Contains(word);
        }

        public override void Update()
        {
            if (Id == 0)
            {
                Console.WriteLine("This component is not exist!");
            }
            else
            {
                Console.WriteLine("Name: " + Program.RTL(Name) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter Name: ");
                    Name = Console.ReadLine();
                }

                Console.WriteLine("Status: " + Program.RTL(Status) + "\nDo you want to change it? y/n: ");
                if (Console.ReadLine().ToLower()[0] == 'y')
                {
                    Console.Write("Enter status (eg:");
                    foreach (string s in Program.db.Components.Select(c => c.Status).Distinct())
                    {
                        Console.Write(", " + Program.RTL(s));
                    }
                    Console.Write("): ");
                    Status = Console.ReadLine();
                }

            }
        }
    }

    public partial class Sockets
    {
        public override string ToString()
        {
            return Program.RTL(Name);
        }
    }

    public partial class ComponentsDBEntities
    {
        public Dictionary<int, Type> MyTables
        {
            get
            {
                return new Dictionary<int, Type>
                {
                    { 1, typeof(Computers) },
                    {2, typeof(Disks) },
                    {3,  typeof(Memories) },
                    {4, typeof(MoBos)},
                    {5, typeof(Processors) },
                    {6, typeof(Components) },
                };
            }
        }
        public int GetNewID()
        {
            return (
                  new List<int> {
                        Computers.Select(c=>c.id).Max(),
                        Memories.Select(c=>c.id).Max(),
                        MoBos.Select(c=>c.id).Max(),
                        Processors.Select(c=>c.id).Max(),
                        Disks.Select(d=>d.id).Max(),
                        Components.Select(c=>c.Id).Max()
                  }).Max<int>() + 1;
        }

        public IMyEntity GetEntityByID(int id)
        {
            List<IMyEntity> entityies = new List<IMyEntity>
            {
                Computers.Find(id),
                Memories.Find(id),
                MoBos.Find(id),
                Processors.Find(id),
                Disks.Find(id),
            };
            List<IMyEntity> answer = entityies.Where(e => e != null).ToList();
            switch (answer.Count)
            {
                case 1:
                    return answer.First();
                case 0:
                    return null;
                default:
                    throw new IndexOutOfRangeException("Too many Emtityies with id:" + id);
            }
        }

        public ICollection<IMyEntity> Search(string word)
        {
            List<IMyEntity> entityies = new List<IMyEntity>();
            entityies.AddRange(Computers);
            entityies.AddRange(Memories);
            entityies.AddRange(MoBos);
            entityies.AddRange(Processors);
            entityies.AddRange(Disks);

            List<IMyEntity> result = new List<IMyEntity>();

            foreach (IMyEntity entity in entityies)
            {
                if (entity.Search(word))
                    result.Add(entity);
            }

            return result;
        }

        public List<string> GetColumnsForTable(Type table)
        {
            List<string> columns = new List<string>();
            Regex regex = new Regex(@"<(\w+)>");

            foreach (FieldInfo info in table.GetRuntimeFields())
            {
                Match match = regex.Match(info.Name);
                if (match.Success)
                    columns.Add(match.Groups[1].Value);
            }

            return columns;
        }

        public String GetTableName(Type table)
        {
            return table.Name;
        }
    }

    public partial class OSs
    {
        public override string ToString()
        {
            return Program.RTL(name) + " " + (architacture == true ? "64 bit" : "32 bit");
        }
    }

    public abstract class IMyEntity
    {
        public abstract void Create();
        public abstract bool Search(string word);
        public abstract string GetComponentType();

        public virtual string GetFullDetails(string tab)
        {
            return tab + ToString() + ":\n";
        }

        public virtual void GetMissData()
        {
            Console.WriteLine("GetMissData not implement in this " + GetComponentType() + " class");
        }

        public virtual void Update()
        {
            Console.WriteLine("Update not implement in this " + GetComponentType() + " class");
        }
        public virtual void SetFreeToUse(bool freeToUse)
        {
            Console.WriteLine(GetComponentType() + " -> Free to use not Exist");
        }
        public virtual bool CanGiveRecommendations()
        {
            if (this is MoBos)
                return true;
            return false;
        }

        public virtual void PrintRecommendations()
        {
            Console.WriteLine("Print Recommendations not implement in this " + GetComponentType() + " class");
        }

        public virtual bool KillMeAndMyChildrens()
        {
            Console.WriteLine("DELETE:\t" + ToString());
            Console.Write(Program.RTL("האם אתה בטוח?? לא ניתן לשנות את זה לאחר מכן! " + "y/n: "));
            if (Console.ReadLine().ToLower()[0] != 'y')
            {
                Program.db.Entry(this).State = EntityState.Deleted;
                return true;
            }
            return false;
        }
    }
}
