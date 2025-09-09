namespace Domain.Enums
{
    public enum ExitTypeEnum
    {
        Casual,
        Especial
    }

    public static class ExitTypeEnumExtensions
    {
        public static string ToString(this ExitTypeEnum type)
        {
            return type switch
            {
                ExitTypeEnum.Casual => "Casual",
                ExitTypeEnum.Especial => "Especial",
                _ => "Unknown"
            };
        }

        public static ExitTypeEnum FromString(string typeString)
        {
            return typeString?.ToLower() switch
            {
                "casual" => ExitTypeEnum.Casual,
                "especial" => ExitTypeEnum.Especial,
                _ => throw new ArgumentException($"Invalid exit type: {typeString}")
            };
        }
    }
}