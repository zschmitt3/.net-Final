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
        System.Console.WriteLine("Enter a product name.");
        string name = Console.ReadLine();
        while(name == ""){
            System.Console.WriteLine("Please enter a valid name.");
            name = Console.ReadLine();
        }
        System.Console.WriteLine("If the product is discontinued, enter TRUE");
        bool discontinued;
        if(Console.ReadLine().ToLower()[0] == 't'){discontinued = true;}else{discontinued = false;}
        Product newProduct = new Product();
        newProduct.ProductName = name;
        newProduct.Discontinued = discontinued;
        database.Products.Add(newProduct);
        database.SaveChanges();
    }else if(answer == '2'){

    }else if(answer == '3'){
        System.Console.WriteLine("Enter D to display discontinued products");
        System.Console.WriteLine("Enter A to display active products");
        System.Console.WriteLine("Enter anything else to display both");
        char display = Console.ReadLine().ToLower()[0];
        if(display == 'd'){
           foreach(var product in database.Products.OrderBy(x => x.ProductName)){
                if(product.Discontinued){
                    System.Console.WriteLine(product.ProductName);
                }
           }
        }else if(display == 'a'){
           foreach(var product in database.Products.OrderBy(x => x.ProductName)){
                if(!product.Discontinued){
                    System.Console.WriteLine(product.ProductName);
                }
           }
        }else{
           foreach(var product in database.Products.OrderBy(x => x.ProductName)){
                System.Console.WriteLine(product.ProductName);
           }
        }
    }else if(answer == '4'){

    }else if(answer == '4'){
        
    }



}
catch (Exception ex)
{
    logger.Error(ex.Message);
}

logger.Info("Program ended");
