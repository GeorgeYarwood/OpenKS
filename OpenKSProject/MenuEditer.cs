using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenKSProject
{
    internal class MenuEditor : Subsystem
    {
        //SQL strings
        const string MENU_ITEM = "MENUITEM";
        const string SQL_CONNECTION = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\OpenKs\OpenKSProject\Menu.mdf;Integrated Security=True";
        const string SQL_QUERY = "SELECT MENUITEM FROM [Table]";

        //Prompts
        const string ERROR_MESSAGE = "Invalid option! Please try again.";
        const string INITIAL_PROMPT = "What would you like to do with the menu?";
        const string INITIAL_PROMPT_OPTIONS = "1. View all items    2. Add a new item   3. Delete an item";
        const string ADD_DELETE_ITEM_PROMPT = "Item name: ";

        const string NEW_LINE = "\n";

        const ConsoleKey ESCAPE_KEY = ConsoleKey.Escape;
        const ConsoleKey ENTER_KEY = ConsoleKey.Enter;

        SqlConnection? sqlClient;

        static MenuEditor instance;
        public static MenuEditor Instance
        {
            get { return instance; }
        }

        public override void Init()
        {
            UpdateRuntimeMenu();
        }

        public override void SlowUpdate()
        {

        }

        public override void FastUpdate()
        {

        }

        public void EditMenu()
        {
            Console.WriteLine(INITIAL_PROMPT);
            Console.WriteLine(NEW_LINE);
            Console.WriteLine(INITIAL_PROMPT_OPTIONS);

        GetMenuOptionInput:
            try
            {
                int UserInput = int.Parse(Console.ReadKey().KeyChar.ToString());
                if (UserInput == 1)
                {
                    goto DisplayAllItems;
                }
                else if (UserInput == 2)
                {
                    goto GetDeleteItemInput;
                }
                else if (UserInput == 3)
                {
                    goto GetNewItemInput;
                }
            }
            catch
            {
                Console.WriteLine(ERROR_MESSAGE);
                goto GetMenuOptionInput;
            }

         GetNewItemInput:


         GetDeleteItemInput:


         DisplayAllItems:

        }

        void SetupSqlConnection(bool Connect)
        {
            if (sqlClient == null)
            {
                sqlClient = new SqlConnection(SQL_CONNECTION);
            }

            if (Connect)
            {
                if (sqlClient.State == System.Data.ConnectionState.Closed)
                {
                    sqlClient.Open();
                }

                //SqlCommand TestCommand = new SqlCommand("INSERT INTO [Table] (MenuItem) VALUES ('Kids nuggets')", sqlClient);
                //TestCommand.Parameters.Add("@menuItemName", SqlDbType.Text).Value = "Bowl Chips";
                //TestCommand.ExecuteNonQuery();
            }
            else
            {
                if (sqlClient.State == System.Data.ConnectionState.Open)
                {
                    sqlClient.Close();
                }
            }
        }

        void UpdateRuntimeMenu()
        {
            SetupSqlConnection(true);
            SqlCommand SearchCommand = new SqlCommand(SQL_QUERY, sqlClient);
            SqlDataReader Reader = SearchCommand.ExecuteReader();
            MenuSearch.runtimeMenu.Clear();
            while (Reader.Read())
            {
                MenuItem ImportedItem = new MenuItem();
                ImportedItem.ItemName = Reader[MENU_ITEM].ToString();

                MenuSearch.runtimeMenu.Add(ImportedItem);
            }
            Reader.Close();
        }
    }
}
