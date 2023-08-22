namespace CustomerManagement.API.Command.Interfaces
{
    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand
    {
        public Task<TResult> HandleAsync(TCommand command);
    }
}
