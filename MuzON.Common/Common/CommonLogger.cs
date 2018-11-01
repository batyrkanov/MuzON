using System;

namespace MuzON.Common.Common
{
    public class CommonLogger
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void InfoLog(string type, string action, string name, Guid id, string userName) =>
            logger.Info($"{type}. '{name}' with id: {id} was {action} at {DateTime.Now}, by User: {userName}");
        
    }
}
