using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvoiceProcessing;
using System.Collections.Generic;
using System.Linq;

namespace MissingInvoice_UnitTests
{
    [TestClass]
    public class MissingInvoiceUnitTests
    {
        [TestMethod]
        public void TestReturn()
        {
            //Arrange
           List<Invoice> invoice = new List<Invoice>()
            {
                new Invoice () { Category = 1, Product = "A", StartInvoiceNumber = 101, EndInvoiceNumber = 103}
            };

            //Act
            InvoiceProcessing.InvoiceProcessing invoiceProcessing = new InvoiceProcessing.InvoiceProcessing();
            List<KeyValuePair<int, int>> missingInvoicesPairs = invoiceProcessing.FindMissingInvoice(invoice);

           //Assert
            Assert.AreEqual(0, missingInvoicesPairs.Count);            
        }

        [TestMethod]
        public void MissingInvoicesTest()
        {
            //Arrange
            List<Invoice> missingInvoice = new List<Invoice>()
            {
                new Invoice () { Category = 1, Product = "A", StartInvoiceNumber = 101, EndInvoiceNumber = 103},
                new Invoice () { Category = 1, Product = "B", StartInvoiceNumber = 120, EndInvoiceNumber = 121 }
            };
           
            //Act
            InvoiceProcessing.InvoiceProcessing missingInvoices = new InvoiceProcessing.InvoiceProcessing();
            List<KeyValuePair<int, int>> missingInvoicesPairs = missingInvoices.FindMissingInvoice(missingInvoice);

            //Assert
            Assert.AreEqual(16, missingInvoicesPairs.Count);
        }

        [TestMethod]
        public void MissingInvoicesTestWithMultipleCategory()
        {
            //Arrange
            List<Invoice> missingInvoice = new List<Invoice>()
            {
                new Invoice () { Category = 1, Product = "A", StartInvoiceNumber = 101, EndInvoiceNumber = 103},
                new Invoice () { Category = 1, Product = "B", StartInvoiceNumber = 105, EndInvoiceNumber = 106 },
                new Invoice () { Category = 2, Product = "C", StartInvoiceNumber = 201, EndInvoiceNumber = 205 },
                new Invoice () { Category = 2, Product = "A", StartInvoiceNumber = 209, EndInvoiceNumber = 212 }
            };

            //Act
            InvoiceProcessing.InvoiceProcessing missingInvoices = new InvoiceProcessing.InvoiceProcessing();
            List<KeyValuePair<int, int>> missingInvoicesPairs = missingInvoices.FindMissingInvoice(missingInvoice);

            //Assert
            Assert.AreEqual(4, missingInvoicesPairs.Count);
        }

        [TestMethod]
        public void MissingInvoicesTestWithMulCatinRandomOrder()
        {
            //Arrange
            List<Invoice> missingInvoice = new List<Invoice>()
            {
                new Invoice () { Category = 1, Product = "A", StartInvoiceNumber = 101, EndInvoiceNumber = 103},
                new Invoice () { Category = 1, Product = "B", StartInvoiceNumber = 105, EndInvoiceNumber = 106 },
                new Invoice () { Category = 2, Product = "C", StartInvoiceNumber = 201, EndInvoiceNumber = 205 },
                new Invoice () { Category = 1, Product = "A", StartInvoiceNumber = 209, EndInvoiceNumber = 212 }
            };
            List<Invoice> missingInvoiceFinal = missingInvoice.OrderBy(s => s.Category).ThenBy(s => s.StartInvoiceNumber).ToList(); 
            
            //Act
            InvoiceProcessing.InvoiceProcessing missingInvoices = new InvoiceProcessing.InvoiceProcessing();
            List<KeyValuePair<int, int>> missingInvoicesPairs = missingInvoices.FindMissingInvoice(missingInvoiceFinal);

            //Assert
            Assert.AreEqual(103, missingInvoicesPairs.Count);
        }
        
    }
}
