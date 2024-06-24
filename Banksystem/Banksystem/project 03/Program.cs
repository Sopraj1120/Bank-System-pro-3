using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace project_03
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        static Bank bank = new Bank(); 

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Create a Customer");
                Console.WriteLine("2. Create an Account");
                Console.WriteLine("3. Deposit Funds");
                Console.WriteLine("4. Withdraw Funds");
                Console.WriteLine("5. Transfer Funds");
                Console.WriteLine("6. View Customer Details");
                Console.WriteLine("7. Exit");

                Console.Write("Enter your choice (1-7): ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Creating a customer...");
                        CreateCustomer();
                        break;
                    case "2":
                        Console.WriteLine("Creating an account...");
                        CreateAccount();
                        break;
                    case "3":
                        Console.WriteLine("Depositing funds...");
                        DepositFunds();
                        break;
                    case "4":
                        Console.WriteLine("Withdrawing funds...");
                        WithdrawFunds();
                        break;
                    case "5":
                        Console.WriteLine("Transferring funds...");
                        TransferFunds();
                        break;
                        break;
                    case "6":
                        Console.WriteLine("Viewing customer details...");
                        ViewCustomerDetails();
                        break;
                    case "7":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please enter a number from 1 to 7.");
                        break;
                }

                Console.WriteLine();
            }
        }

        public static void CreateCustomer()
        {
            Console.WriteLine("Enter Customer ID:");
            int customerId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Customer Name:");
            string customerName = Console.ReadLine();

            Console.WriteLine("Enter Customer Email:");
            string customerEmail = Console.ReadLine();

            Customer newCustomer = new Customer
            {
                CustomerId = customerId,
                Name = customerName,
                Email = customerEmail
            };
            bank.AddCustomer(newCustomer);

            Console.WriteLine("Customer created successfully.");
        }
        public enum AccountType
        {
            Savings,
            Checking
        }


        public static void CreateAccount()
        {
            Console.WriteLine("Enter Customer ID:");
            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Customer ID:");
            }

            Customer findCustomer = bank.FindCustomerById(customerId);
            if (findCustomer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.WriteLine("Enter Account Number:");
            int accountNumber;
            while (!int.TryParse(Console.ReadLine(), out accountNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid Account Number:");
            }

            Console.WriteLine("Enter Account Type (1 for Savings, 2 for Checking):");
            string accountTypeInput = Console.ReadLine();
            AccountType accountType;
            while (!Enum.TryParse(accountTypeInput, out accountType) || !Enum.IsDefined(typeof(AccountType), accountType))
            {
                Console.WriteLine("Invalid input. Please enter a valid Account Type (1 for Savings, 2 for Checking):");
                accountTypeInput = Console.ReadLine();
            }

            Console.WriteLine("Enter the Initial Deposit Amount:");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid positive amount:");
            }

            Account newAccount;
            switch (accountType)
            {
                case AccountType.Savings:
                    newAccount = new SavingAccount(accountNumber, customerId, amount);
                    findCustomer.Accounts.Add(newAccount);
                    Console.WriteLine("Saving account created and saved successfully.");
                    break;
                case AccountType.Checking:
                    newAccount = new CheckingAccount(accountNumber, customerId, amount);
                    findCustomer.Accounts.Add(newAccount);
                    Console.WriteLine("Checking account created and saved successfully.");
                    break;
                default:
                    Console.WriteLine("Invalid account type.");
                    break;
            }
        }

        public static void DepositFunds()
        {
            Console.WriteLine("Enter Account Number:");
            int accountNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter The Amount:");
            decimal amount = decimal.Parse(Console.ReadLine());

            bool depositSuccessful = bank.Deposit(accountNumber, amount);

            if (depositSuccessful)
            {
                Console.WriteLine("Funds deposited successfully.");
            }
            else
            {
                Console.WriteLine("Failed to deposit funds. Account not found.");
            }
        }

        public static void WithdrawFunds()
        {
            Console.WriteLine("Enter Account Number:");
            int accountNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter The Amount to Withdraw:");
            decimal amount = decimal.Parse(Console.ReadLine());

            bool withdrawSuccessful = bank.Withdraw(accountNumber, amount);

            if (withdrawSuccessful)
            {
                Console.WriteLine("Funds withdrawn successfully.");
            }
            else
            {
                Console.WriteLine("Failed to withdraw funds. Insufficient balance or account not found.");
            }
        }

        public static void TransferFunds()
        {
            Console.WriteLine("Enter Source Account Number:");
            int sourceAccountNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Destination Account Number:");
            int destinationAccountNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter The Transfer Amount:");
            decimal amount = decimal.Parse(Console.ReadLine());

            bool transferSuccessful = bank.Transfer(sourceAccountNumber, destinationAccountNumber, amount);

            if (transferSuccessful)
            {
                Console.WriteLine("Funds transferred successfully.");
            }
            else
            {
                Console.WriteLine("Failed to transfer funds. Insufficient balance or account not found.");
            }
        }

        public static void ViewCustomerDetails()
        {
            Console.WriteLine("Enter Customer ID:");
            int customerId;
            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Customer ID:");
            }

            Customer findCustomer = bank.FindCustomerById(customerId);

            if (findCustomer != null)
            {
                Console.WriteLine($"Customer found: {findCustomer.CustomerId} - {findCustomer.Name}");

                foreach (var account in findCustomer.Accounts)
                {
                    Console.WriteLine($"Account: {account.AccountNumber}, Type: {account.GetType().Name}, Balance: {account.Balance}");
                }
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
    }


}





