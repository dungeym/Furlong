namespace Furlong.UnitTests.Synchronous.ChainFactoryRequestResponse.Domain
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