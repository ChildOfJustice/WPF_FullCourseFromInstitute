namespace Task_1
{
    public class AbstractHandler: IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }
        
        public virtual object Handle(object request)
        {
            return this._nextHandler?.Handle(request);
        }
    }
}