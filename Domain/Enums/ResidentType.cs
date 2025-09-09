namespace Domain.Enums
{
    public enum ResidentType
    {
        Regular,
        Especial,
        Temporal,
        Practicante
    }

    public static class ResidentTypeExtensions
    {
        public static string ToString(this ResidentType type)
        {
            return type switch
            {
                ResidentType.Regular => "Regular",
                ResidentType.Especial => "Especial",
                ResidentType.Temporal => "Temporal",
                ResidentType.Practicante => "Practicante",
                _ => "Unknown"
            };
        }

        public static ResidentType FromString(string typeString)
        {
            return typeString?.ToLower() switch
            {
                "regular" => ResidentType.Regular,
                "especial" => ResidentType.Especial,
                "temporal" => ResidentType.Temporal,
                "practicante" => ResidentType.Practicante,
                _ => throw new ArgumentException($"Invalid resident type: {typeString}")
            };
        }

        public static bool IsValid(string typeString)
        {
            return Enum.GetNames(typeof(ResidentType))
                      .Any(name => name.Equals(typeString, StringComparison.OrdinalIgnoreCase));
        }
    }
}