using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace GroupProject;

public class Invoice
{
    public int ID { get; }
    public int InvoiceNumber { get; }
    public string CustomerName { get; set; }
    public string InvoiceDate { get; }
    public SqlMoney InvoiceAmount { get; }

    public Invoice(int id, int invoiceNumber, string customerName, string invoiceDate, SqlMoney invoiceAmount)
    {
        ID = id;
        InvoiceNumber = invoiceNumber;
        CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
        InvoiceDate = invoiceDate ?? throw new ArgumentNullException(nameof(invoiceDate));
        InvoiceAmount = invoiceAmount;
    }

}