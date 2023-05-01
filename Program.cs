using NLog;
using System.Linq;
using NWConsole.Model;

string path = Directory.GetCurrentDirectory() + "\\nlog.config";

var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");
try
{
    var database = new NWContext();
    System.Console.WriteLine("Enter 1 to ADD a record to the Products table");
    System.Console.WriteLine("Enter 2 to EDIT a record in the Products table");
    System.Console.WriteLine("Enter 3 to DISPLAY ALL records in the Products table");
    System.Console.WriteLine("Enter 4 to DISPLAY a record to the Products table");
    char answer = Console.ReadLine()[0];

    
    if(answer == '1'){
        int id = 1+database.Products.OrderByDescending(x => x.ProductId).First().ProductId;
        System.Console.WriteLine("Enter a product name.");
        string name = Console.ReadLine();
        while(name == ""){
            System.Console.WriteLine("Please enter a valid name.");
            name = Console.ReadLine();
        }
        System.Console.WriteLine("If the product is discontinued, enter TRUE");
        if(Console.ReadLine().ToLower()[0] == 't'){bool discontinued = true;}else{bool discontinued = false;}
        
    }



}
catch (Exception ex)
{
    logger.Error(ex.Message);
}

logger.Info("Program ended");
