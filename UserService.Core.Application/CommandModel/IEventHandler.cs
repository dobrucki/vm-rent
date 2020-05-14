namespace UserService.Core.Application.CommandModel
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IEvent
    {
        
    }
}