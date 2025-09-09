namespace Domain.Enums
{
    public enum ResidentTypeEnum
    {
        Universitario,
        Colegio
    }

    public static class ResidentTypeEnumExtensions
    {
        public static string ToString(this ResidentTypeEnum type)
        {
            return type switch
            {
                ResidentTypeEnum.Universitario => "Universitario",
                ResidentTypeEnum.Colegio => "Colegio",
                _ => "Unknown"
            };
        }

        public static ResidentTypeEnum FromString(string typeString)
        {
            return typeString?.ToLower() switch
            {
                "universitario" => ResidentTypeEnum.Universitario,
                "colegio" => ResidentTypeEnum.Colegio,
                _ => throw new ArgumentException($"Invalid resident type: {typeString}")
            };
        }
    }
}