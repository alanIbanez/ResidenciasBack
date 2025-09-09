namespace Domain.Enums
{
    public enum NotificationType
    {
        Salida,
        Autorizacion,
        Evento,
        Asistencia,
        Novedad
    }

    public static class NotificationTypeExtensions
    {
        public static string ToString(this NotificationType type)
        {
            return type switch
            {
                NotificationType.Salida => "Salida",
                NotificationType.Autorizacion => "Autorizacion",
                NotificationType.Evento => "Evento",
                NotificationType.Asistencia => "Asistencia",
                NotificationType.Novedad => "Novedad",
                _ => "Unknown"
            };
        }

        public static NotificationType FromString(string typeString)
        {
            return typeString?.ToLower() switch
            {
                "salida" => NotificationType.Salida,
                "autorizacion" => NotificationType.Autorizacion,
                "evento" => NotificationType.Evento,
                "asistencia" => NotificationType.Asistencia,
                "novedad" => NotificationType.Novedad,
                _ => throw new ArgumentException($"Invalid notification type: {typeString}")
            };
        }

        public static bool IsValid(string typeString)
        {
            return Enum.GetNames(typeof(NotificationType))
                      .Any(name => name.Equals(typeString, StringComparison.OrdinalIgnoreCase));
        }
    }
}