namespace Domain.Enums
{
    public enum UserRole
    {
        Admin,
        Preceptor,
        Tutor,
        Guard,
        Resident
    }

    public static class UserRoleExtensions
    {
        public static string ToString(this UserRole role)
        {
            return role switch
            {
                UserRole.Admin => "Admin",
                UserRole.Preceptor => "Preceptor",
                UserRole.Tutor => "Tutor",
                UserRole.Guard => "Guard",
                UserRole.Resident => "Resident",
                _ => "Unknown"
            };
        }

        public static UserRole FromString(string roleString)
        {
            return roleString?.ToLower() switch
            {
                "admin" => UserRole.Admin,
                "preceptor" => UserRole.Preceptor,
                "tutor" => UserRole.Tutor,
                "guard" => UserRole.Guard,
                "resident" => UserRole.Resident,
                _ => throw new ArgumentException($"Invalid user role: {roleString}")
            };
        }

        public static int GetDefaultRoleId(this UserRole role)
        {
            return role switch
            {
                UserRole.Admin => 1,
                UserRole.Preceptor => 2,
                UserRole.Tutor => 3,
                UserRole.Guard => 4,
                UserRole.Resident => 5,
                _ => 0
            };
        }

        public static bool IsValid(string roleString)
        {
            return Enum.GetNames(typeof(UserRole))
                      .Any(name => name.Equals(roleString, StringComparison.OrdinalIgnoreCase));
        }
    }
}