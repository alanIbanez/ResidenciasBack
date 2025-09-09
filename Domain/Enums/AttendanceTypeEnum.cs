namespace Domain.Enums
{
    public enum AttendanceTypeEnum
    {
        Diaria,
        Evento
    }

    public static class AttendanceTypeEnumExtensions
    {
        public static string ToString(this AttendanceTypeEnum type)
        {
            return type switch
            {
                AttendanceTypeEnum.Diaria => "Diaria",
                AttendanceTypeEnum.Evento => "Evento",
                _ => "Unknown"
            };
        }

        public static AttendanceTypeEnum FromString(string typeString)
        {
            return typeString?.ToLower() switch
            {
                "diaria" => AttendanceTypeEnum.Diaria,
                "evento" => AttendanceTypeEnum.Evento,
                _ => throw new ArgumentException($"Invalid attendance type: {typeString}")
            };
        }
    }
}