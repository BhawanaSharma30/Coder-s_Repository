using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceProcessing
{
    public class InvoiceProcessing
    {
        static void Main(string[] args)
        {
            InvoiceProcessing iProc = new InvoiceProcessing();
            List<Invoice> lInvoice = iProc.GenerateInputData();
            iProc.FindMissingInvoice(lInvoice);
            Console.ReadLine();
        }

        List<Invoice> GenerateInputData()
        {
            int input, category, startInvoiceNumber, endInvoiceNumber;
            string product;

            List<Invoice> listInvoice = new List<Invoice>();

            do
            {
                Console.WriteLine("     Options to insert input data, Please select required one         ");
                Console.WriteLine("=========================================================================");
                Console.WriteLine("1=> Press 1 to insert records......");
                Console.WriteLine("0=> Press 0 to exit / continue to find missing Invoices.......");
                int.TryParse(Console.ReadLine(), out input);

                switch (input)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine("Please insert Category...");
                        int.TryParse(Console.ReadLine(), out category);
                        Console.WriteLine("Please insert Product...");
                        product = Console.ReadLine();
                        Console.WriteLine("Please insert startInvoiceNumber...");
                        int.TryParse(Console.ReadLine(), out startInvoiceNumber);
                        Console.WriteLine("Please insert endInvoiceNumber...");
                        int.TryParse(Console.ReadLine(), out endInvoiceNumber);

                        Invoice invoice = new Invoice()
                        {
                            Category = category,
                            Product = product,
                            StartInvoiceNumber = startInvoiceNumber,
                            EndInvoiceNumber = endInvoiceNumber
                        };

                        listInvoice.Add(invoice);
                        break;
                }
                } while (input != 0);
            return listInvoice.OrderBy(s => s.Category).ThenBy(s => s.StartInvoiceNumber).ToList();
        }

        void PrintData(List<Invoice> linvoice)
        {
            foreach (Invoice invoice in linvoice)
            {
                Console.WriteLine(invoice.Category);
                Console.WriteLine(invoice.Product);
                Console.WriteLine(invoice.StartInvoiceNumber);
                Console.WriteLine(invoice.EndInvoiceNumber);
            }
        }

        public List<KeyValuePair<int, int>> FindMissingInvoice(List<Invoice> lInvoice)
        {
            Console.WriteLine("         Below is the missing invoice report....         ");
            Console.WriteLine("===========================================================");
            Console.WriteLine("Category    " + "Invoice Numbers");

            int category = 0, startIn = 0, EndIn = 0, count = 0;
            List<KeyValuePair<int, int>> missingInvoices = new List<KeyValuePair<int, int>>();

            foreach (Invoice I in lInvoice)
            {
                if (category != I.Category)
                    count = 0;
                else
                {
                    if ((I.StartInvoiceNumber - EndIn) > 1)
                    {
                        if (count == 0)
                            Console.Write(I.Category + "            ");
                        for (int i = EndIn + 1; i < I.StartInvoiceNumber; i++)
                        {
                            Console.Write(i + " ");
                            missingInvoices.Add(new KeyValuePair<int,int>(I.Category, i));
                            count++;
                        }
                        count++;
                    }
                    category = I.Category;
                    startIn = I.StartInvoiceNumber;
                    EndIn = I.EndInvoiceNumber;
                }

                if (count == 0)
                    Console.WriteLine("  ");
                category = I.Category;
                startIn = I.StartInvoiceNumber;
                EndIn = I.EndInvoiceNumber;
            }
            return missingInvoices;
        }       
    }

        public class Invoice
        {
            public int Category { get; set; }
            public string Product { get; set; }
            public int StartInvoiceNumber { get; set; }
            public int EndInvoiceNumber { get; set; }
        }
    }

