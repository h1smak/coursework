namespace BlaBlaApi.Models
{
    using System;
    using System.Text.RegularExpressions;

    public class User
    {
        private DateTime _birthDate;
        private string _phone = string.Empty;
        private string _email = string.Empty;
        private UserRole _role;

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Email
        {
            get => _email;
            set
            {
                if (!Regex.IsMatch(value,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                    throw new ArgumentException("Невірний email");
                _email = value;
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                var age = CalculateAge(value);
                if (age < 18)
                    throw new ArgumentException("Користувач має бути старше 18 років");
                _birthDate = value;
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (!Regex.IsMatch(value, @"^\d{10}$"))
                    throw new ArgumentException("В номері має бути рівно 10 цифр");
                _phone = value;
            }
        }

        public double Rating { get; set; } = 0.0;

        public UserRole Role
        {
            get => _role;
            set
            {
                if (!Enum.IsDefined(typeof(UserRole), value))
                    throw new ArgumentException("Невірна роль");
                _role = value;
            }
        }

        public virtual string GetProfile(bool isConfirmedTrip) => isConfirmedTrip
            ? $"{Name}, {Email}, {Phone}, {Rating}"
            : $"{Name}, {Rating}";

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age;
        }
    }

    public enum UserRole
    {
        Passenger,
        Driver
    }

}
