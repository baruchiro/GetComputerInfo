using AddToComputersDB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddToComputersDB
{
    class Program
    {
        public static ComponentsDBEntities db = new ComponentsDBEntities();
        public static OleDbConnection CONNECTION = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" +
                @"Data Source='F:\OneDrive\מסמכים\מחשבים\רישום רכיבים.accdb'");

        static void Main(string[] args)
        {

            foreach (Computers mobo in db.Computers.Where(m => m.MoBos.Count>0))
                Console.WriteLine(mobo.GetFullDetails(""));
            char choose;
            do
            {
                Console.Write("Choose Option:\n" +
                    "a. For new record\n" +
                    "b. For recommends\n" +
                    "c. For work with specific component\n" +
                    "d. For search word\n" +
                    "e. For custom query\n" +
                    "0. For Exit\n>");
                string answer = Console.ReadLine(); //for exception outofrange in empty imput
                choose = answer.Length == 0 ? 'x' : answer[0];

                switch (choose)
                {
                    case 'a':
                        InsertNewRow();
                        break;
                    case 'b':
                        GetRecommendations();
                        break;
                    case 'c':
                        WorkWithSpecificComponent();
                        break;
                    case 'd':
                        SerchWord();
                        break;
                    case 'e':
                        CustomQuery();
                        break;
                }
            } while (choose != '0');
            Console.WriteLine("EXIT");
        }

        private static void CustomQuery()
        {
            Dictionary<int, Type> tables = db.MyTables;
            int selected;
            do
            {
                Type table;
                Console.WriteLine("Select Table to Query:");
                foreach (KeyValuePair<int, Type> row in tables)
                {
                    Console.WriteLine(row.Key + ". For " + row.Value.Name);
                }
                Console.WriteLine("0. for Exit");
                Console.Write("Enter here: ");
                selected = Convert.ToInt32(Console.ReadLine());
                if (selected != 0)
                {
                    table = tables[selected];


                    Console.WriteLine("Enter WHERE condition:\nFields:");
                    foreach (string column in db.GetColumnsForTable(table))
                        Console.WriteLine(column);
                    string where = " WHERE " + Console.ReadLine();

                    Console.WriteLine("Enter ORDER BY condition:\nFields:");
                    foreach (string column in db.GetColumnsForTable(table))
                        Console.WriteLine(column);
                    string orderby = Console.ReadLine();
                    if(!orderby.Equals(""))
                    orderby = " ORDER BY " + orderby;

                    string start = "SELECT * FROM dbo.";

                    foreach (IMyEntity entity in db.Set(table).SqlQuery(start + table.Name + where + orderby))
                    {
                        Console.WriteLine(entity.ToString());
                    }
                }
            } while (selected != 0);
        }

        private static void SerchWord()
        {
            string searchWord;
            do
            {
                Console.Write("Enter word to search\nTo Exit press Enter: ");
                searchWord = Console.ReadLine();
                foreach (IMyEntity entity in db.Search(searchWord))
                {
                    Console.WriteLine(entity.ToString());
                }
            } while (searchWord != null && searchWord.Length > 0);
        }

        private static void WorkWithSpecificComponent()
        {
            int id = 999;
            do
            {
                try
                {
                    Console.Write("Enter component id: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    IMyEntity entity = db.GetEntityByID(id);

                    if (entity != null)
                    {
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~\n" +
                            entity.ToString() +
                            "\n~~~~~~~~~~~~~~~~~~~~~~\n");
                        int select = 999;
                        do
                        {
                            try
                            {
                                Console.Write("Choose option:\n" +
                                    "1. For print full details\n" +
                                    "2. For enter miss data\n" +
                                    "3. For reinput data\n" +
                                    "4. For change \"Free to use\" status\n" +
                                    (entity.CanGiveRecommendations() ? "5. For get recommendations\n" : "") +
                                    "6. For KILL me and my chikdrens\n" +
                                    "0. For Exit\n>");

                                select = Convert.ToInt32(Console.ReadLine());

                                switch (select)
                                {
                                    case 1:
                                        Console.WriteLine(entity.GetFullDetails(""));
                                        break;
                                    case 2:
                                        entity.GetMissData();
                                        Console.WriteLine(db.SaveChanges() + " rows changes");
                                        break;
                                    case 3:
                                        entity.Update();
                                        Console.WriteLine(db.SaveChanges() + " rows changes");
                                        break;
                                    case 4:
                                        Console.Write("Free to use?:\n0. For False\n1. For True\n>");
                                        entity.SetFreeToUse(Console.ReadLine()[0] == '1');
                                        Console.WriteLine(db.SaveChanges() + " rows changes");
                                        break;
                                    case 5:
                                        entity.PrintRecommendations();
                                        break;
                                    case 6:
                                        if (entity.KillMeAndMyChildrens())
                                        {
                                            Console.WriteLine(db.SaveChanges() + " rows changes");
                                            Console.WriteLine("null OR exist: " + db.GetEntityByID(id));
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cannot success to delete this entity and his childrends.\n" +
                                                "So the program close now to remove any delete marks.");
                                            Console.ReadLine();
                                            Environment.Exit(1);
                                        }
                                        break;
                                }
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("ERROR:\t" + RTL(e.Message));
                            }
                        } while (select != 0);
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("ERROR:\t" + e.Message);
                }
                catch (Exception e)
                {
                    throw e;
                }
            } while (id != 0);
        }

        private static void GetRecommendations()
        {
            int select;
            do
            {
                Console.WriteLine("Select MoBo:");
                foreach (MoBos mobo in db.MoBos)
                    Console.WriteLine(mobo.id + ". For " + mobo.ToString());
                Console.Write("0. For all Recommendations and Exit\n>");
                select = Convert.ToInt32(Console.ReadLine());

                if (select == 0)
                {
                    foreach (MoBos mobo in db.MoBos)
                    {
                        mobo.PrintRecommendations();
                        Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                    }
                }
                else
                {
                    db.MoBos.Find(select).PrintRecommendations();
                    Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                }
            } while (select != 0);
        }

        private static void InsertNewRow()
        {
            int tableID = 1;
            do
            {
                try
                {
                    Console.WriteLine(RTL("הסדר הנכון להוספה הוא: מחשב, מעבד, לוח אם, דיסק, זכרון"));

                    foreach (KeyValuePair<int, Type> table in db.MyTables)
                    {
                        Console.WriteLine(table.Key + ". For " + table.Value.Name);

                    }
                    Console.WriteLine("0. for Exit");
                    Console.Write("Enter here: ");
                    tableID = Convert.ToInt32(Console.ReadLine());

                    switch (tableID)
                    {
                        case 1: //Computers
                            Computers computer = new Computers();
                            computer.Create();
                            db.Computers.Add(computer);
                            //db.Entry(computer).State = EntityState.Added;
                            Console.WriteLine(db.SaveChanges() + " rows changes");
                            Console.WriteLine("ID: " + db.Computers.Find(computer.id).id);
                            break;
                        case 2: //Disks
                            Disks disk = new Disks();
                            disk.Create();
                            db.Entry(disk).State = EntityState.Added;
                            Console.WriteLine(db.SaveChanges() + " rows changes");
                            Console.WriteLine("ID: " + db.Disks.Find(disk.id).id);
                            break;
                        case 3: //Memories
                            Memories memory = new Memories();
                            memory.Create();
                            db.Memories.Add(memory);
                            //db.Entry(computer).State = EntityState.Added;
                            Console.WriteLine(db.SaveChanges() + " rows changes");
                            Console.WriteLine("ID: " + db.Memories.Find(memory.id).id);
                            break;
                        case 4: //mMoBos
                            MoBos mobo = new MoBos();
                            mobo.Create();
                            db.MoBos.Add(mobo);
                            //db.Entry(computer).State = EntityState.Added;
                            Console.WriteLine(db.SaveChanges() + " rows changes");
                            Console.WriteLine("ID: " + db.MoBos.Find(mobo.id).id);
                            break;
                        case 5: //Proccesrs
                            Processors processor = new Processors();
                            processor.Create();
                            db.Processors.Add(processor);
                            //db.Entry(computer).State = EntityState.Added;
                            Console.WriteLine(db.SaveChanges() + " rows changes");
                            Console.WriteLine("ID: " + db.Processors.Find(processor.id).id);
                            break;
                        case 6: //Components
                            Components component = new Components();
                            component.Create();
                            db.Components.Add(component);
                            //db.Entry(computer).State = EntityState.Added;
                            Console.WriteLine(db.SaveChanges() + " rows changes");
                            Console.WriteLine("ID: " + db.Components.Find(component.Id).Id);
                            break;
                        default:
                            tableID = 0;
                            break;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("ERROR:\t" + RTL(e.Message));
                }
                catch (Exception e)
                {
                    throw e;
                }

            } while (tableID != 0);
        }

        public static string RTL(string text)
        {
            if (text == null)
                return "";
            if (text.Length == 0)
                return text;
            char[] arr = text.ToCharArray();
            if (arr[0] >= 'z')
            {
                for (int i = 0; i < arr.Length / 2; i++)
                {
                    char temp = arr[i];
                    arr[i] = arr[arr.Length - 1 - i];
                    arr[arr.Length - 1 - i] = temp;
                }
            }

            return new string(arr);
        }

        /*public static void Run(string command)
{
    OleDbDataReader reader;
    try
    {
        OleDbCommand cmd = new OleDbCommand(command, CONNECTION);

        cmd.Parameters.Add("@id", OleDbType.Integer).Value = 25;


        Console.WriteLine(cmd.ExecuteNonQuery() + " Rows Changes");



        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader[i] + "\t");
            }
            Console.WriteLine();
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("ERROR: " + e.Message);
        Console.WriteLine(e.StackTrace);

    }
    CONNECTION.Close();
}
*/
        /*public static void InsertInto()
        {
            OleDbConnection conn = new OleDbConnection(
                "Provider=Microsoft.ACE.OLEDB.12.0;" +
                @"Data Source='F:\OneDrive\מסמכים\מחשבים\רישום רכיבים.accdb'"
);
            string query = "INSERT INTO @table";

            OleDbDataReader reader;

            try
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand("INSERT INTO [לוחות אם]" +
                    " (מזהה) VALUES (@id)", conn);

                cmd.Parameters.Add("@id", OleDbType.Integer).Value = 25;


                // Console.WriteLine(cmd.ExecuteNonQuery() + " Rows Changes");

                OleDbCommand max = new OleDbCommand("SELECT MAX(MaxFromTables.Expr1000)+1 FROM MaxFromTables", conn);

                reader = max.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine(reader[0].ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                Console.WriteLine(e.StackTrace);

            }
            conn.Close();
        }
        */
        /*private static string GetStringFromList(List<string> list, bool isNames = false)
{
    string result = "";
    foreach (string s in list)
    {
        result += "'" + s + "',";
    }
    if (isNames)
        result = result.Replace("'", "");
    return result.Remove(result.Length - 1);
}
*/
        /*public static Dictionary<int, string> GetTables()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            DataTable tables = CONNECTION.GetSchema("Tables", null);

            int j = 0;
            for (int i = 0; i < tables.Rows.Count; i++)
            {
                object[] items = tables.Rows[i].ItemArray;
                if (items[3].ToString().Equals("TABLE") && !items[2].ToString().Equals("מערכות הפעלה"))
                {
                    result.Add(++j, tables.Rows[i].ItemArray[2].ToString());
                }
                //result.Add(i - startTable + 1, tables.Rows[i].ItemArray[2].ToString());
            }

            return result;
        }
        */
        /*public static Dictionary<string, string> GetColumns(string tableName)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            DataTable columns = CONNECTION.GetSchema("Columns", new string[4] { null, null, tableName, null });

            for (int i = 0; i < columns.Rows.Count; i++)
            {
                result.Add(
                    columns.Rows[i].ItemArray[3].ToString(),
                    columns.Rows[i].ItemArray[columns.Rows[i].ItemArray.Length - 1].ToString());
            }
            return result;
        }
        */
    }
}
