using NLog;

namespace TransShip.LogBook
{
    public static class LogBook
    {
        public static Logger TextLog { get; set; }
        public static Logger EMail { get; set; }
    }
}
