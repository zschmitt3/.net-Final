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
    logger.Info(answer+" chosen.");
    
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
        logger.Info("Name: "+name+"; Disc.: "+discontinued);
        Product newProduct = new Product();
        newProduct.ProductName = name;
        newProduct.Discontinued = discontinued;
        database.Products.Add(newProduct);
        database.SaveChanges();
    }else if(answer == '2'){
        System.Console.WriteLine("Enter name of the product to be edited.");
        string productName = Console.ReadLine();
        while(productName == ""){
            System.Console.WriteLine("Please enter a valid name.");
            productName = Console.ReadLine();
        }
        logger.Info("Name: "+productName);

        Product product = new Product();
        foreach(var productIteration in database.Products){
                if(productIteration.ProductName == productName){
                    product = productIteration;
                }
        }
        if(product == new Product()){
            System.Console.WriteLine("Invalid product name.");
            logger.Error("Product not found.");
        }else{
            System.Console.WriteLine("Enter N to edit the Product Name");
            System.Console.WriteLine("Enter D to toggle Discontinued");
            char editColumn = Console.ReadLine().ToLower()[0];
            logger.Info(editColumn+" selected.");
            if(editColumn == 'n'){
                System.Console.WriteLine("Enter a new name:");
                product.ProductName = Console.ReadLine();
                database.SaveChanges();
            }else if(editColumn == 'd'){
                if(product.Discontinued){product.Discontinued=false;}else{product.Discontinued=true;}
                database.SaveChanges();
            }
        }
    }else if(answer == '3'){
        System.Console.WriteLine("Enter D to display discontinued products");
        System.Console.WriteLine("Enter A to display active products");
        System.Console.WriteLine("Enter anything else to display both");
        char display = Console.ReadLine().ToLower()[0];
        logger.Info(display+" selected.");
        
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
        System.Console.WriteLine("Enter name of the product to be displayed.");
        string productName = Console.ReadLine();
        while(productName == ""){
            System.Console.WriteLine("Please enter a valid name.");
            productName = Console.ReadLine();
        }
        logger.Info("Name: "+productName);
        Product product = new Product();
        foreach(var productIteration in database.Products){
                if(productIteration.ProductName == productName){
                    product = productIteration;
                }
        }

        if(product == new Product()){
            System.Console.WriteLine("Invalid product name.");
        }else{
            System.Console.WriteLine(product.ProductId);
            System.Console.WriteLine(product.ProductName);
            System.Console.WriteLine(product.SupplierId);
            System.Console.WriteLine(product.CategoryId);
            System.Console.WriteLine(product.QuantityPerUnit);
            System.Console.WriteLine(product.UnitPrice);
            System.Console.WriteLine(product.UnitsInStock);
            System.Console.WriteLine(product.UnitsOnOrder);
            System.Console.WriteLine(product.ReorderLevel);
            System.Console.WriteLine(product.Discontinued);
        }
    }



}
catch (Exception ex)
{
    logger.Error(ex.Message);
}

logger.Info("Program ended");
