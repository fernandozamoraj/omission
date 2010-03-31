using System;
using System.Collections.Generic;
using NUnit.Framework;
using Omission.Framework;

namespace Omission.UnitTests
{
   
    [TestFixture]
    public class DefaultHandlerConfigurationFixture 
    {
        DefaultExceptionConfiguration _defaultHandlerConfiguration;

        [SetUp]
        public void Setup()
        {
            _defaultHandlerConfiguration = new DefaultExceptionConfiguration(new FakeAppConfig());
        }

        [Test]
        public void Check_it_accepts_a_mapping()
        {
            _defaultHandlerConfiguration.MapExceptionToHandler<Exception>(new FakeHandler());

            IExceptionHandler handler = _defaultHandlerConfiguration.GetHandler(typeof (Exception));

            Assert.IsNotNull(handler);
            Assert.AreEqual(typeof(FakeHandler), handler.GetType());
        }

        [Test]
        public void Check_it_overwrites_a_mapping()
        {
            _defaultHandlerConfiguration.MapExceptionToHandler<Exception>(new FakeHandler());
            _defaultHandlerConfiguration.MapExceptionToHandler<Exception>(new AnotherFakeHandler());
            IExceptionHandler handler = _defaultHandlerConfiguration.GetHandler(typeof(Exception));
            

            Assert.IsNotNull(handler);
            Assert.AreEqual(typeof(AnotherFakeHandler), handler.GetType());
        }

        [Test]
        public void Check_it_holds_multiple_configurations()
        {
            _defaultHandlerConfiguration.MapExceptionToHandler<Exception>(new FakeHandler());
            _defaultHandlerConfiguration.MapExceptionToHandler<ApplicationException>(new AnotherFakeHandler());

            IExceptionHandler fakeExceptionHandler = _defaultHandlerConfiguration.GetHandler(typeof(Exception));
            IExceptionHandler anotherFakeExceptionHandler = _defaultHandlerConfiguration.GetHandler(typeof(ApplicationException));

            Assert.IsNotNull(fakeExceptionHandler);
            Assert.IsNotNull(anotherFakeExceptionHandler);
            Assert.AreNotSame(fakeExceptionHandler, anotherFakeExceptionHandler);
            Assert.AreEqual(typeof(FakeHandler), fakeExceptionHandler.GetType());
            Assert.AreEqual(typeof(AnotherFakeHandler), anotherFakeExceptionHandler.GetType());
        }

        [Test]
        public void Check_logger_can_be_configured()
        {
            _defaultHandlerConfiguration
                .MapExceptionToHandler<ApplicationException>
                (new AnotherFakeHandler())
                .With(new FakeLogger())
                .AndAlso(new AnotherFakeLogger());


            List<IExceptionLogger> loggers = _defaultHandlerConfiguration.GetLoggersFor(new ApplicationException());

            Assert.AreEqual(2, loggers.Count);
            Assert.AreEqual(typeof(FakeLogger), loggers[0].GetType());
            Assert.AreEqual(typeof(AnotherFakeLogger), loggers[1].GetType());

        }

        [Test]
        public void Check_logger_can_be_overwritten()
        {
            _defaultHandlerConfiguration
                .MapExceptionToHandler<ApplicationException>
                (new AnotherFakeHandler())
                .With(new FakeLogger())
                .With(new AnotherFakeLogger());


            List<IExceptionLogger> loggers = _defaultHandlerConfiguration.GetLoggersFor(new ApplicationException());

            Assert.AreEqual(1, loggers.Count);
            Assert.AreEqual(typeof(AnotherFakeLogger), loggers[0].GetType());
        }

        [Test]
        public void Check_loggers_are_cached_properly()
        {
            FakeLogger fakeLogger = new FakeLogger();
            _defaultHandlerConfiguration
                .MapExceptionToHandler<ApplicationException>(new FakeHandler())
                .With(fakeLogger);
            
            _defaultHandlerConfiguration
                .MapExceptionToHandler<NullReferenceException>(new FakeHandler())
                .With(new FakeLogger());

            List<IExceptionLogger> loggersForApplicationException = 
                _defaultHandlerConfiguration.GetLoggersFor(new ApplicationException());
            List<IExceptionLogger> loggersForNullReferenceException =
                _defaultHandlerConfiguration.GetLoggersFor(new NullReferenceException());
            
            
            Assert.AreEqual(1, loggersForApplicationException.Count);
            Assert.AreSame(fakeLogger, loggersForNullReferenceException[0]);
            Assert.AreSame(loggersForApplicationException[0], loggersForNullReferenceException[0]);

        }

    }
}
