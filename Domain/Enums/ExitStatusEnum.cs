namespace Domain.Enums
{
    public enum ExitStatusEnum
    {
        Solicitado,
        EnProceso,
        AutorizacionTutor,
        AutorizacionPreceptor,
        Autorizado,
        Rechazado,
        Cancelado
    }

    public static class ExitStatusEnumExtensions
    {
        public static string ToString(this ExitStatusEnum status)
        {
            return status switch
            {
                ExitStatusEnum.Solicitado => "Solicitado",
                ExitStatusEnum.EnProceso => "En proceso",
                ExitStatusEnum.AutorizacionTutor => "Autorizacion Tutor",
                ExitStatusEnum.AutorizacionPreceptor => "Autorizacion Preceptor",
                ExitStatusEnum.Autorizado => "Autorizado",
                ExitStatusEnum.Rechazado => "Rechazado",
                ExitStatusEnum.Cancelado => "Cancelado",
                _ => "Unknown"
            };
        }

        public static ExitStatusEnum FromString(string statusString)
        {
            return statusString?.ToLower() switch
            {
                "solicitado" => ExitStatusEnum.Solicitado,
                "en proceso" => ExitStatusEnum.EnProceso,
                "autorizacion tutor" => ExitStatusEnum.AutorizacionTutor,
                "autorizacion preceptor" => ExitStatusEnum.AutorizacionPreceptor,
                "autorizado" => ExitStatusEnum.Autorizado,
                "rechazado" => ExitStatusEnum.Rechazado,
                "cancelado" => ExitStatusEnum.Cancelado,
                _ => throw new ArgumentException($"Invalid exit status: {statusString}")
            };
        }
    }
}