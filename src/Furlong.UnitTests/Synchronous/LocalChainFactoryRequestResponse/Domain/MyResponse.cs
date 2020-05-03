namespace Furlong.UnitTests.Synchronous.LocalChainFactoryRequestResponse.Domain
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