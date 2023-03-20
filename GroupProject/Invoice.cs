using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace GroupProject;

/// <summary>
/// 
/// </summary>
public class Invoice
{
    /// <summary>
    /// 
    /// </summary>
    public int ID { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public int InvoiceNumber { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public string CustomerName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string InvoiceDate { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public double InvoiceAmount { get; }

    public Invoice(int id, int invoiceNumber, string customerName, string invoiceDate, double invoiceAmount)
    {
        ID = id;
        InvoiceNumber = invoiceNumber;
        CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
        InvoiceDate = invoiceDate ?? throw new ArgumentNullException(nameof(invoiceDate));
        InvoiceAmount = invoiceAmount;
    }

}