using PrsLibrary;
using PrsLibrary.Controllers;
using PrsLibrary.Models;

Connection connection = new();
connection.Connect();

UsersController userCtrl = new(connection);


User? u = userCtrl.Login("sa", "sa");
if(u is not null)
{
Console.WriteLine($"{u.ID} | {u.Username} | {u.FirstName} | {u.LastName}");
} else
{
    Console.WriteLine("Username or Password is incorrect!");
}



//IEnumerable<Product> products = prodCtrl.GetAllProducts();
//foreach(Product p in products)
//{
//    Console.WriteLine($"{p.PartNbr, -15} | {p.Name, -20} | {p.Price, 10:C} | {p.Vendor.Name, -30}");
//}

//VendorsController vendorCtrl = new(connection);
//IEnumerable<Vendor> vendors = vendorCtrl.GetAllVendors();
//foreach(Vendor v in vendors)
//{
//    Console.WriteLine($"{v.Code} {v.Name}");
//}

//Vendor newVendor = new()
//{
//    Code = "X2X",
//    Name = "Xtreme",
//    Address = "111 Main",
//    City = "Mason",
//    State = "OH",
//    Zip = "12345",
//    Phone = "1111",
//    Email = "X2X@gmail.com"
//};
//bool ok = vendorCtrl.Insert(newVendor);
//Console.WriteLine($"The insert was successful! {ok}");

//Vendor? X2Xvendor = vendorCtrl.GetVendorByPk(6);
//X2Xvendor.Name = "Xtreme 2 Xtreme";
//X2Xvendor.Address = "11X2X Way";
//X2Xvendor.Phone = "555-555";
//bool updated = vendorCtrl.Update(X2Xvendor);
//Console.WriteLine($"The insert was successful! {updated}");

//UsersController userCtrl = new(connection);

//User? xxuser = userCtrl.GetByPk(8);
//xxuser.Username = "yy";
//xxuser.Password = "yy";
//xxuser.FirstName = "yy";
//xxuser.LastName = "yy";
//bool updated = userCtrl.Update(xxuser);
//Console.WriteLine($"The insert was successful! {updated}");

//User newUser = new()
//{
//    Username = "xx", Password = "xx", FirstName = "xx", LastName = "xx", Phone = "911", Email = "911@gmail.com", IsReviewer = true, IsAdmin = false
//};  //need semicolon after the closing brace.
//bool ok = userCtrl.Insert(newUser);
//Console.WriteLine($"The insert was successful! {ok}");

//bool success = userCtrl.Delete(7);
//Console.WriteLine($"The delete was successful! {success}");

//User ? user = userCtrl.GetByPk(11);
//if(user is null)
//{
//    Console.WriteLine("User is not found!");
//} else
//{
//Console.WriteLine($"{user.FirstName} {user.LastName}");
//}

//IEnumerable<User> users = userCtrl.GetAllUsers();
//foreach(User u in users) //abbreviated to u, so can reuse the user up above.
//{
//    Console.WriteLine($"{u.FirstName} {u.LastName}");
//}

//connection.SelectSql(""); //dont have to call anything, but put empty string so it overwrites it

//connection.SelectProdSql("");

connection.Disconnect();






