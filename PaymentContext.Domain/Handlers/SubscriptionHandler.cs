using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Repositories;
using PaymentContext.Shared.Command;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
{
    private readonly IStudentRepository _repository;

    public SubscriptionHandler( IStudentRepository repository)
    {
        _repository = repository;
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
        if (_repository.DocumentExists(command.Document)
        {
            AddNotification("Document", "Este CPF já está em uso");
        }

        // VERIFICA SE E-MAIL JÁ ESTA CADASTRADO
        if (_repository.EmailExists(command.Email)
            AddNotification("Email", "Este Email já está em uso");
        // GERAR OS VOS
        //GEREAR AS ENTIDADES APLICAR AS VALIDAÇOES
        // SALVAR AS INFORMAÇOES
        // ENVIAR E-MAIL DE BOAS VINDAS
        // RETORNAR INFORMACOES
        return new CommandResult(true, "Assinatura relizada com sucesso");
    }
}
