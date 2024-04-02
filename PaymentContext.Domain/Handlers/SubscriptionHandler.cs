using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Command;
using PaymentContext.Shared.Handlers;
using System.Xml.Linq;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreatePaypalSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler( IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar seu cadastro");
        }
        //VERIFICAR SE dOCUMENTO JÁ ESTÁ CADASTRADO
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está em uso");

        // VERIFICA SE E-MAIL JÁ ESTA CADASTRADO
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este Email já está em uso");
        // GERAR OS VOS
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood,command.City,command.State, command.Country, command.ZipCode);

        //GEREAR AS ENTIDADES APLICAR AS VALIDAÇOES
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid,
            command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), address, email);

        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Agrupar AS Validaçoes
        AddNotifications(name, document, email, address, student, subscription, payment);

        // Salva AS INFORMAÇOES
        _repository.CreateSubscription(student);

        // ENVIAR E-MAIL DE BOAS VINDAS
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao pague facil","Sua assinatura foi criada");

        // RETORNAR INFORMACOES
        return new CommandResult(true, "Assinatura relizada com sucesso");
    }

    public ICommandResult Handle(CreatePaypalSubscriptionCommand command)
    {
        //VERIFICAR SE dOCUMENTO JÁ ESTÁ CADASTRADO
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está em uso");

        // VERIFICA SE E-MAIL JÁ ESTA CADASTRADO
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este Email já está em uso");
        // GERAR OS VOS
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

        //GEREAR AS ENTIDADES APLICAR AS VALIDAÇOES
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new PayPalPayment(command.TransactionCode,command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid,
            command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), address, email);

        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Agrupar AS Validaçoes
        AddNotifications(name, document, email, address, student, subscription, payment);

        // Salva AS INFORMAÇOES
        _repository.CreateSubscription(student);

        // ENVIAR E-MAIL DE BOAS VINDAS
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao pague facil", "Sua assinatura foi criada");

        // RETORNAR INFORMACOES
        return new CommandResult(true, "Assinatura relizada com sucesso");
    }
}
