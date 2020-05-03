namespace Furlong.UnitTests.Asynchronous.AsyncChainFactoryRequestResponse.Domain
{
    public class MyResponse
    {
        public MyResponse(object handledBy)
        {
            HandledBy = handledBy;
        }

        public object HandledBy { get; }
    }
}