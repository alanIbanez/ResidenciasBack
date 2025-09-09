namespace Domain.Enums
{
    public enum UserRoleEnum
    {
        Admin,
        Preceptor,
        Tutor,
        Guard,
        Resident
    }

    public static class UserRoleEnumExtensions
    {
        public static string ToString(this UserRoleEnum role)
        {
            return role switch
            {
                UserRoleEnum.Admin => "Admin",
                UserRoleEnum.Preceptor => "Preceptor",
                UserRoleEnum.Tutor => "Tutor",
                UserRoleEnum.Guard => "Guard",
                UserRoleEnum.Resident => "Resident",
                _ => "Unknown"
            };
        }

        public static UserRoleEnum FromString(string roleString)
        {
            return roleString?.ToLower() switch
            {
                "admin" => UserRoleEnum.Admin,
                "preceptor" => UserRoleEnum.Preceptor,
                "tutor" => UserRoleEnum.Tutor,
                "guard" => UserRoleEnum.Guard,
                "resident" => UserRoleEnum.Resident,
                _ => throw new ArgumentException($"Invalid user role: {roleString}")
            };
        }

        public static int GetDefaultRoleId(this UserRoleEnum role)
        {
            return role switch
            {
                UserRoleEnum.Admin => 1,
                UserRoleEnum.Preceptor => 2,
                UserRoleEnum.Tutor => 3,
                UserRoleEnum.Guard => 4,
                UserRoleEnum.Resident => 5,
                _ => 0
            };
        }
    }
}