﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranzadi.DocumentAnalysis.Messaging.Test
{
    public class HttpMessageHandlerMoq : HttpMessageHandler
    {
        private readonly Func<int, HttpRequestMessage, HttpResponseMessage> sendAsyncFun;

        private int nOfCalls = 0;
        private int expectedCalls;



        public HttpMessageHandlerMoq(int nExpectedCalls, Func<int, HttpRequestMessage, HttpResponseMessage> validation)
        {
            this.sendAsyncFun = validation;
            this.nOfCalls = 0;
            this.expectedCalls = nExpectedCalls;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            this.nOfCalls++;
            HttpResponseMessage r = this.sendAsyncFun(this.nOfCalls, request);
            return Task.FromResult<HttpResponseMessage>(r);
        }

        public void Verify()
        {
            Assert.AreEqual(this.expectedCalls, this.nOfCalls);
        }

    }
}
