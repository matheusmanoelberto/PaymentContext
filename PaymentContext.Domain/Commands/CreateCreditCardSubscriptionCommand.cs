﻿using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Entities;
using PaymentContext.Shared.Command;

namespace PaymentContext.Domain.Commands;

public class CreateCreditCardSubscriptionCommand : Notifiable, ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }

    public string CardHolderName { get; set; }
    public string CardNumber { get; set; }
    public string LastTransactionNumber { get; set; }

    public string PaymentNumber { get; set; }
    public DateTime PaidDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public decimal Total { get; set; }
    public decimal TotalPaid { get; set; }
    public string Payer { get; set; }
    public string PayerDocument { get; set; }
    public EDocumentType PayerDocumentType { get; set; }
    public string PayerEmail { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract()
             .Requires()
             .HasMinLen(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
             .HasMinLen(LastName, 3, "Name.FirstName", "Sobrenome deve conter pelo menos 3 caracteres")
             .HasMaxLen(FirstName, 40, "Name.FirstName", "Nome deve conter noma ate 40 caracteres")
             );
    }
}
