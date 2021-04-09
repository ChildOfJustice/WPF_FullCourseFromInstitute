namespace Task_1
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        
        object Handle(object request);
    }
}