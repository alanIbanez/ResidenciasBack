namespace Domain.Enums
{
    public enum IncidentTypeEnum
    {
        Retraso,
        Incidente,
        Falta,
        Otro
    }

    public static class IncidentTypeEnumExtensions
    {
        public static string ToString(this IncidentTypeEnum type)
        {
            return type switch
            {
                IncidentTypeEnum.Retraso => "Retraso",
                IncidentTypeEnum.Incidente => "Incidente",
                IncidentTypeEnum.Falta => "Falta",
                IncidentTypeEnum.Otro => "Otro",
                _ => "Unknown"
            };
        }

        public static IncidentTypeEnum FromString(string typeString)
        {
            return typeString?.ToLower() switch
            {
                "retraso" => IncidentTypeEnum.Retraso,
                "incidente" => IncidentTypeEnum.Incidente,
                "falta" => IncidentTypeEnum.Falta,
                "otro" => IncidentTypeEnum.Otro,
                _ => throw new ArgumentException($"Invalid incident type: {typeString}")
            };
        }
    }
}