namespace Furlong.UnitTests.Asynchronous.AsyncLocalChainFactoryRequestReponse.Domain
{
    public class MyResponse
    {
        public MyResponse(string handledBy)
        {
            HandledBy = handledBy;
        }

        public string HandledBy { get; }
    }
}