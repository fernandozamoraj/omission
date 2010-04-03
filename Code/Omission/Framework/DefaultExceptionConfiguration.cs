using System;
using System.Collections.Generic;
using Omission.Framework.Windows;

namespace Omission.Framework
{
    public class DefaultExceptionConfiguration : IExceptionConfiguration
    {
        Type _lastExceptionType;
        Dictionary<Type, IExceptionHandler> _handlerEntries;
        Dictionary<string, IExceptionLogger> _loggerEntries;
        Dictionary<Type, IExceptionHandler> _cachedHandlers;
        Dictionary<Type, IExceptionLogger> _cachedLoggers;
        List<Type> _exceptionsIgnoreForHandlingList; 

        public DefaultExceptionConfiguration(IAppConfig appConfig)
        {
            _handlerEntries = new Dictionary<Type, IExceptionHandler>();
            _loggerEntries = new Dictionary<string, IExceptionLogger>();
            _cachedHandlers = new Dictionary<Type, IExceptionHandler>();
            _cachedLoggers = new Dictionary<Type, IExceptionLogger>();
            _exceptionsIgnoreForHandlingList = new List<Type>();

            GenericWindowsHandler genericHandler = new GenericWindowsHandler(appConfig);

            _cachedHandlers.Add(genericHandler.GetType(), genericHandler);
        }

        public Dictionary<Type, IExceptionHandler> GetHandlers()
        {
            return _handlerEntries;
        }

        public List<IExceptionLogger> GetLoggersFor(Exception exception)
        {
            List<IExceptionLogger> listOfLoggers = new List<IExceptionLogger>();

            foreach (var entry in _loggerEntries)
            {
               if(entry.Key.IndexOf(exception.GetType().ToString())==0)
               {
                   listOfLoggers.Add(entry.Value);
               }
            }

            return listOfLoggers;
        }

        //Allows for configuring handlers
        public IExceptionConfiguration MapExceptionToHandler<TExceptionType>(IExceptionHandler handler) where TExceptionType : Exception
        {
            _lastExceptionType = typeof (TExceptionType);

            IExceptionHandler actualHandler = _cachedHandlers.ContainsKey(handler.GetType())
                                                  ? _cachedHandlers[handler.GetType()]
                                                  : handler;

            if (_handlerEntries.ContainsKey(_lastExceptionType))
            {
                //Overwrite the handler
                _handlerEntries[_lastExceptionType] = actualHandler;
            }
            else
            {
                //Add the new handler
                _handlerEntries.Add(_lastExceptionType, actualHandler);
            }

            if(!_cachedHandlers.ContainsKey(handler.GetType()))
            {
                _cachedHandlers.Add(handler.GetType(), handler);
            }

            return this;
        }

        public IExceptionConfiguration MapExceptionToLogger<TException>(IExceptionLogger logger)
        {
            _lastExceptionType = typeof (TException);

            With(logger);

            return this;
        }

        public IExceptionConfiguration With(IExceptionLogger logger)
        {
            string loggerKey = GetLoggerKey(_lastExceptionType, logger.GetType());

            List<string> keysToRemove = GetKeysOfEntriesToRemove();

            RemoveAllEntries(keysToRemove);

            AddLoggerEntry(loggerKey, logger);

            return this;
        }

        public IExceptionConfiguration IgnoreExceptionForHandling<TExceptionType>() where TExceptionType : Exception
        {
            _lastExceptionType = typeof (TExceptionType);

            if(!_exceptionsIgnoreForHandlingList.Contains(typeof(TExceptionType)))
            {
                _exceptionsIgnoreForHandlingList.Add(typeof(TExceptionType));
            }

            return this;
        }

        public bool ShouldIgnoreForHandling(Type exceptionType)
        {
            return _exceptionsIgnoreForHandlingList.Contains(exceptionType);
        }

        public IExceptionConfiguration AndAlso(IExceptionLogger logger)
        {
            string loggerKey = GetLoggerKey(_lastExceptionType, logger.GetType());
            
            AddLoggerEntry(loggerKey, logger);

            return this;
        }

        public IExceptionHandler GetHandler(Type exceptionType)
        {
            IExceptionHandler resultHandler = null;

            if (!_exceptionsIgnoreForHandlingList.Contains(exceptionType))
            {
                foreach (var entry in _handlerEntries)
                {
                    if (entry.Key == exceptionType)
                    {
                        resultHandler = entry.Value;
                        break;
                    }
                }
            }

            return resultHandler;
        }

        private List<string> GetKeysOfEntriesToRemove()
        {
            List<string> keysToRemove = new List<string>();

            foreach (var loggerEntry in _loggerEntries)
            {
                if (loggerEntry.Key.IndexOf(_lastExceptionType.ToString()) == 0)
                {
                    keysToRemove.Add(loggerEntry.Key);
                }
            }

            return keysToRemove;
        }

        private void RemoveAllEntries(IEnumerable<string> keysToRemove)
        {
            //Remove all entries
            foreach (var key in keysToRemove)
            {
                _loggerEntries.Remove(key);
            }
        }

        private void AddLoggerEntry(string loggerKey, IExceptionLogger logger)
        {
            IExceptionLogger actualLogger = 
                _cachedLoggers.ContainsKey(logger.GetType())
                ? _cachedLoggers[logger.GetType()] : logger;

            if (!_loggerEntries.ContainsKey(loggerKey))
            {
                //Add the new handler
                _loggerEntries.Add(loggerKey, actualLogger);
            }

            if(!_cachedLoggers.ContainsKey(logger.GetType()))
            {
                _cachedLoggers.Add(logger.GetType(), logger);
            }
        }

        private string GetLoggerKey(Type exceptionType, Type exceptionLoggerType)
        {
            return exceptionType + "|" + exceptionLoggerType;
        }
    }
}