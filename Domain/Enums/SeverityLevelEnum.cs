namespace Domain.Enums
{
    public enum SeverityLevelEnum
    {
        Leve,
        Moderado,
        Grave
    }

    public static class SeverityLevelEnumExtensions
    {
        public static string ToString(this SeverityLevelEnum level)
        {
            return level switch
            {
                SeverityLevelEnum.Leve => "Leve",
                SeverityLevelEnum.Moderado => "Moderado",
                SeverityLevelEnum.Grave => "Grave",
                _ => "Unknown"
            };
        }

        public static SeverityLevelEnum FromString(string levelString)
        {
            return levelString?.ToLower() switch
            {
                "leve" => SeverityLevelEnum.Leve,
                "moderado" => SeverityLevelEnum.Moderado,
                "grave" => SeverityLevelEnum.Grave,
                _ => throw new ArgumentException($"Invalid severity level: {levelString}")
            };
        }
    }
}