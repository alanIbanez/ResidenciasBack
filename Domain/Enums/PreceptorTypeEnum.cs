namespace Domain.Enums
{
    public enum PreceptorTypeEnum
    {
        Administrador,
        Monitor
    }

    public static class PreceptorTypeEnumExtensions
    {
        public static string ToString(this PreceptorTypeEnum type)
        {
            return type switch
            {
                PreceptorTypeEnum.Administrador => "Administrador",
                PreceptorTypeEnum.Monitor => "Monitor",
                _ => "Unknown"
            };
        }

        public static PreceptorTypeEnum FromString(string typeString)
        {
            return typeString?.ToLower() switch
            {
                "administrador" => PreceptorTypeEnum.Administrador,
                "monitor" => PreceptorTypeEnum.Monitor,
                _ => throw new ArgumentException($"Invalid preceptor type: {typeString}")
            };
        }
    }
}