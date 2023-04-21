using Base_Atestat.DB;
using Base_Atestat.Debug;

namespace Base_Atestat
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //Console.WriteLine("Program Initialized!");
            PrintConsole.Intialized();
            DBWrapper.StartDB();
        }
    }
}