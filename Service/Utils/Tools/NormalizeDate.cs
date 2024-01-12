namespace Service.Utils.Tools
{
    public static class NormalizeDate
    {
        public static DateTime GetCurrentDateTime()
        {
            TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
            DateTime dataHoraAtual = DateTime.UtcNow;
            return TimeZoneInfo.ConvertTimeFromUtc(dataHoraAtual, timezone);
        }
    }
}
