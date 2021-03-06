﻿namespace Furlong.UnitTests.Synchronous.ChainFactoryRequestResponse.Domain
{
    public class MyLink1 : LinkBase<MyRequest, MyResponse>
    {
        private readonly bool _contiunue;

        public MyLink1(bool contiunue)
        {
            _contiunue = contiunue;
        }

        public override MyResponse Handle(MyRequest request)
        {
            request.Visited.Add(this.GetType().Name);

            if (_contiunue)
            {
                return CallNext(request);
            }
            else
            {
                return new MyResponse(this);
            }
        }
    }
}