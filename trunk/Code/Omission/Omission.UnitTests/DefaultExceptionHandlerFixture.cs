using System;
using NUnit.Framework;
using Omission.Framework;

namespace Omission.UnitTests
{
    [TestFixture]
    public class DefaultExceptionHandlerFixture
    {
        MasterExceptionHandler _defaultHandler;
        FakeHandler _fakeHandler;
        FakeLogger _fakeLogger;
        DefaultExceptionConfiguration _exceptionConfiguration;

        [SetUp]
        public void Setup()
        {
            _fakeHandler = new FakeHandler();
            _fakeLogger = new FakeLogger();
            _exceptionConfiguration = new DefaultExceptionConfiguration(new FakeAppConfig("DefaultHandlerTest", "0.1"));
            _defaultHandler = new MasterExceptionHandler(_fakeLogger, _fakeHandler, _exceptionConfiguration);
        }

        [Test]
        public void Test_that_it_handles_base_exception()
        {
            string exceptionMessage = "generic exception was thrown!";
            ExceptionHandlingResult handlingResult = _defaultHandler.Handle(new Exception(exceptionMessage));

            Assert.AreEqual(false, handlingResult.ReThrow);
            Assert.AreEqual(true, handlingResult.WasHandled);
            Assert.AreEqual(exceptionMessage, _fakeHandler.LastExceptionMessage);
        }

        [Test]
        public void Test_that_it_handles_any_exception_as_long_as_there_is_a_default_handler()
        {
            Exception exception = new ArgumentOutOfRangeException();
            ExceptionHandlingResult handlingResult = _defaultHandler.Handle(exception);

            Assert.AreEqual(false, handlingResult.ReThrow);
            Assert.AreEqual(true, handlingResult.WasHandled);
            Assert.AreEqual(exception.Message, _fakeHandler.LastExceptionMessage);
        }

    }
}
