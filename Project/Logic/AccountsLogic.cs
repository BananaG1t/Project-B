using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{
    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        // Could do something here
    }

    public AccountModel GetById(int id)
    {
        return AccountsAccess.GetById(id);
    }
    public static  List<AccountModel> GetAllUserAccounts()
    {
        return AccountsAccess.GetAllUserAccounts();
    }


    public AccountModel CheckLogin(string email, string password)
    {
        AccountModel acc = AccountsAccess.GetByEmail(email);
        if (acc != null && acc.Password == password)
        {
            CurrentAccount = acc;
            return acc;
        }
        return null;
    }

    public bool CheckNewEmail(string email) // added bool to check if email already exsits in database
    {
        AccountModel acc = AccountsAccess.GetByEmail(email);
        if (acc == null) return true;
        else return false;
    }

    public bool Validinfo(string email, string password) // check if new email and password are valid 
    {
        if (!email.Contains('@')& !email.Contains('.')) 
        {
            Console.WriteLine("Invalid email");
            return false;
        }
        else if (!CheckNewEmail(email))
        {
            Console.WriteLine("Account with this email already exists");
            return false;
        }
        else if (password.Length < 1) 
        {
            Console.WriteLine("Invalid password");
            return false;
        }
        else 
        {
            Console.WriteLine("New account added");
            return true;
        }
    }



}




